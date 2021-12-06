using System.ComponentModel.DataAnnotations;

namespace Haulmer_Comercio.Entidades
{
    public class Ventas
    {
        public int id { get; set; }
        public int monto { get; set; }
        public string idDispositivo { get; set; }
        public int ComercioId { get; set; }
        [Required]
        public string codigoSeguridad { get; set; }
        public Comercios Comercio {  get; set; }    

    }
}
