using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using GoHorse.CLI.Command.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command.Commands
{
    [ExcludeFromCodeCoverage]
    public class SpeCommandArgs : SpeOptions, IVerbosityArgs
    {
        public bool Verbose { get; set; }
        public bool Trace { get; set; }
    }
}
