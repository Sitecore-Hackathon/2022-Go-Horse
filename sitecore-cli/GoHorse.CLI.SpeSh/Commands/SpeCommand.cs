using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GoHorse.CLI.SpeSh.Tasks;

namespace GoHorse.CLI.SpeSh.Commands
{
    [ExcludeFromCodeCoverage]
    public class SpeCommand : SubcommandBase<SpeTask, SpeCommandArgs>
    {
        public SpeCommand(IServiceProvider container)
          : base("spe", "Executes a Powershell Script in the Sitecore instance", container)
        {
            AddOption(ArgOptions.ScriptId);
            AddOption(ArgOptions.Script);
            AddOption(ArgOptions.Session);
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
            else
                task.LogConsoleInformation("You must pass either (--script, -s), (--file, -f) or (--script-id, -sid)");
            return 0;
        }
    }
}
