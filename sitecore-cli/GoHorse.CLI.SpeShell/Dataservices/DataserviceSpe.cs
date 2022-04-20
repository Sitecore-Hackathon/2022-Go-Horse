using GraphQL.Common.Request;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DevEx;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoHorse.CLI.SpeShell.Dataservices
{
    internal class DataserviceSpe : ISpe
    {
        private readonly Func<ISitecoreApiClient> _apiClientFactory;

        public DataserviceSpe(IServiceProvider serviceProvider)
        {
            serviceProvider.ThrowIfNull(nameof(serviceProvider));
            _apiClientFactory = new Func<ISitecoreApiClient>(serviceProvider.GetRequiredService<ISitecoreApiClient>);
        }

        public Task<IEnumerable<string>> SpeIdAsync(EnvironmentConfiguration environmentConfig, string id, string sessionId, CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {"id",id},
                {"sessionId",sessionId}
            };

            var result = CreateApiClient(environmentConfig).RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery($id: String, $sessionId: String){\n  runScriptId(id: $id, sessionId: $sessionId)\n }",
                Variables = (object)dictionary
            }, "runScriptId", cancellationToken);

            return result;
        }

        public Task<IEnumerable<string>> SpeInlineAsync(EnvironmentConfiguration environmentConfig, string script, string sessionId, CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {"sessionId",sessionId},
                {"script",script}
            };

            var result = CreateApiClient(environmentConfig).RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery($sessionId: String, $script: String){\n  runScriptInline(sessionId: $sessionId, script: $script)\n }",
                Variables = (object)dictionary
            }, "runScriptInline", cancellationToken);

            return result;
        }

        private ISitecoreApiClient CreateApiClient(EnvironmentConfiguration environmentConfig)
        {
            ISitecoreApiClient apiClient = _apiClientFactory();
            apiClient.Endpoint = environmentConfig;
            return apiClient;
        }
    }
}
