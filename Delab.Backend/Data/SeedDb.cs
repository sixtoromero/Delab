using Delab.AccessData.Data;
using Delab.Shared.Entities;

namespace Delab.Backend.Data;

public class SeedDb
{
    public DataContext _context { get; }
    public SeedDb(DataContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        //Valida si no existe la base de datos para crearla.
        await _context.Database.EnsureCreatedAsync();
        await CheckCountries();
    }

    private async Task CheckCountries()
    {
        //Si no existe nada en la tabla
        if (!_context.Countries.Any())
        {
            _context.Countries.Add(new Country
            {
                Name = "Colombia",
                CodPhone = "+57",
                States = new List<State>()
                {
                    new State()
                    {
                        Name = "Antioquia",
                        Cities = new    List<City>()
                        {
                            new City() { Name = "Medellín" },
                            new City() { Name = "El Bagre" },
                            new City() { Name = "Itaguí" },
                            new City() { Name = "Envigado" },
                            new City() { Name = "Bello" },
                            new City() { Name = "Rio Negro" }
                        }
                    },
                    new State()
                    {
                        Name = "Cundinamarca",
                        Cities = new List<City>()
                        {
                            new City { Name = "Soacha" },
                            new City { Name = "Facatativa" },
                            new City { Name = "Fusagasuga" },
                            new City { Name = "Chía" },
                            new City { Name = "Zipaquirá" }
                        }
                    }
                }
            });

            _context.Countries.Add(new Country
            {
                Name = "Mexico",
                CodPhone = "+56",
                States = new List<State>()
                {
                    new State()
                    {
                        Name = "Chiapas",
                        Cities = new    List<City>()
                        {
                            new City() { Name = "Tuctla" },
                            new City() { Name = "Tapachula" },
                            new City() { Name = "San Cristobal" },
                            new City() { Name = "Comitan" },
                            new City() { Name = "Cintalapa" }
                        }
                    },
                    new State()
                    {
                        Name = "Colima",
                        Cities = new List<City>()
                        {
                            new City { Name = "Manzanillo" }                            
                        }
                    }
                }
            });

            await _context.SaveChangesAsync();
        }
    }
}
