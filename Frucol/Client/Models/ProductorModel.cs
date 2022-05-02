using System;
namespace Frucol.Client.Models
{
    public class ProductorModel
    {
        public ProductorModel()
        {
        }

        public int? ProductorId { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Cedula { get; set; }
        public DateTime? AnioNacimiento { get; set; }
        public string? TipoEdad { get; set; }
        public int? Edad { get; set; }
        public string? Genero { get; set; }
    }
}
