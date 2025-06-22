using System;
using Prueba.ViewModels;

namespace Prueba.Views.Autor;

public partial class AutorAgregarPage : ContentPage
{
    AutorAgregarViewModel ViewModels;
    public AutorAgregarPage(string accion)
	{
		InitializeComponent();
        BindingContext = ViewModels = new AutorAgregarViewModel(accion);
    }
}