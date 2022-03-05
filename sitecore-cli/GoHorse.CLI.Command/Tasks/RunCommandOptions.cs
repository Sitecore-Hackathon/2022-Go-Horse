// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Tasks.ListOfTargetsOptions
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using Sitecore.DevEx.Client.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command.Tasks
{
    public class RunCommandOptions : TaskOptionsBase
    {
        public string CommandId { get; set; }

        public string Config { get; set; }

        public string EnvironmentName { get; set; }

        [ExcludeFromCodeCoverage]
        public List<string> Targets { get; set; }

        public override void Validate()
        {
            this.Require("CommandId");
            this.Require("Config");
            this.Default("EnvironmentName", (object)"default");
        }
    }
}
