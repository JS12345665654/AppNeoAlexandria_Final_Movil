using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Libro;
using Prueba.Views.ValoraciondeUsuarios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Prueba.ViewModels;
public partial class LibroViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<Libros> libros;
    [ObservableProperty] private ObservableCollection<Libros> librosFiltrados;
    [ObservableProperty] private Libros libroSeleccionado;
    [ObservableProperty] private string textoBusqueda;
    [ObservableProperty] private bool isRefreshing;

    public LibroViewModel()
    {
        Libros = new ObservableCollection<Libros>();
        LibrosFiltrados = new ObservableCollection<Libros>();
        LibroSeleccionado = new Libros();
        Title = Constants.AppName;

        Task.Run(async () => { await ObtenerTodosLibros(); }).Wait();
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
            AplicarFiltro(); 
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
    partial void OnTextoBusquedaChanged(string value)
    {
        AplicarFiltro();
    }

    private void AplicarFiltro()
    {
        if (string.IsNullOrWhiteSpace(TextoBusqueda))
        {
            LibrosFiltrados = new ObservableCollection<Libros>(Libros);
        }
        else
        {
            var filtro = TextoBusqueda.ToLower().Trim();
            LibrosFiltrados = new ObservableCollection<Libros>(
                Libros.Where(l =>
                    !string.IsNullOrEmpty(l.NombreCategoria) &&
                    l.NombreCategoria.ToLower().Contains(filtro)
                ));
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
    private async Task GoToLibroAgregar()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new LibroAgregarPage());
    }
}