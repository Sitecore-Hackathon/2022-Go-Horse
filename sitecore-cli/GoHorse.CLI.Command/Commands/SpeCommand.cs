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
            ((System.CommandLine.Command)this).AddOption(ArgOptions.ScriptId);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Config);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.EnvironmentName);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Verbose);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(SpeTask task, SpeCommandArgs args)
        {
            await task.Execute(args, args.ScriptId).ConfigureAwait(false);
            return 0;
        }
    }
}
