using System.ComponentModel.DataAnnotations;

namespace L01_2020ZR601.Models
{
    public class clientes
    {
        [Key]
        public int clienteId { get; set; }
        public String? nombreCliente { get; set; }
        public String? direccion { get; set; }
    }
}
