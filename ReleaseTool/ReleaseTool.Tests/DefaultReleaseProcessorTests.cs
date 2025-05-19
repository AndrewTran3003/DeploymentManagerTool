using FluentAssertions;
using Moq;
using ReleaseTool.Models;
using ReleaseTool.Services;
using ReleaseTool.Tests.Models;
using ReleaseTool.Tests.TestData;
using Xunit;

namespace ReleaseTool.Tests;

public class DefaultReleaseProcessorTests
{
    [Theory]
    [ClassData(typeof(DefaultReleaseProcessorTestData))]
    public async Task GetReleases_OnGet_ShouldReturnReleases(string _, DefaultReleaseProcessTheory theoryData, int numberOfReleases, List<ReleaseComponent> expectedComponents)
    {
        // Arrange
        var mockDataLoader = new Mock<IReleaseDataLoader>();
        mockDataLoader.Setup(x => x.LoadDeploymentsAsync())
            .ReturnsAsync(theoryData.Deployments!);
        mockDataLoader.Setup(x => x.LoadEnvironmentsAsync())
            .ReturnsAsync(theoryData.Environments!);
        mockDataLoader.Setup(x => x.LoadProjectsAsync())
            .ReturnsAsync(theoryData.Projects!);
        mockDataLoader.Setup(x => x.LoadReleasesAsync())
            .ReturnsAsync(theoryData.Releases!);
        var mockDataProcessor = new DefaultReleaseDataProcessor(mockDataLoader.Object);
        var sut = new DefaultReleaseProcessor(mockDataProcessor);

        // Act
        var actual = await sut.GetReleasesAsync(numberOfReleases);

        // Assert
        actual.Should().BeEquivalentTo(expectedComponents);
    }
}