using Microsoft.Extensions.Logging;
using Prueba.ViewModels;
using Prueba.Views.Autor;
using Prueba.Views.Libro;
using Prueba.Views.Carrito;
using Prueba.Views.Usuario;
using Prueba.Views.Categoria;
using Prueba.Views.Notas;
using Prueba.Views.ValoraciondeUsuarios;
using Prueba.Views;
using Prueba.Utils;
using Prueba.Models;
using Prueba;



namespace Prueba
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "MaterialIconsRegular");
                });

            //Inicializadores 'Singleton de la clase "Api- Service" '
            builder.Services.AddSingleton<ApiService>();

            //Inicializamos todas las partes de cada sección (ViewModels)

            //Sección Login
            builder.Services.AddTransient<LoginyRegistroViewModel>();

            //Sección Libros
            builder.Services.AddTransient<LibroViewModel>();
            builder.Services.AddTransient<LibroDetalleViewModel>();
            builder.Services.AddTransient<LibroAgregarViewModel>();

            //Sección Autor
            builder.Services.AddTransient<AutorViewModel>();
            builder.Services.AddTransient<AutorDetalleViewModel>();
            builder.Services.AddTransient<AutorAgregarViewModel>();

            //Sección Carrito (carrito detalle se incluye dentro porque es una extensión del carrito)   
            builder.Services.AddTransient<CarritoViewModel>();
            builder.Services.AddTransient<CarritoDetalleViewModel>();
            builder.Services.AddTransient<CarritoAgregarViewModel>();

            //Sección Categoria
            builder.Services.AddTransient<CategoriaViewModel>();
            builder.Services.AddTransient<CategoriaAgregarViewModel>();

            //Sección Usuarios
            builder.Services.AddTransient<UsuarioViewModel>();
            builder.Services.AddTransient<UsuarioDetalleViewModel>();
            builder.Services.AddTransient<UsuarioAgregarViewModel>();

            //Sección Notas
            builder.Services.AddTransient<NotasViewModel>();
            builder.Services.AddTransient<NotaDetalleViewModel>();
            builder.Services.AddTransient<NotasAgregarViewModel>();

            //Sección Valoración de los Usuarios
            builder.Services.AddTransient<ValoraciondeUsuariosViewModel>();
            builder.Services.AddTransient<ValoraciondeUsuariosDetalleViewModel>();
            builder.Services.AddTransient<ValoraciondeUsuariosAgregarViewModel>();

            //Sección Login
            builder.Services.AddTransient<LoginyRegistroPage>();

            //A cerca Page
            builder.Services.AddTransient<AcercaPage>();

            //Inicializamos todas las partes de cada sección (Views)

            //Sección Autor
            builder.Services.AddTransient<AutorPage>();
            builder.Services.AddTransient<AutorAgregarPage>();
            builder.Services.AddTransient<AutorDetallePage>();

            //Sección Libros
            builder.Services.AddTransient<LibroPage>();
            builder.Services.AddTransient<LibroAgregarPage>();
            builder.Services.AddTransient<LibroDetallePage>();

            //Sección Carrito
            builder.Services.AddTransient<CarritoPage>();
            builder.Services.AddTransient<CarritoDetallePage>();
            builder.Services.AddTransient<CarritoAgregarPage>();

            //Sección Usuario
            builder.Services.AddTransient<UsuarioPage>();
            builder.Services.AddTransient<UsuarioDetallePage>();

            //Sección Categoría
            builder.Services.AddTransient<CategoriaPage>();
            builder.Services.AddTransient<CategoriaDetallePage>();
            builder.Services.AddTransient<CategoriaAgregarPage>();

            //Sección Notas
            builder.Services.AddTransient<NotasPage>();
            builder.Services.AddTransient<NotasDetallePage>();
            builder.Services.AddTransient<NotasAgregarPage>();

            //Sección Valoración de Usuarios
            builder.Services.AddTransient<ValoraciondeUsuariosPage>();
            builder.Services.AddTransient<ValoraciondeUsuariosAgregarPage>();
            builder.Services.AddTransient<ValoraciondeUsuariosDetallePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
