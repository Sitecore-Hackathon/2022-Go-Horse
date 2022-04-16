using GoHorse.CLI.Command.Dataservices;
using Microsoft.Extensions.Logging;
using Sitecore.DevEx.Client.Logging;
using Sitecore.DevEx.Client.Tasks;
using Sitecore.DevEx.Configuration;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Publishing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Tasks
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
            this._rootConfigurationManager = rootConfigurationManager ?? throw new ArgumentNullException(nameof(rootConfigurationManager));
            this._logger = (ILogger)loggerFactory.CreateLogger<SpeTask>();
            _runCommand = runCommand;
        }

        public async Task Execute(SpeOptions options, string id)
        {
            ((TaskOptionsBase)options).Validate();
            EnvironmentConfiguration environmentConfiguration;
            if (!(await this._rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out environmentConfiguration))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<string> list = (await this._runCommand.SpeAsync(environmentConfiguration, id).ConfigureAwait(false)).ToList<string>();
            stopwatch.Stop();

            _logger.LogTrace(string.Format("Results: Loaded in {0}ms ({1}).", (object)stopwatch.ElapsedMilliseconds, (object)list.Count));

            if (list.Any<string>())
            {
                ColorLogExtensions.LogConsoleInformation(this._logger, $"Results: PowerShell script successful executed {id}", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
                using (List<string>.Enumerator enumerator = list.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                        ColorLogExtensions.LogConsoleInformation(this._logger, enumerator.Current, new ConsoleColor?(ConsoleColor.Green), new ConsoleColor?());
                    environmentConfiguration = (EnvironmentConfiguration)null;
                    stopwatch = (Stopwatch)null;
                }
            }
            else
            {
                ColorLogExtensions.LogConsoleInformation(this._logger, $"Sitecore CLI GoHorse didn't find any PowerShell script with the ID {id} in {(object)environmentConfiguration.Host}", new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
                environmentConfiguration = (EnvironmentConfiguration)null;
                stopwatch = (Stopwatch)null;
            }
        }
    }
}
