using Tools.Abstraction.Interfaces;

namespace Tools.Service;

public class TechService
{
    private readonly ITechTreeLoader _loader;

    public TechService(ITechTreeLoader loader)
    {
        _loader = loader;
    }

    public async Task ImportTechTreeAsync(string path)
    {
        await _loader.LoadFromFileAsync(path);
    }
}
