
namespace ComparteYAyuda;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnVerDonaciones(
        object? sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync(
            nameof(DonacionesPage));
    }

    private async void OnPublicarDonacion(
        object? sender,
        EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(
                "PublicarDonacionPage");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error al abrir el formulario",
                ex.ToString(),
                "Aceptar");
        }
    }
}
