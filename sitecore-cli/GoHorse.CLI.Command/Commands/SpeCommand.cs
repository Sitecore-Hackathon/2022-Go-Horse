using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using GoHorse.CLI.Command.Tasks;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Commands
{
    [ExcludeFromCodeCoverage]
    public class SpeCommand : SubcommandBase<SpeTask, SpeCommandArgs>
    {
        public SpeCommand(IServiceProvider container)
          : base("spe", "Executes a Powershell Script in the Sitecore instance", container)
        {
            ((System.CommandLine.Command)this).AddOption(ArgOptions.CommandId);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Verbose);
        }

        protected override async Task<int> Handle(SpeTask task, SpeCommandArgs args)
        {
            await task.Execute(args, args.CommandId).ConfigureAwait(false);
            return 0;
        }
    }
}
