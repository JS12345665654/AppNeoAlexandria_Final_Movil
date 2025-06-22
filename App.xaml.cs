using Prueba.ViewModels;
using Prueba.Views.Autor;
using Prueba.Views.Libro;
using Prueba.Views.Carrito;
using Prueba.Views.Categoria;
using Prueba.Views.Usuario;
using Prueba.Views;
using System.IdentityModel.Tokens.Jwt;

namespace Prueba
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            string token = Preferences.Get("Token", string.Empty);
            string idRol = Preferences.Get("IdRol", string.Empty);

            if (!string.IsNullOrEmpty(token) && !EstaTokenExpirado(token))
            {
                // Token válido: restaurar estado
                Transport.IdRol = idRol;
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                // Token inválido o no presente
                MainPage = new NavigationPage(new LoginyRegistroPage());
            }
        }

        private bool EstaTokenExpirado(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);

                var fechaExp = jwt.ValidTo.ToLocalTime();
                return fechaExp < DateTime.Now;
            }
            catch
            {
                // Si falla al leer el token, lo tratamos como expirado
                return true;
            }
        }
    }
}