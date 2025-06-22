using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Autor;
using Prueba;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Prueba.ViewModels
{
    public partial class AutorViewModel : BaseViewModel
    {
        [ObservableProperty] public ObservableCollection<Autores> autores;
        [ObservableProperty] public Autores autorSeleccionado;
        [ObservableProperty] private bool isRefreshing;

        public AutorViewModel()
        {
            Autores = new ObservableCollection<Autores>();
            autorSeleccionado = new Autores();

            Title = Constants.AppName;

            Task.Run(async () => { await ObtenerTodosAutores(); }).Wait();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task ObtenerTodosAutores()
        {
            IsBusy = isRefreshing = true;

            var autores = await ApiService.ObtenerTodosAutores();

            if (autores != null)
            {
                Autores = new ObservableCollection<Autores>(autores);
            }

            IsBusy = isRefreshing = false;
        }
        
        [RelayCommand]
        private async Task GoToAutorDetalle()
        {
            if (autorSeleccionado == null) return;

           await Application.Current.MainPage.Navigation.PushAsync(new AutorDetallePage(autorSeleccionado, "Detalle"));
        }

        [RelayCommand]
        private async Task GoToAutorAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new AutorAgregarPage("Agregar"));
        }

        [RelayCommand]
        private async Task GoToAutorModificar()
        {
            if (autorSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new AutorDetallePage(autorSeleccionado, "Modificar"));
        }

        [RelayCommand]
        private async Task GoToAutorEliminar()
        {
            if (autorSeleccionado == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new AutorDetallePage(autorSeleccionado, "Eliminar"));
        }
    }
}
