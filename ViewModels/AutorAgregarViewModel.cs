using Prueba.Models;
using Prueba.Utils;
using Prueba.ViewModels;
using Prueba.Views;
using Prueba;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Prueba.ViewModels
{
    public partial class AutorAgregarViewModel : BaseViewModel
    {
        [ObservableProperty] private int idAutor;
        [ObservableProperty] private string nombreAutor;
        [ObservableProperty] private string biografia;
        [ObservableProperty] private DateTime anioNacimiento;
        [ObservableProperty] private DateTime anioFallecimiento;

        [ObservableProperty] private string textoBoton;

        private readonly string _accion;

        public AutorAgregarViewModel(string accion)
        {
            _accion = accion;
            Title = "Agregar Autor";
            textoBoton = "Agregar";

            anioNacimiento = DateTime.Now;
            anioFallecimiento = DateTime.Now;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task AgregarAutor()
        {
            var autores = new Autores
            {
                IdAutor = this.idAutor,
                NombreAutor = this.nombreAutor,
                Biografia = this.biografia,
                AnioNacimiento = this.anioNacimiento,
                AnioFallecimiento = this.anioFallecimiento
            };

            try
            {
                await ApiService.AgregarAutor(autores);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Se agregó un nuevo autor.", "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
            }
        }
    }
}