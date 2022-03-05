// Decompiled with JetBrains decompiler
// Type: Sitecore.DevEx.Extensibility.Publishing.RegisterExtension
// Assembly: Sitecore.DevEx.Extensibility.Publishing, Version=4.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 8AB531F3-111F-41B9-B846-117170FE660B
// Assembly location: C:\Users\RodrigoPeplau\Desktop\Sitecore.DevEx.Extensibility.Publishing.4.1.1\plugin\Sitecore.DevEx.Extensibility.Publishing.dll

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using GoHorse.CLI.Command.Commands;
using Sitecore.DevEx.Serialization.Client;
using System;
using System.Collections.Generic;
using System.CommandLine;
using GoHorse.CLI.Command.Dataservices;

namespace GoHorse.CLI.Command
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container)
        {
            var requiredService = container.GetRequiredService<PublishCommand>();
            requiredService.AddCommand(container.GetRequiredService<ListOfTargetsCommand>());
            requiredService.AddCommand(container.GetRequiredService<RunCommandCommand>());
            return (IEnumerable<ISubcommand>)new ISubcommand[1]
            {
        (ISubcommand) requiredService
            };
        }

        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSerialization().AddSingleton<PublishCommand>()
                .AddSingleton<ListOfTargetsCommand>()
                .AddSingleton<IRunCommand,DataserviceRunCommand>()
                .AddSingleton<RunCommandCommand>();
        }


    }
}
