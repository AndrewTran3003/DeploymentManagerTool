using ReleaseTool.Models;

namespace ReleaseTool.Services;

public interface IReleaseDataProcessor
{
    public Task<IEnumerable<ReleaseDataFlattened>> GetFlatennedReleaseDataAsync();
}