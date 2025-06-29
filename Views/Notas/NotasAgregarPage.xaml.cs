using Prueba.ViewModels;

namespace Prueba.Views.Notas;

public partial class NotasAgregarPage : ContentPage
{
    public NotasAgregarPage(int idlibro)
    {
        InitializeComponent();
        BindingContext = new NotasAgregarViewModel(idlibro);
    }
}
