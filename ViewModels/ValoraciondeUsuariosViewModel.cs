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
        [ObservableProperty]
        private ObservableCollection<ValoraciondeUsuario> valoraciones;

        [ObservableProperty]
        private ValoraciondeUsuario valoracionSeleccionada;

        [ObservableProperty]
        private bool isRefreshing;

        private readonly int? _idLibro;
        public int? IdLibro => _idLibro;

        public ValoraciondeUsuariosViewModel(int? idLibro = null)
        {
            _idLibro = idLibro;
            Valoraciones = new ObservableCollection<ValoraciondeUsuario>();
            ValoracionSeleccionada = new ValoraciondeUsuario();
            Title = "Valoraciones";

            _ = ObtenerValoracionesIniciales();
        }

        private async Task ObtenerValoracionesIniciales()
        {
            if (_idLibro.HasValue)
                await ObtenerValoracionesPorLibro(_idLibro.Value);
            else
                await ObtenerTodasValoraciones();
        }

        [RelayCommand]
        private async Task ObtenerValoracionesPorLibro(int idLibro)
        {
            IsBusy = IsRefreshing = true;

            var lista = await ApiService.ObtenerValoracionesPorLibro(idLibro);
            var usuarios = await ApiService.ObtenerTodosUsuarios();

            foreach (var valoracion in lista)
            {
                valoracion.NombreUsuario = usuarios.FirstOrDefault(u => u.IdUsuario == valoracion.IdUsuario)?.Nombre ?? "Sin nombre";
            }

            Valoraciones = new ObservableCollection<ValoraciondeUsuario>(lista);

            IsBusy = IsRefreshing = false;
        }

        [RelayCommand]
        private async Task ObtenerTodasValoraciones()
        {
            IsBusy = IsRefreshing = true;

            var lista = await ApiService.ObtenerTodasValoraciones();
            var usuarios = await ApiService.ObtenerTodosUsuarios();

            foreach (var valoracion in lista)
            {
                valoracion.NombreUsuario = usuarios.FirstOrDefault(u => u.IdUsuario == valoracion.IdUsuario)?.Nombre ?? "Sin nombre";
            }

            Valoraciones = new ObservableCollection<ValoraciondeUsuario>(lista);

            IsBusy = IsRefreshing = false;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task GoToValoracionDetalle()
        {
            if (ValoracionSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosDetallePage(ValoracionSeleccionada, "Detalle"));

            await RefrescarAlVolver();
        }

        [RelayCommand]
        private async Task GoToValoracionModificar()
        {
            if (ValoracionSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosDetallePage(ValoracionSeleccionada, "Modificar"));

            await RefrescarAlVolver();
        }

        [RelayCommand]
        private async Task GoToValoracionEliminar()
        {
            if (ValoracionSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosDetallePage(ValoracionSeleccionada, "Eliminar"));

            await RefrescarAlVolver();
        }

        [RelayCommand]
        private async Task GoToValoracionAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosAgregarPage());

            await RefrescarAlVolver();
        }

        private async Task RefrescarAlVolver()
        {
            // Espera breve para asegurar el pop de la navegación antes de refrescar
            await Task.Delay(300);

            if (_idLibro.HasValue)
                await ObtenerValoracionesPorLibro(_idLibro.Value);
            else
                await ObtenerTodasValoraciones();
        }
    }
}