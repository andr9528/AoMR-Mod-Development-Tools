namespace Tools.Abstraction.Interfaces.Services;

public interface IXmlLoader
{
    Task LoadFromFileAsync(string xmlPath);
}
