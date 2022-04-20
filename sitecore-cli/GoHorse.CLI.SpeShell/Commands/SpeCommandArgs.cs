using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.Diagnostics.CodeAnalysis;
using GoHorse.CLI.SpeShell.Tasks;

namespace GoHorse.CLI.SpeShell.Commands
{
    [ExcludeFromCodeCoverage]
    public class SpeCommandArgs : SpeOptions, IVerbosityArgs
    {
        public bool Verbose { get; set; }
        public bool Trace { get; set; }
    }
}
