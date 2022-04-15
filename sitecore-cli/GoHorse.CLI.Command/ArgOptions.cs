// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.ArgOptions
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command
{
    [ExcludeFromCodeCoverage]
    internal static class ArgOptions
    {
        internal static readonly Option CommandId = 
            new Option(new string[2]{ "--command-id", "-cid" }, "Command ID in Sitecore to be executed"){
                Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
            };

        internal static readonly Option Verbose = 
            new Option(new string[2]{ "--verbose", "-v" }, "Write some additional diagnostic and performance data"){
                Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
            };

        static ArgOptions()
        {
        }
    }
}
