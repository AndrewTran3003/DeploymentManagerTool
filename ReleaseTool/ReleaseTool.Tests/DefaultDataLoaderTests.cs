using FluentAssertions;
using Moq;
using ReleaseTool.Models;
using ReleaseTool.Services;
using ReleaseTool.Tests.TestData;
using Xunit;

namespace ReleaseTool.Tests;

public class DefaultDataLoaderTests
{
    [Theory]
    [ClassData(typeof(DefaultDataLoaderTestData))]
    public async Task LoadCollection_OnLoad_ShouldReturnData(string _, string path, List<Deployment> expected)
    {
        // Arrange
        var sut = new DefaultDataLoader();
        
        // Act
        var actual = await sut.LoadCollectionAsync<Deployment>(path);
    
        // Assert
        actual.Should().BeEquivalentTo(expected);
    }
}