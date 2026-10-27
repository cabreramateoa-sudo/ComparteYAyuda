
namespace ComparteYAyuda;

public partial class PublicarDonacionPage : ContentPage
{
    public PublicarDonacionPage()
    {
        InitializeComponent();
    }

    private async void OnPublicarClicked(
        object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(ArticuloEntry.Text) ||
            CategoriaPicker.SelectedIndex == -1 ||
            string.IsNullOrWhiteSpace(DescripcionEditor.Text) ||
            string.IsNullOrWhiteSpace(DonanteEntry.Text) ||
            string.IsNullOrWhiteSpace(ContactoEntry.Text))
        {
            await DisplayAlertAsync(
                "Campos incompletos",
                "Completa todos los campos antes de continuar.",
                "Aceptar");
            return;
        }

        await DisplayAlertAsync(
            "Formulario completado",
            "Los datos se validaron correctamente. El guardado definitivo se integrará con la base de datos del proyecto.",
            "Aceptar");
    }

    private async void OnCancelarClicked(
        object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
