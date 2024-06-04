namespace Inventario.Backend.Models
{
    public class Operacion
    {
        public int Id { get; set; }

        public string Tipo { get; set; } = null!;

        public string Producto { get; set;} = null!;

        public DateTime Fecha { get; set; }

        public int Cantidad { get; set; }

        public int Stock { get; set; }

    }
}
