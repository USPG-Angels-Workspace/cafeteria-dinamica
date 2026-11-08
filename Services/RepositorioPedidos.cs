using System.Globalization;
using CafeteriaDinamica.Models;

namespace CafeteriaDinamica.Services;

/// <summary>
/// Conecta la cola dinamica con el archivo de texto Data/pedidos.txt
/// (una linea por pedido: numero|cliente|producto).
/// </summary>
public class RepositorioPedidos
{
    private const char Separador = '|';

    private readonly string _archivoTxt;
    private readonly object _candado = new();
    private int _siguienteNumero = 1;

    public Cola Cola { get; } = new Cola();

    public RepositorioPedidos(IWebHostEnvironment env)
    {
        string carpeta = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(carpeta);
        _archivoTxt = Path.Combine(carpeta, "pedidos.txt");
        Cargar();
    }

    /// <summary>Lee el txt y encola cada pedido en el orden del archivo.</summary>
    private void Cargar()
    {
        if (!File.Exists(_archivoTxt)) return;

        foreach (string linea in File.ReadAllLines(_archivoTxt))
        {
            string[] partes = linea.Split(Separador);
            if (partes.Length != 3) continue;                                   // linea mal formada
            if (!int.TryParse(partes[0], NumberStyles.None, CultureInfo.InvariantCulture, out int numero)) continue;

            string cliente = partes[1].Trim();
            string producto = partes[2].Trim();
            if (cliente.Length == 0 || producto.Length == 0) continue;

            Cola.Encolar(new Pedido(numero, cliente, producto));
            if (numero >= _siguienteNumero) _siguienteNumero = numero + 1;
        }
    }

    /// <summary>Vuelca la cola (de frente a final) al txt para que los cambios persistan.</summary>
    private void Guardar()
    {
        File.WriteAllLines(_archivoTxt, Cola.Recorrer()
            .Select(p => $"{p.Numero}{Separador}{p.Cliente}{Separador}{p.Producto}"));
    }

    /// <summary>Crea un pedido con el siguiente numero, lo encola y lo guarda en el txt.</summary>
    public Pedido Registrar(string cliente, string producto)
    {
        lock (_candado)
        {
            var pedido = new Pedido(_siguienteNumero++, cliente, producto);
            Cola.Encolar(pedido);
            Guardar();
            return pedido;
        }
    }

    /// <summary>Atiende (elimina) el pedido del frente y actualiza el txt. Null si no hay pedidos.</summary>
    public Pedido? Atender()
    {
        lock (_candado)
        {
            Pedido? atendido = Cola.Desencolar();
            if (atendido != null) Guardar();
            return atendido;
        }
    }
}
