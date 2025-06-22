using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prueba.Models;

public partial class Usuario
{
    [Key]
    public int IdUsuario { get; set; }

    public string? Nombre { get; set; }
    public string? Email { get; set; }
    public string? Contrasenia { get; set; }
    public string? Imagen { get; set; }
    public string? CategoriaPreferida { get; set; }

    public string? IdRol { get; set; }

    [JsonPropertyName("rol")]
    public string? RolDesdeApi { get; set; }

    public string NombreRol => (RolDesdeApi)?.Trim().ToUpper() switch
    {
        "ADMINISTRADOR" => "Administrador",
        "USUARIO" => "Usuario",
        "0" => "Administrador",
        "1" => "Usuario",
        _ => "Desconocido"
    };
    public bool Activo { get; set; }
}