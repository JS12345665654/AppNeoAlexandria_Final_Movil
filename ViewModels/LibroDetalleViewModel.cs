using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Notas;
using Prueba.Views.ValoraciondeUsuarios;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class LibroDetalleViewModel : BaseViewModel
{
    [ObservableProperty] private Libros detalleLibro;
    [ObservableProperty] private bool esSoloLectura;
    [ObservableProperty] private bool modoEdicion;
    [ObservableProperty] private string textoBoton;
    [ObservableProperty] private bool mostrarBotones;

    [ObservableProperty] private List<Categoria> listaCategorias;
    [ObservableProperty] private Categoria? categoriaSeleccionada;

    [ObservableProperty] private List<Autores> listaAutores;
    [ObservableProperty] private Autores? autorSeleccionado;

    private readonly string _accion;

    public LibroDetalleViewModel(Libros libro, string accion)
    {
        detalleLibro = libro;
        _accion = accion;
        Title = $"{accion} Libro";

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

        _ = CargarCategorias();
        _ = CargarAutores();
    }

    [RelayCommand]
    private void ActivarEdicion()
    {
        ModoEdicion = true;
        EsSoloLectura = false;
        TextoBoton = "Guardar";
        _ = CargarCategorias();
        _ = CargarAutores();
    }

    [RelayCommand]
    private async Task EjecutarAccion()
    {
        if (_accion == "Detalle" && ModoEdicion)
        {
            await ModificarLibro();
        }
        else if (_accion == "Modificar")
        {
            await ModificarLibro();
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

    [RelayCommand]
    private async Task ConfirmarYEliminar()
    {
        bool confirmar = await Application.Current.MainPage.DisplayAlert(
            "Confirmar", "¿Quiere eliminar el libro?", "Sí", "Cancelar");

        if (!confirmar) return;

        try
        {
            IsBusy = true;
            await ApiService.EliminarLibro(DetalleLibro.IdLibro);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Libro eliminado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo eliminar el libro.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task ModificarLibro()
    {
        try
        {
            IsBusy = true;

            if (CategoriaSeleccionada != null)
                DetalleLibro.IdCategoria = CategoriaSeleccionada.IdCategoria;

            if (AutorSeleccionado != null)
                DetalleLibro.IdAutor = AutorSeleccionado.IdAutor;

            await ApiService.ModificarLibro(DetalleLibro);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Libro modificado correctamente.", "Aceptar");
            await GoBack();
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "No se pudo modificar el libro.", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task CargarCategorias()
    {
        var categorias = await ApiService.ObtenerTodasCategorias();
        ListaCategorias = categorias;
        CategoriaSeleccionada = categorias.FirstOrDefault(c => c.IdCategoria == DetalleLibro.IdCategoria);
    }

    private async Task CargarAutores()
    {
        var autores = await ApiService.ObtenerTodosAutores();
        ListaAutores = autores;
        AutorSeleccionado = autores.FirstOrDefault(a => a.IdAutor == DetalleLibro.IdAutor);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        try
        {
            var libroPage = Application.Current.MainPage.Navigation
                .NavigationStack
                .Reverse()
                .FirstOrDefault(p => p is Views.Libro.LibroPage) as Views.Libro.LibroPage;

            if (libroPage?.BindingContext is LibroViewModel vm)
            {
                await vm.ObtenerTodosLibros();
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar la lista: {ex.Message}", "Aceptar");
        }

        await Application.Current.MainPage.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task VerNotas()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new NotasPage(DetalleLibro.IdLibro));
    }
}