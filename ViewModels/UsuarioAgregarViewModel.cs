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
    public partial class UsuarioAgregarViewModel : BaseViewModel
    {
        [ObservableProperty] private int _IdUsuario;
        [ObservableProperty] private string _Nombre;
        [ObservableProperty] private string _Email;
        [ObservableProperty] private string _Contrasenia;
        [ObservableProperty] private string _Imagen;
        [ObservableProperty] private string _CategoriaPreferida;
        [ObservableProperty] private string _IdRol;
        [ObservableProperty] private bool _Activo;

        public UsuarioAgregarViewModel()
        {
            Title = Constants.AppName;
        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        [RelayCommand]
        private async Task CrearUsuarios(Usuario usuarios)
        {
            var usuario = new Usuario
            {
                IdUsuario = this._IdUsuario,
                Nombre = this._Nombre,
                Email = this._Email,
                Contrasenia = this._Contrasenia,
                Imagen = this._Imagen,
                CategoriaPreferida = this._CategoriaPreferida,
                IdRol = this._IdRol,
                Activo = this._Activo
            };

            try
            {
                await ApiService.CrearUsuarios(usuarios);

                await Application.Current.MainPage.DisplayAlert("Exito", "Se nuevo agrego un nuevo Usuario.", "Aceptar");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar.", "Aceptar");
            }

            await Application.Current.MainPage.Navigation.PopAsync();

        }
    }
}
