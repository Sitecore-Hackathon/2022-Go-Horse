using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command
{
    [ExcludeFromCodeCoverage]
    internal static class ArgOptions
    {
        internal static readonly Option CommandId = 
            new Option(new string[2]{ "--command-id", "-cid" }, "Powershell script ID in Sitecore to be executed"){
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
