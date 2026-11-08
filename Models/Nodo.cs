namespace CafeteriaDinamica.Models;

/// <summary>
/// Nodo de la cola dinamica: guarda un pedido y una referencia (Next)
/// al nodo que esta detras de el en la fila.
/// </summary>
public class Nodo
{
    public Pedido Dato;
    public Nodo? Next;

    public Nodo(Pedido dato)
    {
        Dato = dato;
        Next = null;
    }
}
