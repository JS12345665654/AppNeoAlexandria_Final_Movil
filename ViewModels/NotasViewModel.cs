using Prueba.Models;
using Prueba.Utils;
using Prueba.Views.Notas;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Prueba.ViewModels;

public partial class NotasViewModel : BaseViewModel
{
    [ObservableProperty] private ObservableCollection<Notas> notas;
    [ObservableProperty] private Notas notaSeleccionada;
    [ObservableProperty] private bool isRefreshing;
    private readonly int? _idLibro;

    public NotasViewModel(int? idLibro = null)
    {
        _idLibro = idLibro;
        Notas = new ObservableCollection<Notas>();
        NotaSeleccionada = new Notas();
        Title = Constants.AppName;

        Task.Run(async () => { await ObtenerTodasNotas(); }).Wait();
    }

    [RelayCommand]
    private async Task Cancelar()
    {
        await Application.Current.MainPage.Navigation.PopAsync();
    }

    [RelayCommand]
    private async Task ObtenerTodasNotas()
    {
        IsBusy = isRefreshing = true;

        var lista = await ApiService.ObtenerTodasNotas();

        if (lista != null)
        {
            if (_idLibro.HasValue)
                lista = lista.Where(n => n.IdLibro == _idLibro.Value).ToList();

            Notas = new ObservableCollection<Notas>(lista);
        }

        IsBusy = isRefreshing = false;
    }

    [RelayCommand]
    private async Task GoToNotaDetalle()
    {
        if (NotaSeleccionada == null) return;
        await Application.Current.MainPage.Navigation.PushAsync(
            new NotasDetallePage(NotaSeleccionada, "Detalle"));
    }

    [RelayCommand]
    private async Task GoToNotaModificar()
    {
        if (NotaSeleccionada == null) return;
        await Application.Current.MainPage.Navigation.PushAsync(
            new NotasDetallePage(NotaSeleccionada, "Modificar"));
    }

    [RelayCommand]
    private async Task GoToNotaEliminar()
    {
        if (NotaSeleccionada == null) return;
        await Application.Current.MainPage.Navigation.PushAsync(
            new NotasDetallePage(NotaSeleccionada, "Eliminar"));
    }

    [RelayCommand]
    private async Task GoToNotaAgregar()
    {
        await Application.Current.MainPage.Navigation.PushAsync(
            new NotasAgregarPage());
    }
}
