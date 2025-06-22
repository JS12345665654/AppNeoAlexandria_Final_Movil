using Prueba.Models;
using Prueba.Utils;
using Prueba;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Prueba.ViewModels
{
    public partial class LibroAgregarViewModel : BaseViewModel
    {
        [ObservableProperty] private int _IdLibro;
        [ObservableProperty] private int _IdCategoria;
        [ObservableProperty] private string _Nombre;
        [ObservableProperty] private string _Descripcion;
        [ObservableProperty] private decimal _Precio;
        [ObservableProperty] private int _Stock;
        [ObservableProperty] private DateTime _AniodePublicacion;
        [ObservableProperty] private string _Imagen;
        [ObservableProperty] private int _IdAutor;

        public LibroAgregarViewModel()
        {
            Title = Constants.AppName;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task CrearLibro()
        {
            var crearLibro = new Libros
            {
                IdLibro = this._IdLibro,
                IdCategoria = this._IdCategoria,
                Nombre = this._Nombre,
                Descripcion = this._Descripcion,
                Precio = this._Precio,
                Stock = this._Stock,
                AniodePublicacion = this._AniodePublicacion,
                Imagen = this._Imagen,
                IdAutor = this._IdAutor
            };

            try
            {
                await ApiService.CrearLibro(crearLibro);

                await Application.Current.MainPage.DisplayAlert("Exito", "Se creo un nuevo libro.", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al guardar .", "Aceptar");
            }

            await Application.Current.MainPage.Navigation.PopAsync();

        }

    }
}