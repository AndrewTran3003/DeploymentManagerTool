using ReleaseTool.Models;
using Xunit;

namespace ReleaseTool.Tests.TestData;

public class DefaultReleaseDataFormatterTestData: TheoryData<string, List<ReleaseComponent>, string>
{
    public DefaultReleaseDataFormatterTestData()
    {
        ShouldReturnWhenThereIsOneRelease();
        ShouldReturnWhenThereAreMultipleReleases();
        ShouldReturnWhenThereIsNoRelease();
    }

    private void ShouldReturnWhenThereIsOneRelease()
    {
        var input = new List<ReleaseComponent>
        {
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1",
                Index = 1
            }
        };

        var expected =
            "Release Release-1 of project Random Quotes was kept because it was the most recent release to Staging\n";
        Add(nameof(ShouldReturnWhenThereIsOneRelease), input, expected);
    }

    private void ShouldReturnWhenThereAreMultipleReleases()
    {
        var input = new List<ReleaseComponent>
        {
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-1",
                Index = 1
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-2",
                Index = 2
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-3",
                Index = 3
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-4",
                Index = 4
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-5",
                Index = 5
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-6",
                Index = 6
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1-7",
                Index = 7
            },
        };

        var expect =
            @"Release Release-1-1 of project Random Quotes was kept because it was the most recent release to Staging
Release Release-1-2 of project Random Quotes was kept because it was the 2nd most recent release to Staging
Release Release-1-3 of project Random Quotes was kept because it was the 3rd most recent release to Staging
Release Release-1-4 of project Random Quotes was kept because it was the 4th most recent release to Staging
Release Release-1-5 of project Random Quotes was kept because it was the 5th most recent release to Staging
Release Release-1-6 of project Random Quotes was kept because it was the 6th most recent release to Staging
Release Release-1-7 of project Random Quotes was kept because it was the 7th most recent release to Staging
";
        Add(nameof(ShouldReturnWhenThereAreMultipleReleases), input, expect);
    }

    private void ShouldReturnWhenThereIsNoRelease()
    {
        var input = new List<ReleaseComponent>();
        var expected = "There is no release to keep";
        Add(nameof(ShouldReturnWhenThereIsNoRelease), input, expected);
    }
}