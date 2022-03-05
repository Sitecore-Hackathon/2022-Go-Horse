// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Tasks.ListOfTargetsTask
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

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
    public class RunCommandTask
    {
        private readonly IRootConfigurationManager _rootConfigurationManager;
        private readonly ILogger _logger;
        private readonly IContentPublisher _contentPublisher;
        private readonly IRunCommand _runCommand;

        public RunCommandTask(
          IRootConfigurationManager rootConfigurationManager,
          ILoggerFactory loggerFactory,
          IContentPublisher contentPublisher,
          IRunCommand runCommand)
        {
            this._rootConfigurationManager = rootConfigurationManager ?? throw new ArgumentNullException(nameof(rootConfigurationManager));
            this._logger = (ILogger)loggerFactory.CreateLogger<PublishTask>();
            this._contentPublisher = contentPublisher;
            _runCommand = runCommand;
        }

        public async Task Execute(RunCommandOptions options, string id)
        {
            ((TaskOptionsBase)options).Validate();
            EnvironmentConfiguration environmentConfiguration;
            if (!(await this._rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out environmentConfiguration))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");
            Stopwatch stopwatch = Stopwatch.StartNew();
            //List<string> list = (await this._contentPublisher.GetListOfTargetsAsync(environmentConfiguration).ConfigureAwait(false)).ToList<string>();
            List<string> list = (await this._runCommand.RunCommandAsync(environmentConfiguration, id).ConfigureAwait(false)).ToList<string>();
            stopwatch.Stop();

            this._logger.LogTrace(string.Format("Targets: Loaded in {0}ms ({1} targets).", (object)stopwatch.ElapsedMilliseconds, (object)list.Count));


            // Run command



            if (list.Any<string>())
            {
                ColorLogExtensions.LogConsoleInformation(this._logger, "Targets list:", new ConsoleColor?(ConsoleColor.Yellow), new ConsoleColor?());
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
                ColorLogExtensions.LogConsoleInformation(this._logger, string.Format("Sitecore Cli didn't find any publishing targets on {0}", (object)environmentConfiguration.Host), new ConsoleColor?(ConsoleColor.Red), new ConsoleColor?());
                environmentConfiguration = (EnvironmentConfiguration)null;
                stopwatch = (Stopwatch)null;
            }
        }
    }
}
