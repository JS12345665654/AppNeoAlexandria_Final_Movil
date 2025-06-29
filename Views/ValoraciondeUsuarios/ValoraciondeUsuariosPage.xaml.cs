using Prueba.ViewModels;

namespace Prueba.Views.ValoraciondeUsuarios
{
    public partial class ValoraciondeUsuariosPage : ContentPage
    {
        public ValoraciondeUsuariosPage(int idlibro)
        {
            InitializeComponent();
            BindingContext = new ValoraciondeUsuariosViewModel(idlibro);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ValoraciondeUsuariosViewModel vm)
            {
                if (vm.IdLibro == null)
                {
                    vm.ObtenerTodasValoracionesCommand.Execute(null);
                }
                else
                {
                    vm.ObtenerValoracionesPorLibroCommand.Execute(vm.IdLibro);
                }
            }
        }
    }
}