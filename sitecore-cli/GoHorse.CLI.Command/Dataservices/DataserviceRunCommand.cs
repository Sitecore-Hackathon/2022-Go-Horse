using GraphQL.Common.Request;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DevEx;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Dataservices
{
    internal class DataserviceRunCommand : IRunCommand
    {
        private readonly Func<ISitecoreApiClient> _apiClientFactory;

        public DataserviceRunCommand(IServiceProvider serviceProvider)
        {
            AssertionExtensions.ThrowIfNull<IServiceProvider>(serviceProvider, nameof(serviceProvider));
            this._apiClientFactory = new Func<ISitecoreApiClient>((serviceProvider).GetRequiredService<ISitecoreApiClient>);
        }

        public Task<IEnumerable<string>> RunCommandAsync(EnvironmentConfiguration environmentConfig, string id, CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> dictionary = (IDictionary<string, string>)new Dictionary<string, string>()
            {
                {"id",id}
            };

            var result = this.CreateApiClient(environmentConfig).RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery($id: String){\n  runCommand(id: $id)\n }",
                Variables = (object)dictionary
            }, "runCommand", cancellationToken);

            return result;
        }

        private ISitecoreApiClient CreateApiClient(EnvironmentConfiguration environmentConfig)
        {
            ISitecoreApiClient apiClient = this._apiClientFactory();
            apiClient.Endpoint = environmentConfig;
            return apiClient;
        }

    }
}
