using Delab.AccessData.Data;
using Delab.Helpers;
using Delab.Shared.Entities;
using Delab.Shared.Enum;

namespace Delab.Backend.Data;

public class SeedDb
{
    public readonly DataContext _context;
    public readonly IUserHelper _userHelper;

    public SeedDb(DataContext context, IUserHelper userHelper)
    {
        _context = context;
        _userHelper = userHelper;
    }

    public async Task SeedAsync()
    {
        //Valida si no existe la base de datos para crearla.
        await _context.Database.EnsureCreatedAsync();
        await CheckCountries();
        await CheckRolesAsync();
        await CheckUserAsync("Nexxtplanet", "SPI", "soporte@nexxtplanet.net", "+1 786 503", UserType.ADMIN);
    }

    private async Task CheckUserAsync(string firstName, string lastName, string email, string phone, UserType userType)
    {
        User user = await _userHelper.GetUserAsync(email);
        if (user == null)
        {
            user = new()
            {
                FirstName = firstName,
                LastName = lastName,
                FullName = $"{firstName} {lastName}",
                Email = email,
                UserName = email,
                PhoneNumber = phone,
                JobPosition = "Administrador",
                UserFrom = "SeedDb",
                UserRoleDetails = new List<UserRoleDetails> { new UserRoleDetails { UserType = userType } },
                Active = true,
            };

            await _userHelper.AddUserAsync(user, "123456");
            await _userHelper.AddUserToRoleAsync(user, userType.ToString());

            // Para Confirmar automaticamente el Usuario y activar la cuenta
            string token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
            await _userHelper.ConfirmEmailAsync(user, token);
            await _userHelper.AddUserClaims(userType, email);
        }

    }

    private async Task CheckRolesAsync()
    {
        await _userHelper.CheckRoleAsync(UserType.ADMIN.ToString());
        await _userHelper.CheckRoleAsync(UserType.USUARIO.ToString());
        await _userHelper.CheckRoleAsync(UserType.AUXILIAR.ToString());
        await _userHelper.CheckRoleAsync(UserType.CAJERO.ToString());
        await _userHelper.CheckRoleAsync(UserType.CLINICA.ToString());
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
