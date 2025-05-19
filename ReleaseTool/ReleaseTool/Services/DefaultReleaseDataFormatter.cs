using System.Text;
using ReleaseTool.Models;

namespace ReleaseTool.Services;

public class DefaultReleaseDataFormatter : IReleaseDataFormatter
{
    public string FormatReleaseData(List<ReleaseComponent> source)
    {
        if (source.Count == 0)
        {
            return "There is no release to keep";
        }
        var result = new StringBuilder();
        foreach (var component in source)
        {
            result.AppendLine($"Release {component.Release} of project {component.Project} was kept because it was the {FormatReleaseIndex(component.Index)}most recent release to {component.Environment}");
        }
        return result.ToString();
    }

    private static string FormatReleaseIndex(int releaseNumber)
    {
        return releaseNumber switch
        {
            1 => string.Empty,
            2 => "2nd ",
            3 => "3rd ",
            _ => $"{releaseNumber}th "
        };
    }
}