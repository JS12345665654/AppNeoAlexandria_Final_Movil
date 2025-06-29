using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models;

public partial class Notas
{
    public int IdNota { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("Libros")]
    public int IdLibro { get; set; }
    [NotMapped] public string? NombreUsuario { get; set; }
    [NotMapped] public string? NombreLibro { get; set; }
    public string? TextoNota { get; set; }
}