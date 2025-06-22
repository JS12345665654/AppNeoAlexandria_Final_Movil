using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Categoria;
using Prueba;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Prueba.ViewModels
{
    public partial class CategoriaViewModel : BaseViewModel
    {
        [ObservableProperty] private ObservableCollection<Categoria> categorias;
        [ObservableProperty] private Categoria categoriaSeleccionada;
        [ObservableProperty] private bool isRefreshing;

        public CategoriaViewModel()
        {
            Categorias = new ObservableCollection<Categoria>();
            CategoriaSeleccionada = new Categoria();

            Title = Constants.AppName;

            Task.Run(async () => await ObtenerTodasCategorias()).Wait();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task ObtenerTodasCategorias()
        {
            IsBusy = isRefreshing = true;

            var obtenidas = await ApiService.ObtenerTodasCategorias();

            if (obtenidas != null)
                Categorias = new ObservableCollection<Categoria>(obtenidas);

            IsBusy = isRefreshing = false;
        }

        [RelayCommand]
        private async Task GoToCategoriaDetalle()
        {
            if (CategoriaSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new CategoriaDetallePage(CategoriaSeleccionada, "Detalle"));
        }

        [RelayCommand]
        private async Task GoToCategoriaModificar()
        {
            if (CategoriaSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new CategoriaDetallePage(CategoriaSeleccionada, "Modificar"));
        }

        [RelayCommand]
        private async Task GoToCategoriaEliminar()
        {
            if (CategoriaSeleccionada == null) return;

            await Application.Current.MainPage.Navigation.PushAsync(
                new CategoriaDetallePage(CategoriaSeleccionada, "Eliminar"));
        }

        [RelayCommand]
        private async Task GoToCategoriaAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new CategoriaAgregarPage("Agregar"));
        }
    }
}
