namespace Inventario.Backend.Models
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
    }
}
