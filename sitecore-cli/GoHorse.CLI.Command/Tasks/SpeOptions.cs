using Sitecore.DevEx.Client.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command.Tasks
{
    public class SpeOptions : TaskOptionsBase
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
