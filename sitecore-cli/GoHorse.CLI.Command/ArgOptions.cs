// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.ArgOptions
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command
{
    [ExcludeFromCodeCoverage]
    internal static class ArgOptions
    {
        internal static readonly Option Config = new Option(new string[2]
        {
      "--config",
      "-c"
        }, "Path to root sitecore.json directory (default: cwd)")
        {
            Argument = (Argument)new Argument<string>((Func<string>)(() => Environment.CurrentDirectory))
        };

        internal static readonly Option CommandId = new Option(new string[2]
        {
      "--command-id",
      "-cid"
        }, "Command ID in Sitecore to be executed")
        {
            Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
        };

        internal static readonly Option EnvironmentName = new Option(new string[2]
    {
      "--environment-name",
      "-n"
    }, "Named Sitecore environment to use. Default: 'default'.")
        {
            Argument = (Argument)new Argument<string>()
        };
        internal static readonly Option Verbose = new Option(new string[2]
        {
      "--verbose",
      "-v"
        }, "Write some additional diagnostic and performance data")
        {
            Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
        };
        internal static readonly Option Trace = new Option(new string[2]
        {
      "--trace",
      "-t"
        }, "Write more additional diagnostic and performance data")
        {
            Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
        };
        internal static readonly Option Languages = new Option(new string[2]
        {
      "--languages",
      "-l"
        }, "Comma separated list of languages to publish. Unspecified causes ALL languages to be published.")
        {
            Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
        };
        internal static readonly Option PublishPath;
        internal static readonly Option Republish;
        internal static readonly Option Targets;

        static ArgOptions()
        {
            Option option = new Option(new string[2]
            {
        "--path",
        "-p"
            }, "Sitecore Item path  or GUID to smart publish. Unspecified causes the full database to be published.");
            Argument<string> obj = new Argument<string>((Func<string>)(() => string.Empty));
            ((Argument)obj).Arity = ArgumentArity.ZeroOrOne;
            option.Argument = (Argument)obj;
            ArgOptions.PublishPath = option;
            ArgOptions.Republish = new Option(new string[2]
            {
        "--republish",
        "-r"
            }, "Use republish, smart-publish is by default")
            {
                Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
            };
            ArgOptions.Targets = new Option(new string[2]
            {
        "--targets",
        "--pt"
            }, "List of publishing targets to publish. Blank publishes to Internet (web).")
            {
                Argument = (Argument)new Argument<List<string>>((Func<List<string>>)(() => (List<string>)null))
            };
        }
    }
}
