using ReleaseTool.Models;

namespace ReleaseTool.Services;

public interface IReleaseProcessor
{
    public Task<List<ReleaseComponent>> GetReleasesAsync(int numberOfReleasesToKeep);
}