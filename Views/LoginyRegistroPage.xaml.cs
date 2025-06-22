using Prueba;
using Prueba.ViewModels;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace Prueba.Views;

public partial class LoginyRegistroPage : ContentPage
{
    LoginyRegistroViewModel ViewModel;
    public LoginyRegistroPage()
    {
        InitializeComponent();
        BindingContext = ViewModel = new LoginyRegistroViewModel();
    }
}