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

    [ObservableProperty]
    private List<ValoraciondeUsuario> listaValoraciones;

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
        _ = CargarValoraciones();
    }

    [RelayCommand]
    private void ActivarEdicion()
    {
        ModoEdicion = true;
        EsSoloLectura = false;
        TextoBoton = "Guardar";
        _ = CargarCategorias();
        _ = CargarAutores();
        _ = CargarValoraciones();
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

    private async Task CargarValoraciones()
    {
        ListaValoraciones = await ApiService.ObtenerValoracionesPorLibro(DetalleLibro.IdLibro);
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

    [RelayCommand]
    private async Task AgregarNota()
    {
        await Application.Current.MainPage.Navigation.PushAsync(
            new NotasAgregarPage(DetalleLibro.IdLibro));
    }
    [RelayCommand]
    private async Task VerValoraciones()
    {
        await Application.Current.MainPage.Navigation.PushAsync(
            new ValoraciondeUsuariosPage(DetalleLibro.IdLibro));
    }


    [RelayCommand]
    private async Task AñadirAlCarrito()
    {
        if (Transport.IdUsuario == 0)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Usuario inválido. No se puede crear el carrito.", "Aceptar");
            return;
        }

        try
        {
            IsBusy = true;

            var carritoConDetalle = new CarritoConDetalleDTO
            {
                IdUsuario = Transport.IdUsuario,
                PrecioTotalCarrito = (double)DetalleLibro.Precio,
                FechaCreacion = DateTime.Now,
                Descripcion = $"Compra del día {DateTime.Now:dd/MM/yyyy}",
                Estado = true,
                IdLibro = DetalleLibro.IdLibro,
                PrecioTotalDetalleCarrito = (double)DetalleLibro.Precio,
                FechaFactura = DateTime.Now,
                FechaCreacionFactura = DateTime.Now,
                DetalleFactura = $"Libro '{DetalleLibro.Nombre}' comprado el {DateTime.Now:dd/MM/yyyy}"
            };

            bool ok = await ApiService.CrearCarritoConDetalle(carritoConDetalle);
            if (ok)
                await Application.Current.MainPage.DisplayAlert("Éxito", "Libro añadido al carrito correctamente.", "Aceptar");
            else
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo añadir al carrito", "Aceptar");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al crear carrito: {ex.Message}", "Aceptar");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task GoToValoraciones()
    {
        await Application.Current.MainPage.Navigation.PushAsync(new ValoraciondeUsuariosPage(DetalleLibro.IdLibro));
    }
}