using System.ComponentModel.DataAnnotations;

namespace Haulmer_Comercio.Entidades
{
    public class Comercios
    {
        public int id { get; set; }
        [Required]
        public string rut { get; set; }
        [Required]
        public string nombre { get; set; }
        public int puntos { get; set; }
        public List<Ventas> ventas { get; set; }
    }
}
