using System;

namespace ColaConPrioridadApp
{
    // Clase que representa al elemento o cliente con su prioridad
    public class Cliente
    {
        public string Nombre { get; set; }
        public int Prioridad { get; set; } // 1 (Más alta), 2 (Media), 3 (Baja)

        public override string ToString()
        {
            return $"[Cliente: {Nombre} | Prioridad: {Prioridad}]";
        }
    }

    // Nodo para construir la Cola con Prioridad de forma dinámica
    public class NodoPrioridad
    {
        public Cliente Dato { get; set; }
        public NodoPrioridad Siguiente { get; set; }

        public NodoPrioridad(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }

    // Estructura de Cola con Prioridad
    public class ColaConPrioridad
    {
        private NodoPrioridad frente;
        private NodoPrioridad fin;
        public int Count { get; private set; }

        public ColaConPrioridad()
        {
            frente = null;
            fin = null;
            Count = 0;
        }

        // =========================================================================
        // ENQUEUE CON PRIORIDAD: Inserta ordenadamente según la regla solicitada
        // "Todos los que estén delante de él tengan mayor o igual prioridad"[cite: 15]
        // =========================================================================
        public void Enqueue(Cliente cliente)
        {
            NodoPrioridad nuevoNodo = new NodoPrioridad(cliente);

            // Caso 1: Si la cola está vacía, o si el nuevo elemento tiene MAYOR prioridad 
            // (número menor) que el frente actual, entra al principio de la cola.
            if (IsEmpty() || cliente.Prioridad < frente.Dato.Prioridad)
            {
                nuevoNodo.Siguiente = frente;
                frente = nuevoNodo;
                if (fin == null) fin = nuevoNodo; // Si era el único, fin también apunta a él
            }
            else
            {
                // Caso 2: Buscamos la posición correcta recorriendo la cola.
                // Nos detenemos antes de que el siguiente elemento tenga MENOR prioridad (número mayor).
                NodoPrioridad actual = frente;

                while (actual.Siguiente != null && actual.Siguiente.Dato.Prioridad <= cliente.Prioridad)
                {
                    actual = actual.Siguiente;
                }

                // Insertamos el nuevo nodo en la posición encontrada
                nuevoNodo.Siguiente = actual.Siguiente;
                actual.Siguiente = nuevoNodo;

                // Si quedó al final, actualizamos el puntero 'fin'
                if (nuevoNodo.Siguiente == null)
                {
                    fin = nuevoNodo;
                }
            }

            Count++;
        }

        // ==========================================
        // DEQUEUE: Extrae el elemento del frente (el de mayor prioridad)
        // ==========================================
        public Cliente Dequeue()
        {
            if (IsEmpty())
            {
                return null;
            }

            Cliente atendido = frente.Dato;
            frente = frente.Siguiente;
            Count--;

            if (frente == null)
            {
                fin = null;
            }

            return atendido;
        }

        // Método para verificar si está vacía
        public bool IsEmpty()
        {
            return frente == null;
        }

        // Método para mostrar el estado actual de la cola con sus prioridades
        public void MostrarCola()
        {
            if (IsEmpty())
            {
                Console.WriteLine("  (Cola con prioridad vacía)");
                return;
            }

            NodoPrioridad actual = frente;
            int pos = 1;
            while (actual != null)
            {
                Console.WriteLine($"  {pos}. {actual.Dato}");
                actual = actual.Siguiente;
                pos++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ColaConPrioridad colaPrioridad = new ColaConPrioridad();
            bool salir = false;

            // Cargamos algunos elementos de prueba con diferentes prioridades
            colaPrioridad.Enqueue(new Cliente { Nombre = "Roberto", Prioridad = 3 });
            colaPrioridad.Enqueue(new Cliente { Nombre = "Ana", Prioridad = 1 });
            colaPrioridad.Enqueue(new Cliente { Nombre = "Marcos", Prioridad = 2 });
            colaPrioridad.Enqueue(new Cliente { Nombre = "Elena", Prioridad = 1 });

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE COLA CON PRIORIDAD (1, 2 ó 3) ===");
                Console.WriteLine("Estado actual de la cola (Orden de atención - Frente arriba):");
                colaPrioridad.MostrarCola();

                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Agregar nuevo cliente con prioridad (`Enqueue` ordenado)[cite: 15]");
                Console.WriteLine("2. Atender / Extraer al siguiente cliente (`Dequeue`)");
                Console.WriteLine("3. Salir");
                Console.Write("\nSeleccione una opción (1-3): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el nombre del cliente: ");
                        string nombre = Console.ReadLine();

                        Console.Write("Ingrese la prioridad (1 = Alta, 2 = Media, 3 = Baja)[cite: 15]: ");
                        if (int.TryParse(Console.ReadLine(), out int prioridad) && (prioridad >= 1 && prioridad <= 3))
                        {
                            Cliente nuevoCliente = new Cliente
                            {
                                Nombre = string.IsNullOrEmpty(nombre) ? "Sin Nombre" : nombre,
                                Prioridad = prioridad
                            };

                            colaPrioridad.Enqueue(nuevoCliente);
                            Console.WriteLine("¡Cliente ubicado correctamente en la cola según su prioridad!");
                        }
                        else
                        {
                            Console.WriteLine("Prioridad inválida. Debe ser 1, 2 o 3.");
                        }
                        break;

                    case "2":
                        Cliente atendido = colaPrioridad.Dequeue();
                        if (atendido != null)
                        {
                            Console.WriteLine($"¡Atendiendo al cliente: {atendido}!");
                        }
                        else
                        {
                            Console.WriteLine("La cola está vacía, no hay clientes para atender.");
                        }
                        break;

                    case "3":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}