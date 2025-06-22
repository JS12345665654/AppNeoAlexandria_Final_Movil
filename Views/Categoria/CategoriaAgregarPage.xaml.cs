using Prueba.ViewModels;

namespace Prueba.Views.Categoria;

public partial class CategoriaAgregarPage : ContentPage
{
    public CategoriaAgregarPage(string accion)
    {
        InitializeComponent();
        BindingContext = new CategoriaAgregarViewModel(accion);
    }
}
