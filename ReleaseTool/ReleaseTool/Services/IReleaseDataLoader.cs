using ReleaseTool.Models;

namespace ReleaseTool.Services;

public interface IReleaseDataLoader
{
    public Task<IEnumerable<Deployment>> LoadDeploymentsAsync();
    public Task<IEnumerable<Models.Environment>> LoadEnvironmentsAsync();
    public Task<IEnumerable<Project>> LoadProjectsAsync();
    public Task<IEnumerable<Release>> LoadReleasesAsync();
    
}