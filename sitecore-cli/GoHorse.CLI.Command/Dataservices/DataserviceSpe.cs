using GraphQL.Common.Request;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DevEx;
using Sitecore.DevEx.Configuration.Models;
using Sitecore.DevEx.Serialization.Client.Datasources.Sc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Dataservices
{
    internal class DataserviceSpe : ISpe
    {
        private readonly Func<ISitecoreApiClient> _apiClientFactory;

        public DataserviceSpe(IServiceProvider serviceProvider)
        {
            AssertionExtensions.ThrowIfNull<IServiceProvider>(serviceProvider, nameof(serviceProvider));
            this._apiClientFactory = new Func<ISitecoreApiClient>((serviceProvider).GetRequiredService<ISitecoreApiClient>);
        }

        public Task<IEnumerable<string>> SpeIdAsync(EnvironmentConfiguration environmentConfig, string id, string sessionId, CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> dictionary = (IDictionary<string, string>)new Dictionary<string, string>()
            {
                {"id",id},
                {"sessionId",sessionId}
            };

            var result = this.CreateApiClient(environmentConfig).RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery($id: String, $sessionId: String){\n  runScriptId(id: $id, sessionId: $sessionId)\n }",
                Variables = (object)dictionary
            }, "runScriptId", cancellationToken);

            return result;
        }

        public Task<IEnumerable<string>> SpeInlineAsync(EnvironmentConfiguration environmentConfig, string script, string sessionId, CancellationToken cancellationToken = default)
        {
            IDictionary<string, string> dictionary = (IDictionary<string, string>)new Dictionary<string, string>()
            {
                {"script",script},
                {"sessionId",sessionId}
            };

            var result = this.CreateApiClient(environmentConfig).RunQuery<IEnumerable<string>>("/sitecore/api/management", new GraphQLRequest()
            {
                Query = "\nquery($script: String, $sessionId: String){\n  runScriptInline(script: $script, sessionId: $sessionId)\n }",
                Variables = (object)dictionary
            }, "runScriptInline", cancellationToken);

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
