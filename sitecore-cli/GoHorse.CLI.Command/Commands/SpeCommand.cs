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
            AddOption(ArgOptions.ScriptId);
            AddOption(ArgOptions.Script);
            AddOption(ArgOptions.SessionId);
            AddOption(ArgOptions.File);
            AddOption(ArgOptions.Config);
            AddOption(ArgOptions.EnvironmentName);
            AddOption(ArgOptions.Verbose);
            AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(SpeTask task, SpeCommandArgs args)
        {
            if (!string.IsNullOrEmpty(args.ScriptId))
                await task.Execute(args, args.ScriptId).ConfigureAwait(false);
            else if (!string.IsNullOrEmpty(args.Script))
                await task.ExecuteInline(args, args.Script).ConfigureAwait(false);
            else if (!string.IsNullOrEmpty(args.File))
                await task.ExecuteFile(args, args.File).ConfigureAwait(false);
            return 0;
        }
    }
}
