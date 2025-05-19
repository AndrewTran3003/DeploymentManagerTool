using ReleaseTool.Models;
using ReleaseTool.Tests.Models;
using Xunit;

namespace ReleaseTool.Tests.TestData;

public class DefaultReleaseProcessorTestData : TheoryData<string,DefaultReleaseProcessTheory, int,List<ReleaseComponent>>
{
    public DefaultReleaseProcessorTestData()
    {
        ReleasesToKeep1_ShouldReturnOneRelease();
        ReleasesToKeep1_ShouldReturnOneReleaseForEachEnvironment();
        ReleasesToKeep2_ShouldReturnTwoReleases();
        ReleasesToKeep2_ShouldReturnMaximumTwoReleasesPerEnvironmentAndProject();
    }

    private void ReleasesToKeep2_ShouldReturnMaximumTwoReleasesPerEnvironmentAndProject()
    {
        var deployments = new List<Deployment>
        {
            new()
            {
                Id = "Deployment-1-1-1",
                ReleaseId = "Release-1-1",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-09T10:00:00")
            },
            new()
            {
                Id = "Deployment-1-1-2",
                ReleaseId = "Release-1-1",
                EnvironmentId = "Environment-2",
                DeployedAt = DateTime.Parse("2000-01-08T10:00:00")
            }, 
            new()
            {
                Id = "Deployment-1-2-1",
                ReleaseId = "Release-1-2",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-08T10:00:00")
            },
            new()
            {
                Id = "Deployment-1-3-1",
                ReleaseId = "Release-1-3",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-07T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-1-1",
                ReleaseId = "Release-2-1",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-08T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-1-2",
                ReleaseId = "Release-2-1",
                EnvironmentId = "Environment-2",
                DeployedAt = DateTime.Parse("2000-01-08T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-2-1",
                ReleaseId = "Release-2-2",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-07T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-3-1",
                ReleaseId = "Release-2-3",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-06T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-3-2",
                ReleaseId = "Release-2-3",
                EnvironmentId = "Environment-2",
                DeployedAt = DateTime.Parse("2000-01-06T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-4-1",
                ReleaseId = "Release-2-4",
                EnvironmentId = "Environment-1",
                DeployedAt = DateTime.Parse("2000-01-05T10:00:00")
            },
            new()
            {
                Id = "Deployment-2-4-2",
                ReleaseId = "Release-2-4",
                EnvironmentId = "Environment-2",
                DeployedAt = DateTime.Parse("2000-01-05T10:00:00")
            }
            
        };
        var releases = new List<Release>
        {
            new()
            {
                Id = "Release-1-1",
                ProjectId = "Project-1",
                Version = "1.0.0",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-1-2",
                ProjectId = "Project-1",
                Version = "1.0.1",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-1-3",
                ProjectId = "Project-1",
                Version = "1.0.2",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-2-1",
                ProjectId = "Project-2",
                Version = "1.0.0",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-2-2",
                ProjectId = "Project-2",
                Version = "1.0.1",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-2-3",
                ProjectId = "Project-2",
                Version = "1.0.2",
                Created = DateTime.Parse("2000-01-02T09:00:00")
            },
            new()
            {
                Id = "Release-2-4",
                ProjectId = "Project-2",
                Version = "1.0.3",
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
        var theoryData = new DefaultReleaseProcessTheory
        {
            Deployments = deployments,
            Releases = releases,
            Environments = enviroments,
            Projects = projects,
        };
        var numberOfReleasesToKeep = 2;
        var expected = new List<ReleaseComponent>
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
                Environment = "Production",
                Project = "Random Quotes",
                Release = "Release-1-1",
                Index = 1
            },
            new()
            {
                Environment = "Staging",
                Project = "Pet Shop",
                Release = "Release-2-1",
                Index = 1
            },
            new()
            {
                Environment = "Staging",
                Project = "Pet Shop",
                Release = "Release-2-2",
                Index = 2
            },
            new()
            {
                Environment = "Production",
                Project = "Pet Shop",
                Release = "Release-2-1",
                Index = 1
            },
            new()
            {
                Environment = "Production",
                Project = "Pet Shop",
                Release = "Release-2-3",
                Index = 2
            }
        };
        Add(nameof(ReleasesToKeep2_ShouldReturnMaximumTwoReleasesPerEnvironmentAndProject), theoryData, numberOfReleasesToKeep, expected);
    }

    private void ReleasesToKeep1_ShouldReturnOneReleaseForEachEnvironment()
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
                EnvironmentId = "Environment-2",
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
        var theoryData = new DefaultReleaseProcessTheory
        {
            Deployments = deployments,
            Releases = releases,
            Environments = enviroments,
            Projects = projects,
        };
        var numberOfReleasesToKeep = 1;
        var expected = new List<ReleaseComponent>
        {
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1",
                Index = 1
            },
            new()
            {
                Environment = "Production",
                Project = "Random Quotes",
                Release = "Release-2",
                Index = 1
            }
        };
        Add(nameof(ReleasesToKeep1_ShouldReturnOneReleaseForEachEnvironment), theoryData, numberOfReleasesToKeep, expected);
    }
    
    private void ReleasesToKeep2_ShouldReturnTwoReleases()
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
        var theoryData = new DefaultReleaseProcessTheory
        {
            Deployments = deployments,
            Releases = releases,
            Environments = enviroments,
            Projects = projects,
        };
        var numberOfReleasesToKeep = 2;
        var expected = new List<ReleaseComponent>
        {
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1",
                Index = 1
            },
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-2",
                Index = 2
            }
        };
        Add(nameof(ReleasesToKeep2_ShouldReturnTwoReleases), theoryData, numberOfReleasesToKeep, expected);
    }

    private void ReleasesToKeep1_ShouldReturnOneRelease()
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
        var theoryData = new DefaultReleaseProcessTheory
        {
            Deployments = deployments,
            Releases = releases,
            Environments = enviroments,
            Projects = projects,
        };
        var numberOfReleasesToKeep = 1;
        var expected = new List<ReleaseComponent>
        {
            new()
            {
                Environment = "Staging",
                Project = "Random Quotes",
                Release = "Release-1",
                Index = 1
            }
        };
        Add(nameof(ReleasesToKeep1_ShouldReturnOneRelease), theoryData, numberOfReleasesToKeep, expected);
    }
}