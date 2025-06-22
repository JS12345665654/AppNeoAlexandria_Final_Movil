using Prueba.ViewModels;

namespace Prueba.Views.Notas;

public partial class NotasAgregarPage : ContentPage
{
    public NotasAgregarPage()
    {
        InitializeComponent();
        BindingContext = new NotasAgregarViewModel();
    }
}
