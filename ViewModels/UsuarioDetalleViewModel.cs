using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class UsuarioDetalleViewModel : BaseViewModel
{
    [ObservableProperty] private Usuario detalleUsuario;
    [ObservableProperty] private bool esSoloLectura;
    [ObservableProperty] private bool modoEdicion;
    [ObservableProperty] private string textoBoton;
    [ObservableProperty] private bool mostrarBotones;

    private readonly string _accion;

    public UsuarioDetalleViewModel(Usuario usuario, string accion)
    {
        detalleUsuario = usuario;
        _accion = accion;
        Title = $"{accion} Usuario";

        MostrarBotones = Transport.IdRol == "Administrador";

        if (_accion == "Detalle")
        {
            EsSoloLectura = true;
            ModoEdicion = false;
            TextoBoton = "Volver";
        }
        else if (_accion == "Eliminar")
        {
            EsSoloLectura = true;
            ModoEdicion = false;
            TextoBoton = "Eliminar";
        }
        else if (_accion == "Modificar")
        {
            EsSoloLectura = false;
            ModoEdicion = true;
            TextoBoton = "Guardar";
        }
    }

    [RelayCommand]
    private void ActivarEdicion()
    {
        ModoEdicion = true;
        EsSoloLectura = false;
        TextoBoton = "Guardar";
    }

    [RelayCommand]
    private async Task EjecutarAccion()
    {
        if (_accion == "Detalle" && ModoEdicion)
        {
            await ModificarUsuario();
        }
        else if (_accion == "Modificar")
        {
            await ModificarUsuario();
        }
        else if (_accion == "Eliminar")
        {
            await ConfirmarYEliminar();
        }
        else
        {
            await GoBack();
        }
    }

    [RelayCommand]
    private async Task ConfirmarYEliminar()
    {
        bool confirmar = await Application.Current.MainPage.DisplayAlert(
            "Confirmar", "¿Quiere eliminar el Usuario?", "Sí", "Cancelar");

        if (!confirmar) return;

        IsBusy = true;

        try
        {
            bool exito = await ApiService.BorrarUsuario(DetalleUsuario.IdUsuario);

            if (exito)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario eliminado correctamente.", "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el usuario.", "Aceptar");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Fallo inesperado: {ex.Message}", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task ModificarUsuario()
    {
        try
        {
            IsBusy = true;
            var rolInput = DetalleUsuario.RolDesdeApi?.Trim().ToUpper();
            if (rolInput == "ADMINISTRADOR")
                DetalleUsuario.IdRol = "0";
            else if (rolInput == "USUARIO")
                DetalleUsuario.IdRol = "1";

            await ApiService.ModificarUsuario(DetalleUsuario);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario modificado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo modificar el usuario.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }
}
