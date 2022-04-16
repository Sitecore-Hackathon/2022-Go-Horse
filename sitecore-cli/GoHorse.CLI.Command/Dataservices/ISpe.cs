using Sitecore.DevEx.Configuration.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Dataservices
{
    public interface ISpe
    {
        Task<IEnumerable<string>> SpeAsync(
          EnvironmentConfiguration environmentConfig, string id,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
