using System.ComponentModel.DataAnnotations;

namespace Prueba.Models;

public partial class Categoria
{
    [Key]
    public int IdCategoria { get; set; }

    public string NombreCategoria { get; set; } = null!;

    public string DescripcionCategoria { get; set; } = null!;
}
