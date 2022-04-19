using System;
using System.CommandLine;
using System.Diagnostics.CodeAnalysis;

namespace GoHorse.CLI.Command
{
    [ExcludeFromCodeCoverage]
    internal static class ArgOptions
    {
        internal static readonly Option Config =
            new Option(new string[2]{"--config","-c"}, "Path to root sitecore.json directory (default: cwd)")
            {
                Argument = (Argument)new Argument<string>((Func<string>)(() => Environment.CurrentDirectory))
            };

        internal static readonly Option ScriptId =
            new Option(new string[2] { "--script-id", "-sid" }, "Powershell script ID (or path) in Sitecore to be executed")
            {
                Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
            };

        internal static readonly Option Script =
            new Option(new string[2] { "--script", "-s" }, "Powershell script inline command to be executed")
            {
                Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
            };

        internal static readonly Option File =
            new Option(new string[2] { "--file", "-f" }, "Local Powershell script file to be executed remotely")
            {
                Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
            };

        internal static readonly Option Session =
            new Option(new string[2] { "--session", "-ses" }, "Session Id to be used as the Powershell session (default: 'Default')")
            {
                Argument = (Argument)new Argument<string>((Func<string>)(() => string.Empty))
            };

        internal static readonly Option Verbose =
            new Option(new string[2] { "--verbose", "-v" }, "Write some additional diagnostic and performance data")
            {
                Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
            };

        internal static readonly Option Trace = 
            new Option(new string[2] { "--trace", "-t" }, "Write more additional diagnostic and performance data")
            {
                Argument = (Argument)new Argument<bool>((Func<bool>)(() => false))
            };

        internal static readonly Option EnvironmentName = 
            new Option(new string[2] { "--environment-name", "-n" }, "Named Sitecore environment to use. Default: 'default'.")
            {
                Argument = (Argument)new Argument<string>()
            };


        static ArgOptions()
        {
        }
    }
}
