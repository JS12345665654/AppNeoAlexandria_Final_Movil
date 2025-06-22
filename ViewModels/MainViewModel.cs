using Prueba.Views;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Prueba.Views.Autor;
using Prueba.Views.Carrito;
using Prueba.Views.Categoria;
using Prueba.Views.Libro;
using Prueba.Views.Usuario;
using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace Prueba.ViewModels;

public partial class MainViewModel : BaseViewModel
{
    [ObservableProperty] private string _Saludo;
    public MainViewModel()
    {
        Title = "App Neo Alexandría";

        var rol = Preferences.Get("IdRol", "Sin rol");

        _Saludo = $"¡Bienvenido!\nTu rol es: {rol}";
    }

    [RelayCommand]
    public async Task GoToProductoLista()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LibroPage());
    }

    [RelayCommand]
    public async Task GoToUsuarioLista()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new UsuarioPage());
    }

    [RelayCommand]
    public async Task GoToAcerca()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AcercaPage());
    }

    [RelayCommand]
    public async Task GoToCarritoLista()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new CarritoPage());
    }

    [RelayCommand]
    public async Task GoToCategoriaLista()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new CategoriaPage());
    }

    [RelayCommand]
    public async Task GoToAutorLista()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new AutorPage());
    }

    [RelayCommand]
    public async Task Exit()
    {
        bool answer = await Application.Current.MainPage.DisplayAlert("Salir", "¿Desea terminar la sesión y salir?", "Aceptar", "Cancelar");

        if (answer)
        {
            // Borrar datos de sesión
            Preferences.Remove("Token");
            Preferences.Remove("IdUsuario");
            Preferences.Remove("Nombre");
            Preferences.Remove("Email");
            Preferences.Remove("IdRol");

            // Redirigir al login
            Application.Current.MainPage = new NavigationPage(new LoginyRegistroPage());
        }
    }
}