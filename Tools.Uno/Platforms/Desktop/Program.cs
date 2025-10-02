using Tools.Uno;
using Uno.UI.Hosting;

namespace Tools;

internal class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        UnoPlatformHost host = UnoPlatformHostBuilder.Create().App(() => new App()).UseX11().UseLinuxFrameBuffer()
            .UseMacOS().UseWin32().Build();

        host.Run();
    }
}
