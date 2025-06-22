using Prueba.ViewModels;

namespace Prueba.Views.Categoria;

public partial class CategoriaPage : ContentPage
{
    public CategoriaPage()
    {
        InitializeComponent();
        BindingContext = new CategoriaViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CategoriaViewModel vm)
        {
            vm.ObtenerTodasCategoriasCommand.Execute(null);
        }
    }
}