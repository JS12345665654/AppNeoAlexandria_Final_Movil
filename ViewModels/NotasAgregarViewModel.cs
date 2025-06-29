using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class NotasAgregarViewModel : BaseViewModel
{
    [ObservableProperty]
    private string textoNota;

    private readonly int _idLibro;

    public NotasAgregarViewModel(int idLibro)
    {
        Title = "Agregar Nota";
        _idLibro = idLibro;
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task AgregarNota()
    {
        var nota = new Notas
        {
            IdUsuario = Transport.IdUsuario,
            IdLibro = _idLibro,
            TextoNota = TextoNota
        };

        try
        {
            await ApiService.AgregarNota(nota);
            await Application.Current.MainPage.DisplayAlert("Éxito", "Se agregó una nueva Nota.", "Aceptar");
        }
        catch
        {
            await Application.Current.MainPage.DisplayAlert("Error", "ERROR al grabar la nota.", "Aceptar");
        }

        await Application.Current.MainPage.Navigation.PopAsync();
    }
}