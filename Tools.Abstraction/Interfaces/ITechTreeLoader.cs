namespace Tools.Abstraction.Interfaces;

public interface ITechTreeLoader
{
    Task LoadFromFileAsync(string xmlPath);
}
