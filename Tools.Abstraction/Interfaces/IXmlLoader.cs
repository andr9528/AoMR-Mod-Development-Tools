namespace Tools.Abstraction.Interfaces;

public interface IXmlLoader
{
    Task LoadFromFileAsync(string xmlPath);
}
