using Prueba.Models;
using Prueba.Utils;
using Prueba.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class AutorDetalleViewModel : BaseViewModel
{
    [ObservableProperty]
    private Autores detalleAutor;

    [ObservableProperty]
    private bool esSoloLectura;

    [ObservableProperty]
    private bool modoEdicion;

    [ObservableProperty]
    private string textoBoton;

    [ObservableProperty]
    private bool mostrarBotones;

    private readonly string _accion;

    public DateTime AnioFallecimientoSeguro =>
        DetalleAutor.AnioFallecimiento ?? DateTime.Now;

    public AutorDetalleViewModel(Autores autor, string accion)
    {
        detalleAutor = autor;
        _accion = accion;
        Title = $"{accion} Autor";

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
            await ModificarAutor();
        }
        else if (_accion == "Modificar")
        {
            await ModificarAutor();
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
            "Confirmar", "¿Quiere eliminar el Autor?", "Sí", "Cancelar");

        if (!confirmar) return;

        try
        {
            IsBusy = true;
            await ApiService.EliminarAutores(DetalleAutor.IdAutor);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Autor eliminado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el autor.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task ModificarAutor()
    {
        try
        {
            IsBusy = true;
            await ApiService.ModificarAutor(DetalleAutor);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Autor modificado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo modificar el autor.", "Aceptar");
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
