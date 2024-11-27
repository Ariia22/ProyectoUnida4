using Microsoft.AspNetCore.Mvc;
using ProyectoUnida4.Models;

namespace ProyectoUnida4.Controllers
{
    public class CursosController: Controller
    {
        private static List<Curso> Cursos = new List<Curso>
    {
        new Curso { Id = 1, Titulo = "HTML5, CSS3, JavaScript para Principiantes", Imagen = "/img/curso1.jpg", PrecioOriginal = 200, PrecioDescuento = 15, Instructor = "Juan Pedro" },
        new Curso { Id = 2, Titulo = "Curso de Comida Vegetariana", Imagen = "/img/curso2.jpg", PrecioOriginal = 200, PrecioDescuento = 15, Instructor = "Juan Pedro" },
        
    };

        private static List<CarritoItem> Carrito = new List<CarritoItem>();

        
        public IActionResult Index()
        {
            return View(Cursos);
        }

        
        [HttpPost]
        public IActionResult AgregarAlCarrito(int id)
        {
            var curso = Cursos.FirstOrDefault(c => c.Id == id);
            if (curso != null)
            {
                var itemExistente = Carrito.FirstOrDefault(c => c.Id == curso.Id);
                if (itemExistente != null)
                {
                    itemExistente.Cantidad++;
                }
                else
                {
                    Carrito.Add(new CarritoItem
                    {
                        Id = curso.Id,
                        Titulo = curso.Titulo,
                        Imagen = curso.Imagen,
                        Precio = curso.PrecioDescuento,
                        Cantidad = 1
                    });
                }
            }
            return RedirectToAction("Carrito");
        }

        
        public IActionResult Carrito()
        {
            return View(Carrito);
        }

        
        [HttpPost]
        public IActionResult VaciarCarrito()
        {
            Carrito.Clear();
            return RedirectToAction("Carrito");
        }

        
        [HttpPost]
        public IActionResult EliminarDelCarrito(int id)
        {
            var item = Carrito.FirstOrDefault(c => c.Id == id);
            if (item != null)
            {
                Carrito.Remove(item);
            }
            return RedirectToAction("Carrito");
        }
    }
}
