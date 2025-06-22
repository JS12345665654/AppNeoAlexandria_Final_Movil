using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.ValoraciondeUsuarios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Prueba.ViewModels
{
    public partial class ValoraciondeUsuariosViewModel : BaseViewModel
    {
        [ObservableProperty] private ObservableCollection<ValoraciondeUsuario> valoracion;
        [ObservableProperty] private ValoraciondeUsuario valoracionSeleccionada;
        [ObservableProperty] private bool isRefreshing;

        private readonly int? _idLibro;

        public ValoraciondeUsuariosViewModel(int? idLibro = null)
        {
            Valoracion = new ObservableCollection<ValoraciondeUsuario>();
            ValoracionSeleccionada = new ValoraciondeUsuario();
            Title = "Valoraciones";
            _idLibro = idLibro;

            Task.Run(async () => await ObtenerTodasValoraciones()).Wait();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task ObtenerTodasValoraciones()
        {
            IsBusy = isRefreshing = true;

            var lista = await ApiService.ObtenerTodasValoraciones();

            if (lista != null)
            {
                if (_idLibro.HasValue)
                    lista = lista.Where(v => v.IdLibro == _idLibro.Value).ToList();

                Valoracion = new ObservableCollection<ValoraciondeUsuario>(lista);
            }

            IsBusy = isRefreshing = false;
        }

        [RelayCommand]
        private async Task GoToValoracionDetalle()
        {
            if (ValoracionSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosDetallePage(ValoracionSeleccionada, "Detalle"));
        }

        [RelayCommand]
        private async Task GoToValoracionModificar()
        {
            if (ValoracionSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosDetallePage(ValoracionSeleccionada, "Modificar"));
        }

        [RelayCommand]
        private async Task GoToValoracionEliminar()
        {
            if (ValoracionSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosDetallePage(ValoracionSeleccionada, "Eliminar"));
        }

        [RelayCommand]
        private async Task GoToValoracionAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosAgregarPage());
        }
    }
}