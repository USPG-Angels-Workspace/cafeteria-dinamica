namespace CafeteriaDinamica.Models;

/// <summary>
/// Dato que guarda cada nodo de la cola: numero de pedido, nombre del
/// cliente y producto solicitado.
/// </summary>
public class Pedido
{
    public int Numero { get; set; }
    public string Cliente { get; set; }
    public string Producto { get; set; }

    public Pedido(int numero, string cliente, string producto)
    {
        Numero = numero;
        Cliente = cliente;
        Producto = producto;
    }
}
