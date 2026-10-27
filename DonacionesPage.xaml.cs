
namespace ComparteYAyuda;

public partial class DonacionesPage : ContentPage
{
    public DonacionesPage()
    {
        InitializeComponent();
    }

    private void OnBuscarChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        string busqueda = e.NewTextValue?.Trim() ?? "";

        foreach (var elemento in ListaDonaciones.Children)
        {
            if (elemento is Border tarjeta)
            {
                bool coincide = string.IsNullOrWhiteSpace(busqueda)
                    || tarjeta.Content is VerticalStackLayout contenido
                    && contenido.Children.OfType<Label>()
                        .Any(etiqueta =>
                            etiqueta.Text?.Contains(
                                busqueda,
                                StringComparison.OrdinalIgnoreCase) == true);

                tarjeta.IsVisible = coincide;
            }
        }
    }

    private async void OnMostrarInteres(
        object? sender,
        EventArgs e)
    {
        if (sender is Button boton &&
            boton.CommandParameter is string articulo)
        {
            await DisplayAlertAsync(
                "Mostrar interés",
                $"Has seleccionado: {articulo}.\n\nLa solicitud de contacto se integrará cuando se conecte la funcionalidad correspondiente.",
                "Aceptar");
        }
    }

    private async void OnVolverInicio(
        object? sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
