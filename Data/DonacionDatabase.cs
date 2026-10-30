using SQLite;
using ComparteYAyuda.Models;

namespace ComparteYAyuda.Data;

public class DonacionDatabase
{
    private readonly SQLiteAsyncConnection _database;

    public DonacionDatabase()
    {
        string ruta = Path.Combine(
            FileSystem.AppDataDirectory,
            "donaciones.db3");

        _database = new SQLiteAsyncConnection(ruta);
    }

    public async Task InicializarAsync()
    {
        await _database.CreateTableAsync<Donacion>();
    }

    public async Task<int> GuardarDonacionAsync(Donacion donacion)
    {
        await InicializarAsync();
        return await _database.InsertAsync(donacion);
    }

    public async Task<List<Donacion>> ObtenerDonacionesDisponiblesAsync()
    {
        await InicializarAsync();

        return await _database.Table<Donacion>()
            .Where(d => d.Estado == "Disponible")
            .OrderByDescending(d => d.Id)
            .ToListAsync();
    }
}