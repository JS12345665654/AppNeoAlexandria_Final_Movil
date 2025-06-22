using Prueba;
using Prueba.Models;
using Prueba.Utils;
using Prueba.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Prueba.ViewModels;

public partial class LoginyRegistroViewModel : BaseViewModel
{
    public LoginyRegistroViewModel()
    {
        Title = Constants.AppName;
        ModoLogin = true;
        IdRol = "Usuario"; // Valor por defecto para el Picker
        Activo = true;
    }

    // Lista de roles disponibles para el Picker
    public List<string> RolesDisponibles { get; } = new() { "Administrador", "Usuario" };

    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _contrasenia = string.Empty;

    [ObservableProperty] private string nombre = string.Empty;
    [ObservableProperty] private string? categoriaPreferida;
    [ObservableProperty] private string idRol;
    [ObservableProperty] private bool activo;

    // --- Control de vista ---
    [ObservableProperty] private bool modoLogin;

    [RelayCommand]
    public void CambiarModo()
    {
        ModoLogin = !ModoLogin;
        Title = ModoLogin ? "Iniciar Sesión" : "Crear Cuenta";
    }

    [RelayCommand]
    public async Task ConfirmarAsync()
    {
        if (ModoLogin)
            await LoginAsync();
        else
            await RegistrarAsync();
    }

    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Contrasenia))
        {
            await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos", "Aceptar");
            return;
        }

        var apiClient = new ApiService();
        var login = await apiClient.Login(Email, Contrasenia); // devuelve JwtResponseDTO

        if (login != null && login.IsSuccess)
        {
            // Guardar el token en Preferences
            Preferences.Set("Token", login.Token);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(login.Token);

            var idUsuario = jwt.Claims.FirstOrDefault(c => c.Type == "IdUsuario")?.Value;
            var nombre = jwt.Claims.FirstOrDefault(c => c.Type == "Nombre")?.Value;
            var idRol = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var email = jwt.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            // Guardar datos en Transport (para uso en tiempo real)
            Transport.IdUsuario = int.Parse(idUsuario ?? "0");
            Transport.Nombre = nombre;
            Transport.Email = email;
            Transport.IdRol = idRol;

            // Guardar datos útiles en Preferences (para uso persistente)
            Preferences.Set("IdUsuario", Transport.IdUsuario);
            Preferences.Set("Nombre", nombre);
            Preferences.Set("Email", email);
            Preferences.Set("IdRol", idRol);

            Application.Current.MainPage = new NavigationPage(new MainPage());
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Credenciales inválidas", "Aceptar");
        }
    }

    private async Task RegistrarAsync()
    {
        if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Contrasenia))
        {
            await Application.Current.MainPage.DisplayAlert("Atención", "Debe completar todos los campos", "Aceptar");
            return;
        }

        string rolSeleccionado = IdRol switch
        {
            "Administrador" => "Administrador",
            "Usuario" => "Usuario",
            _ => "Usuario"
        };

        var nuevoUsuario = new UsuarioLoginDTO
        {
            Nombre = Nombre,
            Email = Email,
            Contrasenia = Contrasenia,
            CategoriaPreferida = CategoriaPreferida,
            Rol = rolSeleccionado
        };

        try
        {
            var apiClient = new ApiService();
            var response = await apiClient.Registrarse(nuevoUsuario);

            if (response != null)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario creado con éxito", "Aceptar");

                // Limpiar campos y volver al modo login
                Email = Contrasenia = Nombre = CategoriaPreferida = IdRol = string.Empty;
                CambiarModo();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El registro no fue exitoso", "Aceptar");
            }
        }
        catch (Exception)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el usuario", "Aceptar");
        }
    }
}