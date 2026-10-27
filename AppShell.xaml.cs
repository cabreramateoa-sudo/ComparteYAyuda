
namespace ComparteYAyuda;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(
            "PublicarDonacionPage",
            typeof(PublicarDonacionPage));

        Routing.RegisterRoute(
            "DonacionesPage",
            typeof(DonacionesPage));
    }
}
