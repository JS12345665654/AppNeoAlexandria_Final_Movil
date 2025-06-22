using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Carrito;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Prueba.ViewModels
{
    public partial class CarritoViewModel : BaseViewModel
    {
        [ObservableProperty] private ObservableCollection<Carrito> carritos;
        [ObservableProperty] private Carrito carritoSeleccionado;
        [ObservableProperty] private bool isRefreshing;

        public CarritoViewModel()
        {
            Carritos = new ObservableCollection<Carrito>();
            CarritoSeleccionado = new Carrito();

            Title = Constants.AppName;

            Task.Run(async () => await ObtenerTodosCarritos()).Wait();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task ObtenerTodosCarritos()
        {
            IsBusy = isRefreshing = true;

            var lista = await ApiService.ObtenerTodosCarritos();

            if (lista != null)
                Carritos = new ObservableCollection<Carrito>(lista);

            IsBusy = isRefreshing = false;
        }

        [RelayCommand]
        private async Task GoToCarritoDetalle()
        {
            if (CarritoSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new CarritoDetallePage(CarritoSeleccionado, "Detalle"));
        }

        [RelayCommand]
        private async Task GoToCarritoModificar()
        {
            if (CarritoSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new CarritoDetallePage(CarritoSeleccionado, "Modificar"));
        }

        [RelayCommand]
        private async Task GoToCarritoEliminar()
        {
            if (CarritoSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new CarritoDetallePage(CarritoSeleccionado, "Eliminar"));
        }

        [RelayCommand]
        private async Task GoToCarritoAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new CarritoAgregarPage("Agregar"));
        }
    }
}
