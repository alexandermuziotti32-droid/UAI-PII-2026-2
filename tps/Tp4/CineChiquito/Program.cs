using System;

namespace CineListaDoblementeEnlazada
{
    // Clase que define los datos de la Persona / Espectador
    public class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public decimal PrecioEntrada { get; set; }

        public override string ToString()
        {
            return $"[ID: {Id}] Nombre: {Nombre} | Edad: {Edad} años | Entrada: ${PrecioEntrada:F2}";
        }
    }

    // Nodo para la Lista Doblemente Enlazada
    public class NodoDoble
    {
        public Persona Dato { get; set; }
        public NodoDoble Siguiente { get; set; }
        public NodoDoble Anterior { get; set; }

        public NodoDoble(Persona persona)
        {
            Dato = persona;
            Siguiente = null;
            Anterior = null;
        }
    }

    // Estructura de Lista Doblemente Enlazada para el cine
    public class ListaDobleCine
    {
        private NodoDoble cabeza;
        private NodoDoble cola;
        public int Count { get; private set; }

        public ListaDobleCine()
        {
            cabeza = null;
            cola = null;
            Count = 0;
        }

        // a. Registrar una nueva persona en la lista doblemente enlazada
        public void Registrar(Persona p)
        {
            NodoDoble nuevo = new NodoDoble(p);
            if (cabeza == null)
            {
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }
            Count++;
        }

        // b. Eliminar persona de la lista (en cualquier posición) buscando por ID
        public bool Eliminar(int id)
        {
            if (cabeza == null) return false;

            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Id == id)
                {
                    // Si es el único elemento
                    if (cabeza == cola)
                    {
                        cabeza = null;
                        cola = null;
                    }
                    // Si es la cabeza
                    else if (actual == cabeza)
                    {
                        cabeza = cabeza.Siguiente;
                        cabeza.Anterior = null;
                    }
                    // Si es la cola
                    else if (actual == cola)
                    {
                        cola = cola.Anterior;
                        cola.Siguiente = null;
                    }
                    // Si está en el medio
                    else
                    {
                        actual.Anterior.Siguiente = actual.Siguiente;
                        actual.Siguiente.Anterior = actual.Anterior;
                    }

                    Count--;
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // c. Actualizar los datos de la persona
        public bool Actualizar(int id, string nuevoNombre, int nuevaEdad, decimal nuevoPrecio)
        {
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Id == id)
                {
                    actual.Dato.Nombre = nuevoNombre;
                    actual.Dato.Edad = nuevaEdad;
                    actual.Dato.PrecioEntrada = nuevoPrecio;
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // d. Mostrar Listado de las personas
        public void MostrarListado()
        {
            if (cabeza == null)
            {
                Console.WriteLine("El listado de personas está vacío.");
                return;
            }

            NodoDoble actual = cabeza;
            int i = 1;
            while (actual != null)
            {
                Console.WriteLine($"{i}. {actual.Dato}");
                actual = actual.Siguiente;
                i++;
            }
        }

        // Método auxiliar para verificar existencia por ID
        public bool Existe(int id)
        {
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Id == id) return true;
                actual = actual.Siguiente;
            }
            return false;
        }

        // Método para calcular la recaudación total recorriendo la lista
        public decimal ObtenerRecaudacionTotal()
        {
            decimal total = 0;
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                total += actual.Dato.PrecioEntrada;
                actual = actual.Siguiente;
            }
            return total;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaDobleCine cineLista = new ListaDobleCine();
            Random random = new Random();

            // REQUISITO: El número de personas de la lista se elige al azar entre 0 y 50.
            int cantidadInicial = random.Next(0, 51);
            Console.WriteLine($"--- Generando listado inicial automático de {cantidadInicial} personas ---");

            for (int i = 1; i <= cantidadInicial; i++)
            {
                int edad = random.Next(5, 61); // Edades entre 5 y 60 años
                decimal precio = CalcularPrecioEntrada(edad);

                cineLista.Registrar(new Persona
                {
                    Id = i,
                    Nombre = $"Espectador_{i}",
                    Edad = edad,
                    PrecioEntrada = precio
                });
            }

            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== CONTROL DE CINE (LISTA DOBLEMENTE ENLAZADA) ===");
                Console.WriteLine("a. Llevar un registro de las nuevas personas que ingresan a la lista[cite: 9]");
                Console.WriteLine("b. Poder eliminar a la persona que se va de la lista (en cualquier posición)[cite: 9]");
                Console.WriteLine("c. Actualizar los datos de la persona (modificar algún dato)[cite: 9]");
                Console.WriteLine("d. Mostrar Listado de las personas[cite: 9]");
                Console.WriteLine("e. Salir y ver recaudación total[cite: 9]");
                Console.Write("\nSeleccione una opción (a-e): ");

                string opcion = Console.ReadLine()?.ToLower().Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "a":
                        RegistrarPersona(cineLista, random);
                        break;
                    case "b":
                        EliminarPersona(cineLista);
                        break;
                    case "c":
                        ActualizarPersona(cineLista);
                        break;
                    case "d":
                        Console.WriteLine("--- LISTADO DE PERSONAS EN ESPERA ---");
                        cineLista.MostrarListado();
                        break;
                    case "e":
                        salir = true;
                        MostrarRecaudacionFinal(cineLista);
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una letra entre a y e.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // Método auxiliar para calcular el precio de la entrada según la edad[cite: 9]
        static decimal CalcularPrecioEntrada(int edad)
        {
            if (edad <= 12) return 4000.00m;
            if (edad >= 60) return 4500.00m;
            return 7000.00m;
        }

        // ==========================================
        // a. Registrar nueva persona
        // ==========================================
        static void RegistrarPersona(ListaDobleCine cineLista, Random random)
        {
            Console.WriteLine("--- REGISTRAR NUEVA PERSONA ---");
            Console.Write("Ingrese ID único (número entero): ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            if (cineLista.Existe(id))
            {
                Console.WriteLine("Ya existe una persona registrada con ese ID.");
                return;
            }

            Console.Write("Ingrese el nombre de la persona: ");
            string nombre = Console.ReadLine();

            // Edad generada aleatoriamente entre 5 y 60 años[cite: 9]
            int edad = random.Next(5, 61);
            decimal precio = CalcularPrecioEntrada(edad);

            Persona nueva = new Persona
            {
                Id = id,
                Nombre = string.IsNullOrEmpty(nombre) ? "Sin Nombre" : nombre,
                Edad = edad,
                PrecioEntrada = precio
            };

            cineLista.Registrar(nueva);
            Console.WriteLine($"¡Persona registrada con éxito! (Edad asignada al azar: {edad} años | Entrada: ${precio:F2})");
        }

        // ==========================================
        // b. Eliminar persona en cualquier posición
        // ==========================================
        static void EliminarPersona(ListaDobleCine cineLista)
        {
            Console.WriteLine("--- ELIMINAR PERSONA ---");
            if (cineLista.Count == 0)
            {
                Console.WriteLine("No hay personas en la lista.");
                return;
            }

            cineLista.MostrarListado();
            Console.Write("\nIngrese el ID de la persona que desea eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (cineLista.Eliminar(id))
                {
                    Console.WriteLine("¡Persona eliminada correctamente de la lista doblemente enlazada!");
                }
                else
                {
                    Console.WriteLine("No se encontró ninguna persona con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
            }
        }

        // ==========================================
        // c. Actualizar datos de la persona
        // ==========================================
        static void ActualizarPersona(ListaDobleCine cineLista)
        {
            Console.WriteLine("--- ACTUALIZAR DATOS DE PERSONA ---");
            if (cineLista.Count == 0)
            {
                Console.WriteLine("No hay personas registradas.");
                return;
            }

            cineLista.MostrarListado();
            Console.Write("\nIngrese el ID de la persona a modificar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                if (cineLista.Existe(id))
                {
                    Console.Write("Ingrese nuevo nombre: ");
                    string nombre = Console.ReadLine();
                    Console.Write("Ingrese nueva edad (mínimo 5 años)[cite: 9]: ");
                    if (int.TryParse(Console.ReadLine(), out int edad) && edad >= 5)
                    {
                        decimal precio = CalcularPrecioEntrada(edad);
                        cineLista.Actualizar(id, nombre, edad, precio);
                        Console.WriteLine($"¡Datos actualizados con éxito! Nuevo precio de entrada: ${precio:F2}");
                    }
                    else
                    {
                        Console.WriteLine("Edad inválida.");
                    }
                }
                else
                {
                    Console.WriteLine("No se encontró una persona con ese ID.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        // ==========================================
        // Salir y mostrar recaudación total[cite: 9]
        // ==========================================
        static void MostrarRecaudacionFinal(ListaDobleCine cineLista)
        {
            decimal totalRecaudado = cineLista.ObtenerRecaudacionTotal();

            Console.WriteLine("==================================================");
            Console.WriteLine($"  CANTIDAD TOTAL DE PERSONAS: {cineLista.Count}");
            Console.WriteLine($"  CANTIDAD TOTAL RECAUDADA: ${totalRecaudado:F2}[cite: 9]");
            Console.WriteLine("==================================================");
            Console.WriteLine("Saliendo del sistema del cine. ¡Hasta luego!");
        }
    }
}