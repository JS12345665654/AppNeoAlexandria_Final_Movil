using Prueba.ViewModels;

namespace Prueba.Views.Carrito;

public partial class CarritoPage : ContentPage
{
    public CarritoPage()
    {
        InitializeComponent();
        BindingContext = new CarritoViewModel();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is CarritoViewModel vm)
        {
            vm.ObtenerTodosCarritosCommand.Execute(null);
        }
    }
}
