using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class CarritoDetalleViewModel : BaseViewModel
{
    [ObservableProperty] private Carrito detalleCarrito;
    [ObservableProperty] private bool esSoloLectura;
    [ObservableProperty] private bool modoEdicion;
    [ObservableProperty] private string textoBoton;
    [ObservableProperty] private bool mostrarBotones;

    private readonly string _accion;

    public CarritoDetalleViewModel(Carrito carrito, string accion)
    {
        detalleCarrito = carrito;
        _accion = accion;
        Title = $"{accion} Carrito";

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
            await ModificarCarrito();
        }
        else if (_accion == "Modificar")
        {
            await ModificarCarrito();
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

    private async Task ModificarCarrito()
    {
        try
        {
            IsBusy = true;
            await ApiService.ModificarCarrito(DetalleCarrito);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Carrito modificado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo modificar el carrito.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task ConfirmarYEliminar()
    {
        bool confirmar = await Application.Current.MainPage.DisplayAlert(
            "Confirmar", "¿Quiere eliminar el carrito?", "Sí", "Cancelar");

        if (!confirmar) return;

        try
        {
            IsBusy = true;
            await ApiService.BorrarCarrito(DetalleCarrito.IdCarrito);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Carrito eliminado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el carrito.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoBack()
    {
        try
        {
            var carritoPage = Application.Current.MainPage.Navigation
                .NavigationStack
                .Reverse()
                .FirstOrDefault(p => p is Views.Carrito.CarritoPage) as Views.Carrito.CarritoPage;

            if (carritoPage?.BindingContext is CarritoViewModel vm)
            {
                await vm.ObtenerTodosCarritos();
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar la lista: {ex.Message}", "Aceptar");
        }

        await Application.Current.MainPage.Navigation.PopAsync();
    }
}