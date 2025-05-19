using ReleaseTool.Models;
using Environment = ReleaseTool.Models.Environment;

namespace ReleaseTool.Tests.Models;

public class DefaultReleaseProcessTheory
{
    public List<Deployment>? Deployments { get; set; }
    public List<Release>? Releases { get; set; }
    public List<Project>? Projects { get; set; }
    public List<Environment>? Environments { get; set; }
}