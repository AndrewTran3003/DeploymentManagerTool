using ReleaseTool.Models;
using Environment = ReleaseTool.Models.Environment;

namespace ReleaseTool.Services;

public class DefaultReleaseDataLoader (IDataLoader dataLoader): IReleaseDataLoader
{
    public async Task<IEnumerable<Deployment>> LoadDeploymentsAsync()
    {
        return await dataLoader.LoadCollectionAsync<Deployment>("/Data/Deployments.json");
    }

    public async Task<IEnumerable<Environment>> LoadEnvironmentsAsync()
    {
        return await dataLoader.LoadCollectionAsync<Environment>("/Data/Environments.json");
    }

    public async Task<IEnumerable<Project>> LoadProjectsAsync()
    {
        return await dataLoader.LoadCollectionAsync<Project>("/Data/Projects.json");
    }

    public async Task<IEnumerable<Release>> LoadReleasesAsync()
    {
        return await dataLoader.LoadCollectionAsync<Release>("/Data/Releases.json");
    }
}