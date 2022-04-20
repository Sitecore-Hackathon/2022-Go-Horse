using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.Devex.Client.Cli.Extensibility;
using Sitecore.Devex.Client.Cli.Extensibility.Subcommands;
using GoHorse.CLI.Command.Commands;
using Sitecore.DevEx.Serialization.Client;
using System;
using System.Collections.Generic;
using GoHorse.CLI.Command.Dataservices;

namespace GoHorse.CLI.Command
{
    public class RegisterExtension : ISitecoreCliExtension
    {
        public IEnumerable<ISubcommand> AddCommands(IServiceProvider container)
        {
            var requiredService = container.GetRequiredService<SpeCommand>();
            //requiredService.AddCommand(container.GetRequiredService<RunCommandCommand>());
            return new ISubcommand[1] { requiredService };
        }

        public void AddConfiguration(IConfigurationBuilder configBuilder)
        {
        }

        public void AddServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSerialization()
                .AddSingleton<ISpe, DataserviceSpe>()
                .AddSingleton<SpeCommand>();
        }
    }
}