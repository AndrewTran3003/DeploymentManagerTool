using ReleaseTool.Models;

namespace ReleaseTool.Services;

public class DefaultReleaseProcessor (IReleaseDataProcessor releaseDataProcessor) : IReleaseProcessor
{
    public async Task<List<ReleaseComponent>> GetReleasesAsync(int numberOfReleasesToKeep)
    {
        var result = new List<ReleaseComponent>();

        var flattenedReleaseCollection = await releaseDataProcessor.GetFlatennedReleaseDataAsync();
        var flattenedDataByProjectId = GroupReleaseDataByProject(flattenedReleaseCollection);
        foreach (var collectionByProjectId in flattenedDataByProjectId)
        {
            var collectionsGroupedByProjectIdAndEnvironmentId = GroupReleaseDataByEnvironment(collectionByProjectId);
            foreach (var collectionGroupedByProjectIdAndEnvironmentId in collectionsGroupedByProjectIdAndEnvironmentId)
            {
                var releasesToTake =
                    OrderByDeploymentDateAndTakeReleases(collectionGroupedByProjectIdAndEnvironmentId, numberOfReleasesToKeep);
                result.AddRange(releasesToTake);
            }
        }
        return result;
    }

    private IEnumerable<ReleaseComponent> OrderByDeploymentDateAndTakeReleases(IGrouping<string?, ReleaseDataFlattened> collection, int numberOfReleasesToKeep)
    {
        return collection
            .OrderByDescending(x => x.DeployedAt)
            .Take(numberOfReleasesToKeep)
            .Select((x, i) => new ReleaseComponent
            {
                Environment = x.EnvironmentName ?? string.Empty,
                Release = x.ReleaseId ?? string.Empty,
                Project = x.ProjectName ?? string.Empty,
                Index = i+1
            });
    }
    private IEnumerable<IGrouping<string?, ReleaseDataFlattened>> GroupReleaseDataByEnvironment(IEnumerable<ReleaseDataFlattened> source)
    {
        return source.GroupBy(x => x.EnvironmentId);
    }
    
    private IEnumerable<IGrouping<string?, ReleaseDataFlattened>> GroupReleaseDataByProject(IEnumerable<ReleaseDataFlattened> source)
    {
        return source.GroupBy(x => x.ProjectId);
    }
}