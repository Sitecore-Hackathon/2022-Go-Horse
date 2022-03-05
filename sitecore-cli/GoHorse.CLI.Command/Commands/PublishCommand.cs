// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Commands.PublishCommand
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using GoHorse.CLI.Command.Tasks;
using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Commands
{
    [ExcludeFromCodeCoverage]
    public class PublishCommand : SubcommandBase<PublishTask, PublishArgs>
    {
        public PublishCommand(IServiceProvider container)
          : base("gohorse", "Performs a GoHorse publish operation on all content", container)
        {
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Config);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.EnvironmentName);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Verbose);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Trace);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Languages);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.PublishPath);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Republish);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Targets);
        }

        protected override async Task<int> Handle(PublishTask task, PublishArgs args)
        {
            await task.Execute((PublishTaskOptions)args).ConfigureAwait(false);
            return 0;
        }
    }
}
