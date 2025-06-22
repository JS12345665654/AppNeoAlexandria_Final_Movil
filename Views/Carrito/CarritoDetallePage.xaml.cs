using Prueba.Models;
using Prueba.ViewModels;
using CarritoModel = Prueba.Models.Carrito;

namespace Prueba.Views.Carrito;

public partial class CarritoDetallePage : ContentPage
{
    public CarritoDetallePage(CarritoModel carrito, string accion)
    {
        InitializeComponent();
        BindingContext = new CarritoDetalleViewModel(carrito, accion);
    }
}
