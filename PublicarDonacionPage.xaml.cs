
using ComparteYAyuda.Data;
using ComparteYAyuda.Models;

namespace ComparteYAyuda;

public partial class PublicarDonacionPage : ContentPage
{
    private readonly DonacionDatabase _database = new();

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

        try
        {
            Donacion nuevaDonacion = new()
            {
                Articulo = ArticuloEntry.Text.Trim(),
                Categoria = CategoriaPicker.SelectedItem?.ToString() ?? "",
                Descripcion = DescripcionEditor.Text.Trim(),
                Donante = DonanteEntry.Text.Trim(),
                Contacto = ContactoEntry.Text.Trim(),
                Estado = "Disponible"
            };

            await _database.GuardarDonacionAsync(nuevaDonacion);

            await DisplayAlertAsync(
                "¡Donación publicada!",
                "Tu donación se guardó correctamente.",
                "Aceptar");

            ArticuloEntry.Text = "";
            CategoriaPicker.SelectedIndex = -1;
            DescripcionEditor.Text = "";
            DonanteEntry.Text = "";
            ContactoEntry.Text = "";
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error al guardar",
                $"No se pudo guardar la donación: {ex.Message}",
                "Aceptar");
        }
    }

    private async void OnCancelarClicked(
        object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
