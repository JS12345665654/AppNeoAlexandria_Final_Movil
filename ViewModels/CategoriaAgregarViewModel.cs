using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels
{
    public partial class CategoriaAgregarViewModel : BaseViewModel
    {
        [ObservableProperty] private int idCategoria;
        [ObservableProperty] private string nombreCategoria;
        [ObservableProperty] private string descripcionCategoria;

        public CategoriaAgregarViewModel(string accion)
        {
            Title = "Agregar Categoría";
            // Podés usar 'accion' más adelante si te hace falta
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task AgregarCategoria()
        {
            var categoria = new Categoria
            {
                IdCategoria = IdCategoria,
                NombreCategoria = NombreCategoria,
                DescripcionCategoria = DescripcionCategoria
            };

            try
            {
                await ApiService.AgregarCategoria(categoria);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Se agregó una nueva categoría.", "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar la categoría.", "Aceptar");
            }
        }
    }
}
