namespace ReleaseTool.Models;

public class ReleaseDataFlattened
{
    public string? EnvironmentId { get; set; }
    public string? EnvironmentName { get; set; }
    public DateTime DeployedAt { get; set; }
    public string? ProjectId { get; set; }
    public string? ReleaseId { get; set; }
    public string? ProjectName { get; set; }
}