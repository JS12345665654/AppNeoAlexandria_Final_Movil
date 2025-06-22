using Prueba.Models;
using Prueba.ViewModels;

namespace Prueba.Views.Libro
{
    public partial class LibroAgregarPage : ContentPage
    {
        public LibroAgregarPage()
        {
            InitializeComponent();
            BindingContext = new LibroAgregarViewModel();
        }
    }
}
