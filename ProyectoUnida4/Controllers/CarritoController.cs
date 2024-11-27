using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProyectoUnida4.Models;
using System.Text.Json.Serialization;

public class CarritoController : Controller
{
    private List<Curso> _articulosCarrito;

    public CarritoController()
    {
        _articulosCarrito = new List<Curso>();
    }

    [HttpPost]
    public IActionResult AgregarCurso([FromBody] Curso curso)
    {
        var cursoExistente = _articulosCarrito.FirstOrDefault(c => c.Id == curso.Id);

        if (cursoExistente != null)
        {
            cursoExistente.Cantidad++;
        }
        else
        {
            curso.Cantidad = 1;
            _articulosCarrito.Add(curso);
        }

        SincronizarStorage();
        return Json(_articulosCarrito);
    }

    [HttpPost]
    public IActionResult EliminarCurso([FromBody] string cursoId)
    {
        var curso = _articulosCarrito.FirstOrDefault(c => c.Id == cursoId);

        if (curso != null)
        {
            _articulosCarrito.Remove(curso);
        }

        SincronizarStorage();
        return Json(_articulosCarrito);
    }

    [HttpPost]
    public IActionResult VaciarCarrito()
    {
        _articulosCarrito.Clear();
        SincronizarStorage();
        return Json(_articulosCarrito);
    }

    [HttpGet]
    public IActionResult ObtenerCarrito()
    {
        var carrito = HttpContext.Session.GetString("Carrito");

        if (!string.IsNullOrEmpty(carrito))
        {
            _articulosCarrito = JsonConvert.DeserializeObject<List<Curso>>(carrito);
        }

        return Json(_articulosCarrito);
    }

    private void SincronizarStorage()
    {
        HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(_articulosCarrito));
    }
}

