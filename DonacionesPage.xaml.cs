
using ComparteYAyuda.Data;
using ComparteYAyuda.Models;

using Microsoft.Maui.Controls.Shapes;

namespace ComparteYAyuda;

public partial class DonacionesPage : ContentPage
{
    private readonly DonacionDatabase _database = new();

    public DonacionesPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarDonacionesAsync();
    }

    private async Task CargarDonacionesAsync()
    {
        try
        {
            ListaDonaciones.Children.Clear();

            List<Donacion> donaciones =
                await _database.ObtenerDonacionesDisponiblesAsync();

            if (donaciones.Count == 0)
            {
                ListaDonaciones.Children.Add(new Label
                {
                    Text = "Todavía no hay donaciones disponibles.",
                    TextColor = Color.FromArgb("#64746D"),
                    FontSize = 15,
                    HorizontalTextAlignment = TextAlignment.Center,
                    Margin = new Thickness(10, 20)
                });

                return;
            }

            foreach (Donacion donacion in donaciones)
            {
                var categoria = new Label
                {
                    Text = donacion.Categoria.ToUpper(),
                    TextColor = Color.FromArgb("#327A68"),
                    FontSize = 12,
                    FontAttributes = FontAttributes.Bold
                };

                var articulo = new Label
                {
                    Text = donacion.Articulo,
                    FontSize = 19,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromArgb("#263B35")
                };

                var descripcion = new Label
                {
                    Text = donacion.Descripcion,
                    FontSize = 14,
                    TextColor = Color.FromArgb("#64746D")
                };

                var boton = new Button
                {
                    Text = "Me interesa",
                    BackgroundColor = Color.FromArgb("#327A68"),
                    TextColor = Colors.White,
                    CornerRadius = 10,
                    CommandParameter = donacion.Id
                };

                boton.Clicked += OnMostrarInteres;

                var contenido = new VerticalStackLayout
                {
                    Spacing = 9,
                    Children =
                    {
                        categoria,
                        articulo,
                        descripcion,
                        boton
                    }
                };

                var tarjeta = new Border
                {
                    BackgroundColor = Colors.White,
                    Stroke = Color.FromArgb("#DCE9E2"),
                    StrokeThickness = 1,
                    Padding = 16,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = 16
                    },
                    Content = contenido
                };

                ListaDonaciones.Children.Add(tarjeta);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync(
                "Error",
                $"No se pudieron cargar las donaciones: {ex.Message}",
                "Aceptar");
        }
    }

    private async void OnBuscarChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        string busqueda = e.NewTextValue?.Trim() ?? "";

        foreach (var elemento in ListaDonaciones.Children)
        {
            if (elemento is Border tarjeta &&
                tarjeta.Content is VerticalStackLayout contenido)
            {
                bool coincide = string.IsNullOrWhiteSpace(busqueda)
                    || contenido.Children.OfType<Label>()
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
            boton.CommandParameter is int id)
        {
            try
            {
                List<Donacion> donaciones =
                    await _database.ObtenerDonacionesDisponiblesAsync();

                Donacion? donacion =
                    donaciones.FirstOrDefault(d => d.Id == id);

                if (donacion != null)
                {
                    await DisplayAlertAsync(
                        "Información de contacto",
                        $"Artículo: {donacion.Articulo}\n" +
                        $"Donante: {donacion.Donante}\n" +
                        $"Contacto: {donacion.Contacto}",
                        "Aceptar");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync(
                    "Error",
                    ex.Message,
                    "Aceptar");
            }
        }
    }

    private async void OnVolverInicio(
        object? sender,
        EventArgs e)
    {
        await Shell.Current.GoToAsync("//MainPage");
    }
}
