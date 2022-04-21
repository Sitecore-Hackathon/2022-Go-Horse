using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System.Diagnostics.CodeAnalysis;
using GoHorse.CLI.SpeShell.Tasks;

namespace GoHorse.CLI.SpeSh.Commands
{
    [ExcludeFromCodeCoverage]
    public class SpeCommandArgs : SpeOptions, IVerbosityArgs
    {
        public bool Verbose { get; set; }
        public bool Trace { get; set; }
    }
}
