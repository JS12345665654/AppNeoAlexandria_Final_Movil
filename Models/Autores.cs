using System.ComponentModel.DataAnnotations;

namespace Prueba.Models;

public partial class Autores
{
    [Key]
    public int IdAutor { get; set; }

    public string? NombreAutor { get; set; }

    public string? Biografia { get; set; }

    public DateTime AnioNacimiento { get; set; }

    public DateTime? AnioFallecimiento { get; set; }
}
