using Prueba.ViewModels;

namespace Prueba.Views.Notas;

public partial class NotasPage : ContentPage
{
    public NotasPage(int? idLibro = null)
    {
        InitializeComponent();
        BindingContext = new NotasViewModel(idLibro);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is NotasViewModel vm)
        {
            vm.ObtenerTodasNotasCommand.Execute(null);
        }
    }
}
