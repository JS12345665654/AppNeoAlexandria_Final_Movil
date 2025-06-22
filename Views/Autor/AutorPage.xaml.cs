using Prueba.ViewModels;

namespace Prueba.Views.Autor;

public partial class AutorPage : ContentPage
{
	public AutorPage()
	{
		InitializeComponent();
        BindingContext = new AutorViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AutorViewModel vm)
        {
            vm.ObtenerTodosAutoresCommand.Execute(null);
        }
    }
}