using ReleaseTool.Models;

namespace ReleaseTool.Services;

public class DefaultReleaseDataProcessor (IReleaseDataLoader dataLoader) : IReleaseDataProcessor
{
    public async Task<IEnumerable<ReleaseDataFlattened>> GetFlatennedReleaseDataAsync()
    {
        var (allDeployments, allEnvironments, allProjects, allReleases) = await GetDataAsync();
        return allDeployments
            .Join(allEnvironments, d => d.EnvironmentId, e => e.Id, (d, e) =>
                new
                {
                    EnvironmentId = e.Id,
                    EnvironmentName = e.Name,
                    d.ReleaseId,
                    d.DeployedAt
                })
            .Join(allReleases, x => x.ReleaseId, r => r.Id, (x, r) =>
                new
                {
                    x.EnvironmentName,
                    x.EnvironmentId,
                    x.DeployedAt,
                    r.ProjectId,
                    x.ReleaseId
                })
            .Join(allProjects, x => x.ProjectId, p => p.Id, (x, p) =>
                new ReleaseDataFlattened
                {
                    EnvironmentId = x.EnvironmentId,
                    EnvironmentName = x.EnvironmentName,
                    DeployedAt = x.DeployedAt,
                    ProjectId = x.ProjectId,
                    ReleaseId = x.ReleaseId,
                    ProjectName = p.Name
                });
    }
    private async Task<(IEnumerable<Deployment>, IEnumerable<Models.Environment>, IEnumerable<Project>, IEnumerable<Release>)> GetDataAsync()
    {
        var deployments = await dataLoader.LoadDeploymentsAsync();
        var environments = await dataLoader.LoadEnvironmentsAsync();
        var projects = await dataLoader.LoadProjectsAsync();
        var releases = await dataLoader.LoadReleasesAsync();
        return (deployments, environments, projects, releases);
    }
}