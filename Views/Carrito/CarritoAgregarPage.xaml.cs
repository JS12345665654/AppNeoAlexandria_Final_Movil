using Prueba.ViewModels;

namespace Prueba.Views.Carrito;

public partial class CarritoAgregarPage : ContentPage
{
    public CarritoAgregarPage(string accion)
    {
        InitializeComponent();
        BindingContext = new CarritoAgregarViewModel(accion);
    }
}