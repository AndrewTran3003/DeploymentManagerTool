using ReleaseTool.Models;
using Xunit;

namespace ReleaseTool.Tests.TestData;

public class DefaultDataLoaderTestData : TheoryData<string, string, List<Deployment>>
{
    public DefaultDataLoaderTestData()
    {
        ShouldReturnData();
        WhenExceptionOccurs_ShouldReturnEmptyCollection();
    }

    private void WhenExceptionOccurs_ShouldReturnEmptyCollection()
    {
       Add(nameof(WhenExceptionOccurs_ShouldReturnEmptyCollection), string.Empty, new List<Deployment>());
    }
    private void ShouldReturnData()
    {
         var expected = new List<Deployment>
        {
            new()
            {
                Id = "Deployment-1",
                ReleaseId = "Release-1",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-01T10:00:00")
            },
            new()
            {
                Id = "Deployment-2",
                ReleaseId = "Release-2",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-02T10:00:00")
            },
            new()
            {
                Id = "Deployment-3",
                ReleaseId = "Release-1",
                EnvironmentId = "Environment-2",
                DeployedAt = DateTime.Parse("2000-01-02T11:00:00")
            },
            new()
            {
                Id = "Deployment-4",
                ReleaseId = "Release-2",
                EnvironmentId = "Environment-3",
                DeployedAt = DateTime.Parse("2000-01-02T12:00:00")
            },
            new()
            {
                Id = "Deployment-5",
                ReleaseId = "Release-5",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-01T11:00:00")
            },
            new()
            {
                Id = "Deployment-6",
                ReleaseId = "Release-6",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-02T10:00:00")
            },
            new()
            {
                Id = "Deployment-7",
                ReleaseId = "Release-6",
                EnvironmentId = "Environment-2",
                DeployedAt = DateTime.Parse("2000-01-02T11:00:00")
            },
            new()
            {
                Id = "Deployment-8",
                ReleaseId = "Release-7",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-02T13:00:00")
            },
            new()
            {
                Id = "Deployment-9",
                ReleaseId = "Release-6",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-02T14:00:00")
            },
            new()
            {
                Id = "Deployment-10",
                ReleaseId = "Release-8",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-01T10:00:00")
            }
        };
        Add(nameof(ShouldReturnData), "TestData/Deployments.json", expected);
    }
}