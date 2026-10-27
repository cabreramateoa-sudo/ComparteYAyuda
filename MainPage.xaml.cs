
namespace ComparteYAyuda;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnVerDonaciones(
        object? sender, EventArgs e)
    {
        await DisplayAlertAsync(
            "Donaciones disponibles",
            "Aquí estará la lista de donaciones del proyecto.",
            "Aceptar");
    }

    private async void OnPublicarDonacion(
        object? sender, EventArgs e)
    {
        try
        {
            await Shell.Current.GoToAsync(
                nameof(PublicarDonacionPage));
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
