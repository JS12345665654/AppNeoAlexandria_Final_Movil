using Prueba.ViewModels;

namespace Prueba.Views.ValoraciondeUsuarios;

public partial class ValoraciondeUsuariosAgregarPage : ContentPage
{
    public ValoraciondeUsuariosAgregarPage()
    {
        InitializeComponent();
        BindingContext = new ValoraciondeUsuariosAgregarViewModel();
    }
}
