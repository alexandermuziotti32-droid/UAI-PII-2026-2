using System;

namespace AlmacenContenedoresPilas
{
    // Clase que representa el contenedor con su número de identificación
    public class Contenedor
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return $"[Contenedor ID: {Id}]";
        }
    }

    // Nodo para construir nuestra propia Pila de forma dinámica
    public class NodoPila
    {
        public Contenedor Dato { get; set; }
        public NodoPila Siguiente { get; set; }

        public NodoPila(Contenedor contenedor)
        {
            Dato = contenedor;
            Siguiente = null;
        }
    }

    // Estructura de Pila personalizada donde codificamos Push() y Pop()
    public class PilaContenedores
    {
        private NodoPila cima; // Referencia al elemento superior de la pila
        public int Count { get; private set; }

        public PilaContenedores()
        {
            cima = null;
            Count = 0;
        }

        // ==========================================
        // Método PUSH(): Agrega un elemento a la cima de la pila
        // ==========================================
        public void Push(Contenedor contenedor)
        {
            NodoPila nuevoNodo = new NodoPila(contenedor);
            nuevoNodo.Siguiente = cima; // El nuevo apunta al que era la cima anterior
            cima = nuevoNodo;           // La cima pasa a ser el nuevo nodo
            Count++;
        }

        // ==========================================
        // Método POP(): Remueve y devuelve el elemento de la cima de la pila
        // ==========================================
        public Contenedor Pop()
        {
            if (IsEmpty())
            {
                return null; // La pila está vacía (Underflow)
            }

            Contenedor datoRetornado = cima.Dato;
            cima = cima.Siguiente; // La cima baja al siguiente nodo
            Count--;
            return datoRetornado;
        }

        // Método Peek(): Permite ver el elemento de la cima sin sacarlo
        public Contenedor Peek()
        {
            if (IsEmpty()) return null;
            return cima.Dato;
        }

        // Método para verificar si la pila está vacía
        public bool IsEmpty()
        {
            return cima == null;
        }

        // Método para mostrar el contenido de la pila desde la cima hacia abajo
        public void MostrarPila()
        {
            if (IsEmpty())
            {
                Console.WriteLine("(Pila vacía)");
                return;
            }

            NodoPila actual = cima;
            int nivel = 1;
            while (actual != null)
            {
                Console.WriteLine($"  Nivel {nivel}: {actual.Dato}");
                actual = actual.Siguiente;
                nivel++;
            }
        }

        // Método para buscar si un ID existe en la pila
        bool Existe(int id)
        {
            NodoPila actual = cima;
            while (actual != null)
            {
                if (actual.Dato.Id == id) return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        // =========================================================================
        // Retirar un contenedor específico del almacén
        // Utilizando una pila auxiliar para mover los que están encima, retirar y regresar.
        // =========================================================================
        public bool RetirarContenedorEspecifico(int idBuscado)
        {
            if (IsEmpty())
            {
                Console.WriteLine("El almacén está vacío.");
                return false;
            }

            // Verificamos si el contenedor existe en la pila
            if (!Existe(idBuscado))
            {
                Console.WriteLine($"No se encontró ningún contenedor con el ID {idBuscado} en el almacén.");
                return false;
            }

            PilaContenedores pilaAuxiliar = new PilaContenedores();
            bool encontrado = false;

            // 1. Desapilamos de la principal y pasamos a la auxiliar hasta encontrar el objetivo
            while (!IsEmpty())
            {
                Contenedor actual = Pop(); // Usamos nuestro método Pop codificado manualmente

                if (actual.Id == idBuscado)
                {
                    Console.WriteLine($"\n ¡Contenedor encontrado y retirado con éxito: {actual}!");
                    encontrado = true;
                    break; // Detenemos el ciclo porque ya lo encontramos y sacamos
                }
                else
                {
                    Console.WriteLine($"-> Moviendo temporalmente a pila auxiliar: {actual}");
                    pilaAuxiliar.Push(actual); // Usamos nuestro método Push codificado manualmente
                }
            }

            // 2. Regresamos los contenedores desde la pila auxiliar de vuelta a la principal
            Console.WriteLine("\n--- Regresando los contenedores superiores a la pila principal ---");
            while (!pilaAuxiliar.IsEmpty())
            {
                Contenedor devuelto = pilaAuxiliar.Pop();
                Push(devuelto);
                Console.WriteLine($"<- Regresando al almacén: {devuelto}");
            }

            return encontrado;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PilaContenedores almacen = new PilaContenedores();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== ALMACÉN DE CONTENEDORES (GESTIÓN CON PILAS) ===");
                Console.WriteLine("Estado actual del almacén (Cima arriba):");
                almacen.MostrarPila();

                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Apilar un nuevo contenedor (`Push`)");
                Console.WriteLine("2. Retirar contenedor específico (con pila auxiliar)");
                Console.WriteLine("3. Ver contenedor en la cima (`Peek / Pop`)");
                Console.WriteLine("4. Salir");
                Console.Write("\nSeleccione una opción (1-4): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el número de ID del contenedor (entero): ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            Contenedor nuevo = new Contenedor { Id = id };
                            almacen.Push(nuevo);
                            Console.WriteLine("¡Contenedor apilado con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case "2":
                        Console.Write("Ingrese el ID del contenedor que desea retirar del almacén: ");
                        if (int.TryParse(Console.ReadLine(), out int idRetiro))
                        {
                            almacen.RetirarContenedorEspecifico(idRetiro);
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                        break;
                    case "3":
                        Contenedor cimaActual = almacen.Peek();
                        if (cimaActual != null)
                        {
                            Console.WriteLine($"El contenedor en la cima es: {cimaActual}");
                        }
                        else
                        {
                            Console.WriteLine("El almacén está vacío.");
                        }
                        break;
                    case "4":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema de contenedores. ¡Hasta luego!");
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