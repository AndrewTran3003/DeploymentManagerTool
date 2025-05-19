using ReleaseTool.Models;

namespace ReleaseTool.Services;

public interface IReleaseDataFormatter
{
    public string FormatReleaseData(List<ReleaseComponent> source);
}