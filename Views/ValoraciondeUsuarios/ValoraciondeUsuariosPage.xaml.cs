using Prueba.ViewModels;

namespace Prueba.Views.ValoraciondeUsuarios
{
    public partial class ValoraciondeUsuariosPage : ContentPage
    {
        public ValoraciondeUsuariosPage()
        {
            InitializeComponent();
            BindingContext = new ValoraciondeUsuariosViewModel();
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
}
