using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models;

public partial class Carrito
{
    [Key]
    public int IdCarrito { get; set; }

    [ForeignKey("Usuario")]
    public int IdUsuario { get; set; }

    public decimal PrecioTotalCarrito { get; set; }

    public DateTime FechaCreacion { get; set; }

    public string Descripcion { get; set; } = null!;

    public bool Estado { get; set; }

}
