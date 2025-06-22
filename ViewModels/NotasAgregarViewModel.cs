using Prueba.Models;
using Prueba.Utils;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Prueba.ViewModels;

public partial class NotasAgregarViewModel : BaseViewModel
{
    [ObservableProperty] private int idNota;
    [ObservableProperty] private int idUsuario;
    [ObservableProperty] private int idLibro;
    [ObservableProperty] private string textoNota;

    public NotasAgregarViewModel()
    {
        Title = "Agregar Nota";
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
            IdNota = IdNota,
            IdUsuario = IdUsuario,
            IdLibro = IdLibro,
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
