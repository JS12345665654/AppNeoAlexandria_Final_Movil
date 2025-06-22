using Prueba.Models;
using Prueba.ViewModels;
using NotasModel = Prueba.Models.Notas;

namespace Prueba.Views.Notas;

public partial class NotasDetallePage : ContentPage
{
    public NotasDetallePage(NotasModel nota, string accion)
    {
        InitializeComponent();
        BindingContext = new NotaDetalleViewModel(nota, accion);
    }
}
