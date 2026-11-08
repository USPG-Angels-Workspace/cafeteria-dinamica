namespace CafeteriaDinamica.Models;

/// <summary>
/// Cola dinamica (FIFO) de pedidos implementada con nodos enlazados.
/// Head es el frente (el que se atiende primero) y Tail el final (donde
/// llegan los nuevos); si ambos son null la cola esta vacia.
/// </summary>
public class Cola
{
    private Nodo? Head;
    private Nodo? Tail;

    /// <summary>Cantidad de pedidos pendientes.</summary>
    public int Count { get; private set; }

    public bool EstaVacia => Head == null;

    /// <summary>Agrega un pedido al final de la cola.</summary>
    public void Encolar(Pedido dato)
    {
        Nodo nuevo = new Nodo(dato);

        if (Tail == null)
            Head = Tail = nuevo;          // cola vacia: el nodo es frente y final
        else
        {
            Tail.Next = nuevo;
            Tail = nuevo;
        }
        Count++;
    }

    /// <summary>Saca y devuelve el pedido del frente; null si la cola esta vacia.</summary>
    public Pedido? Desencolar()
    {
        if (Head == null) return null;

        Pedido atendido = Head.Dato;
        Head = Head.Next;
        if (Head == null) Tail = null;    // era el unico nodo
        Count--;
        return atendido;
    }

    /// <summary>Devuelve el pedido del frente sin sacarlo; null si la cola esta vacia.</summary>
    public Pedido? Frente() => Head?.Dato;

    /// <summary>Recorre la cola del frente al final.</summary>
    public IEnumerable<Pedido> Recorrer()
    {
        Nodo? actual = Head;
        while (actual != null)
        {
            yield return actual.Dato;
            actual = actual.Next;
        }
    }
}
