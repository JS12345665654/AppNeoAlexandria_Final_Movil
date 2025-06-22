using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Usuario;
using Prueba;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Prueba.ViewModels
{
    public partial class UsuarioViewModel : BaseViewModel
    {
        [ObservableProperty] private ObservableCollection<Usuario> usuarios;
        [ObservableProperty] private Usuario usuarioSeleccionado;
        [ObservableProperty] private bool isRefreshing;

        public UsuarioViewModel()
        {
            Usuarios = new ObservableCollection<Usuario>();
            UsuarioSeleccionado = new Usuario();

            Title = Constants.AppName;

            Task.Run(async () => { await ObtenerTodosUsuarios(); }).Wait();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task ObtenerTodosUsuarios()
        {
            IsBusy = isRefreshing = true;

            var usuarioObtenidos = await ApiService.ObtenerTodosUsuarios();

            if (usuarioObtenidos != null)
            {
                Usuarios = new ObservableCollection<Usuario>(usuarioObtenidos);
            }

            IsBusy = isRefreshing = false;
        }

        [RelayCommand]
        private async Task GoToUsuarioDetalle()
        {
            if (UsuarioSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new UsuarioDetallePage(UsuarioSeleccionado, "Detalle"));
        }

        [RelayCommand]
        private async Task GoToUsuarioModificar()
        {
            if (UsuarioSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new UsuarioDetallePage(UsuarioSeleccionado, "Modificar"));
        }

        [RelayCommand]
        private async Task GoToUsuarioEliminar()
        {
            if (UsuarioSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new UsuarioDetallePage(UsuarioSeleccionado, "Eliminar"));
        }
    }
}
