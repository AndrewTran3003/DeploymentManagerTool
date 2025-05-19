using FluentAssertions;
using Moq;
using ReleaseTool.Models;
using ReleaseTool.Services;
using ReleaseTool.Tests.TestData;
using Xunit;

namespace ReleaseTool.Tests;

public class DefaultReleaseDataProcessorTests
{
    [Theory]
    [ClassData(typeof(DefaultReleaseDataProcessorTestData))]
    public async Task GetFlatennedReleaseData_OnGet_ShouldReturnFlattenedData(string _, List<Release> releases, List<Project> projects, List<ReleaseTool.Models.Environment> environments, List<Deployment> deployments, List<ReleaseDataFlattened> expected)
    {
        // Arrange
        var mockDefaultReleaseDataLoader = new Mock<IReleaseDataLoader>();
        mockDefaultReleaseDataLoader.Setup(x => x.LoadDeploymentsAsync())
            .ReturnsAsync(deployments);
        mockDefaultReleaseDataLoader.Setup(x => x.LoadEnvironmentsAsync())
            .ReturnsAsync(environments);
        mockDefaultReleaseDataLoader.Setup(x => x.LoadProjectsAsync())
            .ReturnsAsync(projects);
        mockDefaultReleaseDataLoader.Setup(x => x.LoadReleasesAsync())
            .ReturnsAsync(releases);
        
        var sut = new DefaultReleaseDataProcessor(mockDefaultReleaseDataLoader.Object);
        
        // Act
        var actual = await sut.GetFlatennedReleaseDataAsync();
        
        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}