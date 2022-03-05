// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Tasks.PublishTask
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

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
    public class PublishTask
    {
        private readonly IRootConfigurationManager _rootConfigurationManager;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly IContentPublisher _contentPublisher;

        public PublishTask(
          IRootConfigurationManager rootConfigurationManager,
          ILoggerFactory loggerFactory,
          IContentPublisher contentPublisher)
        {
            this._rootConfigurationManager = rootConfigurationManager ?? throw new ArgumentNullException(nameof(rootConfigurationManager));
            this._loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
            this._logger = (ILogger)loggerFactory.CreateLogger<PublishTask>();
            this._contentPublisher = contentPublisher;
        }

        public async Task Execute(PublishTaskOptions options)
        {
            ((TaskOptionsBase)options).Validate();
            Stopwatch outerStopwatch = Stopwatch.StartNew();
            EnvironmentConfiguration environmentConfig;
            if (!(await this._rootConfigurationManager.ResolveRootConfiguration(options.Config)).Environments.TryGetValue(options.EnvironmentName, out environmentConfig))
                throw new InvalidConfigurationException("Environment " + options.EnvironmentName + " was not defined. Use the login command to define it.");
            string languages = options.Languages;
            string[] strArray;
            if (languages == null)
                strArray = (string[])null;
            else
                strArray = languages.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> languagesToPublish = (IEnumerable<string>)strArray ?? Enumerable.Empty<string>();
            string path = options.Path;
            long num = await this._contentPublisher.PublishContent(environmentConfig, languagesToPublish, path, (IEnumerable<string>)options.Targets, republish: options.Republish);
            outerStopwatch.Stop();
            ColorLogExtensions.LogConsoleVerbose(this._logger, string.Empty, new ConsoleColor?(), new ConsoleColor?());
            ColorLogExtensions.LogConsoleVerbose(this._logger, string.Format("Publishing is finished in {0}ms.", (object)outerStopwatch.ElapsedMilliseconds), new ConsoleColor?(), new ConsoleColor?());
            outerStopwatch = (Stopwatch)null;
        }
    }
}
