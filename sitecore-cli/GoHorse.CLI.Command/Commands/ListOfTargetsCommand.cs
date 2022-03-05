﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Commands.ListOfTargetsCommand
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
    public class ListOfTargetsCommand : SubcommandBase<ListOfTargetsTask, ListOfTargetsArgs>
    {
        public ListOfTargetsCommand(IServiceProvider container)
          : base("list-targets", "Gets list of publishing targets", container)
        {
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Config);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.EnvironmentName);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Verbose);
            ((System.CommandLine.Command)this).AddOption(ArgOptions.Trace);
        }

        protected override async Task<int> Handle(ListOfTargetsTask task, ListOfTargetsArgs args)
        {
            await task.Execute((ListOfTargetsOptions)args).ConfigureAwait(false);
            return 0;
        }
    }
}