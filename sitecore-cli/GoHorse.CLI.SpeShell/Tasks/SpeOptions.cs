using Sitecore.DevEx.Client.Tasks;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command.Tasks
{
    public class SpeOptions : TaskOptionsBase
    {
        public string Session { get; set; }
        public string ScriptId { get; set; }
        public string File { get; set; }
        public string Script { get; set; }
        public string Config { get; set; }
        public string EnvironmentName { get; set; }

        [ExcludeFromCodeCoverage]
        public List<string> Targets { get; set; }

        public override void Validate()
        {
            this.Require("Config");
            this.Default("EnvironmentName", (object)"default");
            //this.Default("SessionId", (object)"Default");
        }
    }
}
