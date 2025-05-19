using ReleaseTool.Models;
using Xunit;

namespace ReleaseTool.Tests.TestData;

public class DefaultReleaseDataProcessorTestData: TheoryData<string, List<Release>, List<Project>, List<ReleaseTool.Models.Environment>, List<Deployment>, List<ReleaseDataFlattened>>
{
    public DefaultReleaseDataProcessorTestData()
    {
        ShouldReturnFlattenedData();
    }
    private void ShouldReturnFlattenedData()
    {
        var deployments = new List<Deployment>
        {
            new()
            {
                Id = "Deployment-1",
                ReleaseId = "Release-1",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-08T10:00:00")
            },
            new()
            {
                Id = "Deployment-2",
                ReleaseId = "Release-2",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-02T10:00:00")
            }
        };
        var releases = new List<Release>
        {
            new()
            {
                Id = "Release-1",
                ProjectId = "Project-1",
                Version = "1.0.0",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-2",
                ProjectId = "Project-1",
                Version = "1.0.1",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            }
        };
        var enviroments = new List<ReleaseTool.Models.Environment>
        {
            new()
            {
                Id = "Environment-1",
                Name = "Staging"
            },
            new()
            {
                Id = "Environment-2",
                Name = "Production"
            }
        };
        var projects = new List<Project>
        {
            new()
            {
                Id = "Project-1",
                Name = "Random Quotes"
            },
            new()
            {
                Id = "Project-2",
                Name = "Pet Shop"
            }
        };
        var expected = new List<ReleaseDataFlattened>
        {
            new()
            {
                EnvironmentId = "Environment-1",
                EnvironmentName = "Staging",
                DeployedAt = DateTime.Parse("2000-01-08T10:00:00"),
                ProjectId = "Project-1",
                ReleaseId = "Release-1",
                ProjectName = "Random Quotes"
            },
            new()
            {
                EnvironmentId = "Environment-1",
                EnvironmentName = "Staging",
                DeployedAt = DateTime.Parse("2000-01-02T10:00:00"),
                ProjectId = "Project-1",
                ReleaseId = "Release-2",
                ProjectName = "Random Quotes"
            }
        };
        Add(nameof(ShouldReturnFlattenedData), releases, projects, enviroments,deployments, expected);

    }
}