using Prueba.Models;
using Prueba.ViewModels;
using CategoriaModel = Prueba.Models.Categoria;


namespace Prueba.Views.Categoria;

public partial class CategoriaDetallePage : ContentPage
{
    public CategoriaDetallePage(CategoriaModel categoria, string accion)
    {
        InitializeComponent();
        BindingContext = new CategoriaDetalleViewModel(categoria, accion);
    }
}
