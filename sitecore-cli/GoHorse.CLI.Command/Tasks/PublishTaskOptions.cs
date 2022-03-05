// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Tasks.PublishTaskOptions
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using Sitecore.DevEx.Client.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace GoHorse.CLI.Command.Tasks
{
    public class PublishTaskOptions : TaskOptionsBase
    {
        public string Config { get; set; }

        public string EnvironmentName { get; set; }

        public string Languages { get; set; }

        public string Path { get; set; }

        public bool Republish { get; set; }

        [ExcludeFromCodeCoverage]
        public List<string> Targets { get; set; }

        public override void Validate()
        {
            this.Require("Config");
            this.Default("EnvironmentName", (object)"default");
            this.ValidatePublishPath(this.Path);
            this.ValidateTargets((IEnumerable<string>)this.Targets);
        }

        private void ValidatePublishPath(string publishPath)
        {
            if (!string.IsNullOrWhiteSpace(publishPath) && !publishPath.StartsWith("/sitecore", StringComparison.OrdinalIgnoreCase) && !Guid.TryParse(publishPath, out Guid _))
                throw new TaskValidationException("Option Publish path does not have a valid format.");
        }

        private void ValidateTargets(IEnumerable<string> targets)
        {
            if (targets == null || !targets.Any<string>())
                return;
            List<string> list = targets.GroupBy<string, string>((Func<string, string>)(x => x)).Where<IGrouping<string, string>>((Func<IGrouping<string, string>, bool>)(group => group.Count<string>() > 1)).Select<IGrouping<string, string>, string>((Func<IGrouping<string, string>, string>)(group => group.Key)).ToList<string>();
            if (list.Any<string>())
                throw new TaskValidationException("Option Targets contains duplicates for " + string.Join(", ", (IEnumerable<string>)list));
        }
    }
}
