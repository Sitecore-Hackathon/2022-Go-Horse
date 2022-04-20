using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Configuration;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Publishing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using GoHorse.CLI.SpeShell.Dataservices;

namespace GoHorse.CLI.SpeShell.Tasks
{
    public class SpeTask
    {
        private readonly IRootConfigurationManager _rootConfigurationManager;
        private readonly ILogger _logger;
        private readonly ISpe _runCommand;

        public SpeTask(
          IRootConfigurationManager rootConfigurationManager,
          ILoggerFactory loggerFactory,
          IContentPublisher contentPublisher,
          ISpe runCommand)
        {
            _rootConfigurationManager = rootConfigurationManager ?? throw new ArgumentNullException(nameof(rootConfigurationManager));
            _logger = loggerFactory.CreateLogger<SpeTask>();
            _runCommand = runCommand;
        }

        public void LogConsoleInformation(string message)
        {
            _logger.LogConsoleInformation(message, new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
        }

        public async Task Execute(SpeOptions options, string id)
        {
            options.Validate();
            EnvironmentConfiguration environmentConfiguration;
            if (!(await _rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out environmentConfiguration))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string> list = (await _runCommand.SpeIdAsync(environmentConfiguration, id, options.Session).ConfigureAwait(false)).ToList();
            stopwatch.Stop();

            _logger.LogTrace(string.Format("Results: Loaded in {0}ms ({1}).", stopwatch.ElapsedMilliseconds, list.Count));

            if (list.Any())
            {
                _logger.LogConsoleInformation($"Results: PowerShell script successful executed {id}", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
                using (List<string>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        _logger.LogConsoleInformation(enumerator.Current, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
                    environmentConfiguration = null;
                    stopwatch = null;
                }
            }
            else
            {
                _logger.LogConsoleInformation($"Sitecore CLI GoHorse didn't find any PowerShell script with the ID {id} in {environmentConfiguration.Host}", new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
                environmentConfiguration = null;
                stopwatch = null;
            }
        }

        public async Task ExecuteInline(SpeOptions options, string inlineScript)
        {
            options.Validate();
            if (!(await _rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out EnvironmentConfiguration environmentConfiguration))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");
            Stopwatch stopwatch = Stopwatch.StartNew();

            List<string> list = (await _runCommand.SpeInlineAsync(environmentConfiguration, inlineScript, options.Session).ConfigureAwait(false)).ToList();
            stopwatch.Stop();

            _logger.LogTrace(string.Format("Results: Loaded in {0}ms ({1}).", stopwatch.ElapsedMilliseconds, list.Count));

            if (list.Any())
            {
                _logger.LogConsoleInformation($"Results: PowerShell script successful executed {inlineScript}", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
                using (List<string>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        _logger.LogConsoleInformation(enumerator.Current, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
                    environmentConfiguration = null;
                    stopwatch = null;
                }
            }
            else
            {
                _logger.LogConsoleInformation($"Sitecore CLI GoHorse couldn't run the PowerShell inline script '{inlineScript}' in Host: '{environmentConfiguration.Host}'", new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
                stopwatch = null;
            }
        }

        public async Task ExecuteFile(SpeOptions options, string scriptFile)
        {
            options.Validate();
            if (!(await _rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out EnvironmentConfiguration environmentConfiguration))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");

            Stopwatch stopwatch = Stopwatch.StartNew();

            // File doesn't exists
            if (!File.Exists(scriptFile))
            {
                _logger.LogConsoleInformation($"Sitecore CLI GoHorse couldn't run the PowerShell script file '{scriptFile}' in Host: '{environmentConfiguration.Host}'", new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
                stopwatch = null;
                return;
            }

            // Get script from PS1 if it exists
            var inlineScript = File.ReadAllText(scriptFile);

            // Run inlise script
            List<string> list = (await _runCommand.SpeInlineAsync(environmentConfiguration, inlineScript, options.Session).ConfigureAwait(false)).ToList();
            stopwatch.Stop();
            _logger.LogTrace(string.Format("Results: Loaded in {0}ms ({1}).", stopwatch.ElapsedMilliseconds, list.Count));

            if (list.Any())
            {
                _logger.LogConsoleInformation($"Results: PowerShell script successful executed {scriptFile}", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
                using (List<string>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        _logger.LogConsoleInformation(enumerator.Current, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
                    environmentConfiguration = null;
                    stopwatch = null;
                }
            }
            else
            {
                _logger.LogConsoleInformation($"Sitecore CLI GoHorse couldn't run the PowerShell script file '{scriptFile}' in Host: '{environmentConfiguration.Host}'", new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
                stopwatch = null;
            }
        }
    }
}
