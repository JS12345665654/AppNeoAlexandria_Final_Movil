using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Libro;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prueba.Views.ValoraciondeUsuarios;

namespace Prueba.ViewModels
{
    public partial class LibroViewModel : BaseViewModel
    {
        [ObservableProperty] private ObservableCollection<Libros> libros;
        [ObservableProperty] private Libros libroSeleccionado;
        [ObservableProperty] private bool isRefreshing;

        public LibroViewModel()
        {
            Libros = new ObservableCollection<Libros>();
            LibroSeleccionado = new Libros();

            Title = Constants.AppName;

            Task.Run(async () => { await ObtenerTodosLibros(); }).Wait();
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        public async Task ObtenerTodosLibros()
        {
            IsBusy = isRefreshing = true;

            try
            {
                var lista = await ApiService.ObtenerTodosLibros();
                var autores = await ApiService.ObtenerTodosAutores();
                var categorias = await ApiService.ObtenerTodasCategorias();

                foreach (var libro in lista)
                {
                    var autor = autores.FirstOrDefault(a => a.IdAutor == libro.IdAutor);
                    if (autor != null)
                        libro.NombreAutor = autor.NombreAutor;

                    var categoria = categorias.FirstOrDefault(c => c.IdCategoria == libro.IdCategoria);
                    if (categoria != null)
                        libro.NombreCategoria = categoria.NombreCategoria;
                }

                Libros = new ObservableCollection<Libros>(lista);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudieron cargar los libros: {ex.Message}", "Aceptar");
            }
            finally
            {
                IsBusy = isRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task GoToLibroDetalle()
        {
            if (LibroSeleccionado == null) return;
            await Application.Current.MainPage.Navigation.PushAsync(
                new LibroDetallePage(LibroSeleccionado, "Detalle"));
        }

        [RelayCommand]
        private async Task GoToLibroModificar()
        {
            if (LibroSeleccionado == null) return;
            await Application.Current.MainPage.Navigation.PushAsync(
                new LibroDetallePage(LibroSeleccionado, "Modificar"));
        }

        [RelayCommand]
        private async Task GoToLibroEliminar()
        {
            if (LibroSeleccionado == null) return;
            await Application.Current.MainPage.Navigation.PushAsync(
                new LibroDetallePage(LibroSeleccionado, "Eliminar"));
        }

        [RelayCommand]
        private async Task GoToLibroAgregar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new LibroAgregarPage());
        }

        [RelayCommand]
        private async Task GoToValoraciones()
        {
            await Application.Current.MainPage.Navigation.PushAsync(
                new ValoraciondeUsuariosPage());
        }

    }
}