using Sitecore.DevEx.Configuration.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GoHorse.CLI.Command.Dataservices
{
    public interface IRunCommand
    {
        Task<IEnumerable<string>> RunCommandAsync(
          EnvironmentConfiguration environmentConfig, string id,
          CancellationToken cancellationToken = default(CancellationToken));
    }
}
