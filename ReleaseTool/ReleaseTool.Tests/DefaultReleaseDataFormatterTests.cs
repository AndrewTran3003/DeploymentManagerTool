using FluentAssertions;
using ReleaseTool.Models;
using ReleaseTool.Services;
using ReleaseTool.Tests.TestData;
using Xunit;

namespace ReleaseTool.Tests;

public class DefaultReleaseDataFormatterTests
{
    [Theory]
    [ClassData(typeof(DefaultReleaseDataFormatterTestData))]
    public void FormatReleaseData_OnGet_ShouldReturnMessage(string _, List<ReleaseComponent> input, string expected)
    {
        // Arrange
         var sut = new DefaultReleaseDataFormatter();
         
         //Act
         var actual = sut.FormatReleaseData(input);
         
         // Assert
         actual.Should().Be(expected);
    }
}