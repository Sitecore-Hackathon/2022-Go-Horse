using Sitecore.DevEx.Configuration.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Dataservices
{
    public interface ISpe
    {
        Task<IEnumerable<string>> SpeIdAsync(
          EnvironmentConfiguration environmentConfig, string id, string sessionId,
          CancellationToken cancellationToken = default(CancellationToken));
        Task<IEnumerable<string>> SpeInlineAsync(
          EnvironmentConfiguration environmentConfig, string script, string sessionId,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
