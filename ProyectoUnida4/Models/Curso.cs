namespace ProyectoUnida4.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Imagen { get; set; }
        public decimal PrecioOriginal { get; set; }
        public decimal PrecioDescuento { get; set; }
        public string Instructor { get; set; }
    }
}
