namespace ReleaseTool.Models;

public class ReleaseComponent
{
    public string Release { get; set; } = string.Empty;
    public string Project { get; set; } = string.Empty;
    public string Environment { get; set; } = string.Empty;
    public int Index { get; set; }
}