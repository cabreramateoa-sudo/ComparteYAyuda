using SQLite;

namespace ComparteYAyuda.Models;

[Table("Donaciones")]
public class Donacion
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Articulo { get; set; } = string.Empty;

    [NotNull]
    public string Categoria { get; set; } = string.Empty;

    public string Descripcion { get; set; } = string.Empty;

    public string Donante { get; set; } = string.Empty;

    public string Contacto { get; set; } = string.Empty;

    public string Estado { get; set; } = "Disponible";
}