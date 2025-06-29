using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    public partial class ValoraciondeUsuario : INotifyPropertyChanged
    {
        [Key]
        public int IdValoracion { get; set; }

        [ForeignKey("Usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Libros")]
        public int IdLibro { get; set; }

        private string? _valoracion;
        public string? Valoracion
        {
            get => _valoracion;
            set
            {
                if (_valoracion != value)
                {
                    _valoracion = value;
                    OnPropertyChanged(nameof(Valoracion));
                }
            }
        }

        private int? _puntuacion;
        public int? Puntuacion
        {
            get => _puntuacion;
            set
            {
                if (_puntuacion != value)
                {
                    _puntuacion = value;
                    OnPropertyChanged(nameof(Puntuacion));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        [NotMapped]
        public string NombreUsuario { get; set; } = string.Empty;
    }
}
