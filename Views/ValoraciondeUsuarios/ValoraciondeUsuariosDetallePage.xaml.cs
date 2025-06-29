using Prueba.Models;
using Prueba.ViewModels;
using ValoracionModel = Prueba.Models.ValoraciondeUsuario;

namespace Prueba.Views.ValoraciondeUsuarios;

public partial class ValoraciondeUsuariosDetallePage : ContentPage
{
    public ValoraciondeUsuariosDetallePage(ValoracionModel valoracion, string accion)
    {
        InitializeComponent();
        BindingContext = new ValoraciondeUsuariosDetalleViewModel(valoracion, accion);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ValoraciondeUsuariosViewModel vm)
        {
            vm.ObtenerTodasValoracionesCommand.Execute(null);
        }
    }
}
