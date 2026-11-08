using CafeteriaDinamica.Models;
using CafeteriaDinamica.Services;
using Microsoft.AspNetCore.Mvc;

namespace CafeteriaDinamica.Controllers;

public class PedidosController : Controller
{
    private const int LongitudMaxima = 60;

    private readonly RepositorioPedidos _repo;

    public PedidosController(RepositorioPedidos repo)
    {
        _repo = repo;
    }

    private void Aviso(string mensaje, bool error = false)
    {
        TempData["Mensaje"] = mensaje;
        TempData["Error"] = error;
    }

    // Panel principal: cantidad de pendientes, siguiente pedido y lista completa en orden de llegada.
    public IActionResult Index()
    {
        ViewBag.Pendientes = _repo.Cola.Count;
        ViewBag.Siguiente = _repo.Cola.Frente();
        return View(_repo.Cola.Recorrer().ToList());
    }

    [HttpGet]
    public IActionResult Registrar() => View();

    [HttpPost]
    public IActionResult Registrar(string? cliente, string? producto)
    {
        cliente = (cliente ?? "").Trim();
        producto = (producto ?? "").Trim();

        ViewBag.Cliente = cliente;
        ViewBag.Producto = producto;

        if (cliente.Length == 0 || producto.Length == 0)
        { Aviso("El nombre del cliente y el producto son obligatorios.", true); return View(); }
        if (cliente.Length > LongitudMaxima || producto.Length > LongitudMaxima)
        { Aviso($"Cada campo admite máximo {LongitudMaxima} caracteres.", true); return View(); }
        if (cliente.Contains('|') || producto.Contains('|'))
        { Aviso("El carácter '|' no está permitido.", true); return View(); }

        Pedido pedido = _repo.Registrar(cliente, producto);
        Aviso($"Pedido #{pedido.Numero} registrado para {pedido.Cliente}.");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Atender()
    {
        Pedido? atendido = _repo.Atender();
        if (atendido == null) Aviso("No hay pedidos pendientes para atender.", true);
        else Aviso($"Pedido #{atendido.Numero} atendido: {atendido.Producto} para {atendido.Cliente}.");
        return RedirectToAction(nameof(Index));
    }
}
