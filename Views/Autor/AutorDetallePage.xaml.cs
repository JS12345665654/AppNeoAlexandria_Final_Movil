using Prueba.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using Prueba.ViewModels;

namespace Prueba.Views.Autor
{
    public partial class AutorDetallePage : ContentPage
    {
        AutorDetalleViewModel ViewModel;
        public AutorDetallePage(Autores autor, string accion)
        {
            InitializeComponent();
            BindingContext = ViewModel = new AutorDetalleViewModel(autor, accion);
        }
    }
}