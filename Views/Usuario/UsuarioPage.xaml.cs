using Prueba.ViewModels;

namespace Prueba.Views.Usuario;

public partial class UsuarioPage : ContentPage
{
    public UsuarioPage()
    {
        InitializeComponent();
        BindingContext = new UsuarioViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is UsuarioViewModel vm && vm.ObtenerTodosUsuariosCommand?.CanExecute(null) == true)
        {
            vm.ObtenerTodosUsuariosCommand.Execute(null);
        }
    }
}
