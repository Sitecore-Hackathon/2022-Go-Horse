﻿// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.Commands.ListOfTargetsArgs
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using GoHorse.CLI.Command.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command.Commands
{
    [ExcludeFromCodeCoverage]
    public class RunCommandArgs : RunCommandOptions, IVerbosityArgs
    {
        //public string CommandId { get; set; }

        public bool Verbose { get; set; }

        public bool Trace { get; set; }
    }
}