using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models;

public partial class Libros
{
    [Key]
    public int IdLibro { get; set; }

    [ForeignKey("Categorias")]
    public int IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int? Stock { get; set; }

    public DateTime AniodePublicacion { get; set; }

    public string? Imagen { get; set; }

    [ForeignKey("Autores")]
    public int IdAutor { get; set; }
    public string? NombreAutor { get; set; }
}
