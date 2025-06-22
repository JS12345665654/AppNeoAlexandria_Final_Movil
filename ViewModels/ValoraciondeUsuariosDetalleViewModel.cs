using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class ValoraciondeUsuariosDetalleViewModel : BaseViewModel
{
    [ObservableProperty] private ValoraciondeUsuario detalleValoracion;
    [ObservableProperty] private bool esSoloLectura;
    [ObservableProperty] private bool modoEdicion;
    [ObservableProperty] private string textoBoton;
    [ObservableProperty] private bool mostrarBotones;

    private readonly string _accion;

    public ValoraciondeUsuariosDetalleViewModel(ValoraciondeUsuario valoracion, string accion)
    {
        detalleValoracion = valoracion;
        _accion = accion;
        Title = $"{accion} Valoración";

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
            await ModificarValoracion();
        }
        else if (_accion == "Modificar")
        {
            await ModificarValoracion();
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
            "Confirmar", "¿Desea eliminar esta valoración?", "Sí", "Cancelar");

        if (!confirmar) return;

        try
        {
            IsBusy = true;
            await ApiService.EliminarValoracion(DetalleValoracion.IdValoracion);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Valoración eliminada correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar la valoración.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task ModificarValoracion()
    {
        try
        {
            IsBusy = true;
            await ApiService.ModificarValoracion(DetalleValoracion);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Valoración modificada correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo modificar la valoración.", "Aceptar");
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
