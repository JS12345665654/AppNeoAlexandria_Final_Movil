using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels
{
    public partial class CarritoAgregarViewModel : BaseViewModel
    {
        [ObservableProperty] private int idCarrito;
        [ObservableProperty] private int idUsuario;
        [ObservableProperty] private decimal precioTotalCarrito;
        [ObservableProperty] private DateTime fechaCreacion;
        [ObservableProperty] private string descripcion;
        [ObservableProperty] private bool estado;

        public CarritoAgregarViewModel(string accion)
        {
            Title = "Agregar Carrito";
            FechaCreacion = DateTime.Now;
            Estado = true;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task CrearCarritos()
        {
            var nuevoCarrito = new Carrito
            {
                IdCarrito = IdCarrito,
                IdUsuario = IdUsuario,
                PrecioTotalCarrito = PrecioTotalCarrito,
                FechaCreacion = FechaCreacion,
                Descripcion = Descripcion,
                Estado = Estado
            };

            try
            {
                await ApiService.CrearCarritos(nuevoCarrito);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Se agregó un nuevo carrito.", "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar el carrito.", "Aceptar");
            }
        }
    }
}
