using Prueba.Models;
using Prueba.ViewModels;
using LibrosModel = Prueba.Models.Libros;

namespace Prueba.Views.Libro;

public partial class LibroDetallePage : ContentPage
{
    public LibroDetallePage(LibrosModel libro, string accion)
    {
        InitializeComponent();
        BindingContext = new LibroDetalleViewModel(libro, accion);
    }
}
