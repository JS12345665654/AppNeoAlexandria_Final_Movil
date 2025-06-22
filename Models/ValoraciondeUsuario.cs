using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models;

public partial class ValoraciondeUsuario
{
    [Key]
    public int IdValoracion { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    [ForeignKey("Libros")]
    public int IdLibro { get; set; }

    public string? Valoracion { get; set; }

    public int? Puntuacion { get; set; }
}
