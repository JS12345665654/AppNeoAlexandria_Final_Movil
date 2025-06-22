using Prueba.ViewModels;

namespace Prueba.Views.Libro
{
    public partial class LibroPage : ContentPage
    {
        public LibroPage()
        {
            InitializeComponent();
            BindingContext = new LibroViewModel();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is LibroViewModel vm)
            {
                await vm.ObtenerTodosLibrosCommand.ExecuteAsync(null);
            }
        }
    }
}
