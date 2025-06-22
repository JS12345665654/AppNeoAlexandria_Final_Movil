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
    public partial class ValoraciondeUsuariosAgregarViewModel : BaseViewModel
    {
        [ObservableProperty] private int _IdValoracion;
        [ObservableProperty] private int _IdUsuario;
        [ObservableProperty] private int _IdLibro;
        [ObservableProperty] private string _Valoracion;
        [ObservableProperty] private int _Puntuacion;

        public ValoraciondeUsuariosAgregarViewModel()
        {
            Title = Constants.AppName;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task AgregarValoracion(ValoraciondeUsuario valoraciondeUsuarios)
        {
            var valoracion = new ValoraciondeUsuario
            {
                IdValoracion = this._IdValoracion,
                IdUsuario = this._IdUsuario,
                IdLibro = this._IdLibro,
                Valoracion = this._Valoracion,
                Puntuacion = this._Puntuacion
            };

            try
            {
                await ApiService.AgregarValoracion(valoraciondeUsuarios);

                await Application.Current.MainPage.DisplayAlert("Exito", "Se nuevo agrego una nueva valoracion.", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
            }

            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
