using Prueba.Models;
using Prueba.ViewModels;
using UsuarioModel = Prueba.Models.Usuario;

namespace Prueba.Views.Usuario;
public partial class UsuarioDetallePage : ContentPage
{
    public UsuarioDetallePage(UsuarioModel usuario, string accion)
    {
        InitializeComponent();
        BindingContext = new UsuarioDetalleViewModel(usuario, accion);
    }
}
