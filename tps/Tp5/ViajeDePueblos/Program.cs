using System;

namespace ViajePueblosPilas
{
    // Clase que representa cada pueblo
    public class Pueblo
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return $"[Pueblo: {Nombre}]";
        }
    }

    // Nodo para construir nuestra propia Pila de forma dinámica
    public class NodoPilaPueblo
    {
        public Pueblo Dato { get; set; }
        public NodoPilaPueblo Siguiente { get; set; }

        public NodoPilaPueblo(Pueblo pueblo)
        {
            Dato = pueblo;
            Siguiente = null;
        }
    }

    // Estructura de Pila personalizada codificando Push() y Pop()
    public class PilaPueblos
    {
        private NodoPilaPueblo cima;
        public int Count { get; private set; }

        public PilaPueblos()
        {
            cima = null;
            Count = 0;
        }

        // ==========================================
        // Método PUSH(): Agrega un pueblo a la cima
        // ==========================================
        public void Push(Pueblo pueblo)
        {
            NodoPilaPueblo nuevoNodo = new NodoPilaPueblo(pueblo);
            nuevoNodo.Siguiente = cima;
            cima = nuevoNodo;
            Count++;
        }

        // ==========================================
        // Método POP(): Extrae el pueblo de la cima
        // ==========================================
        public Pueblo Pop()
        {
            if (IsEmpty())
            {
                return null;
            }

            Pueblo datoRetornado = cima.Dato;
            cima = cima.Siguiente;
            Count--;
            return datoRetornado;
        }

        // Método para verificar si está vacía
        public bool IsEmpty()
        {
            return cima == null;
        }

        // Método para mostrar los elementos desde la base hasta la cima (orden de ida)
        public void MostrarRutaIda()
        {
            if (IsEmpty())
            {
                Console.WriteLine("(No hay ruta registrada)");
                return;
            }

            // Para mostrar desde el origen hasta el destino (base a cima), 
            // pasamos los elementos a un vector temporal o los invertimos visualmente.
            Pueblo[] arrayPueblos = new Pueblo[Count];
            NodoPilaPueblo actual = cima;
            int index = Count - 1;

            while (actual != null)
            {
                arrayPueblos[index] = actual.Dato;
                actual = actual.Siguiente;
                index--;
            }

            for (int i = 0; i < arrayPueblos.Length; i++)
            {
                Console.WriteLine($"  Parada {i + 1}: {arrayPueblos[i].Nombre}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PilaPueblos pilaIda = new PilaPueblos();
            bool rutaRegistrada = false;
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE VIAJE ENTRE PUEBLOS (PILAS) ===");
                Console.WriteLine("1. Registrar viaje de IDAE (ingresar pueblos en orden)");
                Console.WriteLine("2. Mostrar trayecto de IDA y VUELTA automático");
                Console.WriteLine("3. Salir");
                Console.Write("\nSeleccione una opción (1-3): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        pilaIda = new PilaPueblos(); // Reiniciamos la pila para un nuevo viaje
                        Console.WriteLine("--- REGISTRO DE RUTA DE IDA ---");
                        Console.Write("¿Cuántos pueblos desea registrar en el trayecto (incluyendo origen y destino)?: ");

                        if (int.TryParse(Console.ReadLine(), out int cantidad) && cantidad > 0)
                        {
                            for (int i = 1; i <= cantidad; i++)
                            {
                                Console.Write($"Ingrese el nombre del pueblo #{i}: ");
                                string nombrePueblo = Console.ReadLine();

                                if (!string.IsNullOrEmpty(nombrePueblo))
                                {
                                    Pueblo p = new Pueblo { Nombre = nombrePueblo };
                                    // Usamos nuestro método Push codificado manualmente
                                    pilaIda.Push(p);
                                }
                                else
                                {
                                    Console.WriteLine("Nombre inválido, intente de nuevo.");
                                    i--; // Repetir iteración
                                }
                            }
                            rutaRegistrada = true;
                            Console.WriteLine("\n¡Ruta de ida registrada exitosamente en la pila!");
                        }
                        else
                        {
                            Console.WriteLine("Cantidad inválida.");
                        }
                        break;

                    case "2":
                        if (!rutaRegistrada || pilaIda.IsEmpty())
                        {
                            Console.WriteLine("Primero debe registrar la ruta de ida (Opción 1).");
                            break;
                        }

                        Console.WriteLine("==========================================");
                        Console.WriteLine("          TRAYECTO DE IDA[cite: 12]              ");
                        Console.WriteLine("==========================================");
                        pilaIda.MostrarRutaIda();

                        Console.WriteLine("\n==========================================");
                        Console.WriteLine("          TRAYECTO DE VUELTA[cite: 12]           ");
                        Console.WriteLine("==========================================");
                        Console.WriteLine("(El conductor regresa por el mismo camino desapilando en orden inverso)\n");

                        // Creamos una copia auxiliar de la pila para no perder los datos al desapilar
                        // o desapilamos directamente mostrando el recorrido de vuelta:
                        // Como la pila guarda el último visitado en la cima, al hacer Pop() 
                        // salimos exactamente en orden inverso (del destino al origen).

                        // Para conservar la pila original para futuras consultas, clonamos o recorrimos:
                        // Una forma elegante con Pilas es desapilar elemento a elemento:
                        int paso = 1;

                        // Creamos una pila temporal para restaurar después si se desea
                        PilaPueblos pilaTemp = new PilaPueblos();

                        while (!pilaIda.IsEmpty())
                        {
                            Pueblo actual = pilaIda.Pop();
                            Console.WriteLine($"  Paso de vuelta {paso}: {actual.Nombre}");
                            pilaTemp.Push(actual);
                            paso++;
                        }

                        // Restauramos la pila original desde la temporal
                        while (!pilaTemp.IsEmpty())
                        {
                            pilaIda.Push(pilaTemp.Pop());
                        }

                        Console.WriteLine("\n¡El conductor ha regresado a casa con éxito!");
                        break;

                    case "3":
                        salir = true;
                        Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
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