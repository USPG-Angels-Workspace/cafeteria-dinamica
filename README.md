# Cafetería dinámica

Hoja de Trabajo de Colas — Programación II (Universidad de San Pablo).

Aplicación web ASP.NET Core MVC (C#) que organiza los pedidos de una cafetería con una **cola dinámica (FIFO)** hecha con nodos enlazados (`Models/Nodo.cs`, `Models/Cola.cs`). Cada pedido guarda número, cliente y producto, y se atiende en el orden en que llegó.

- **Registrar** un nuevo pedido (el número se asigna solo).
- **Atender** el siguiente pedido.
- **Mostrar** todos los pedidos pendientes.
- **Consultar** cuál es el siguiente pedido.
- **Mostrar** cuántos pedidos están pendientes.

Los cambios se guardan en `Data/pedidos.txt` (una línea por pedido: `numero|cliente|producto`) y la cola se recarga desde ese archivo al iniciar.

## Ejecutar

```bash
dotnet run
```

Abrir <http://localhost:5081>.
