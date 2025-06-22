using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba
{
    public class Transport
    {
        public static int IdUsuario { get; set; }
        public static string Nombre { get; set; } = string.Empty;
        public static string Email { get; set; } = string.Empty;
        public static string IdRol { get; set; } = string.Empty;
        public static bool EsAdministrador => IdRol == "Administrador";
    }
}
