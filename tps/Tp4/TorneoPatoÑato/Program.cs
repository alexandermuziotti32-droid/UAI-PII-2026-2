using System;

namespace TorneoPatoNhato
{
    // Clase que representa a cada participante (niño/a)
    public class Participante
    {
        public string Dni { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }

        public override string ToString()
        {
            return $"[DNI: {Dni}] {Apellido}, {Nombre} | Edad: {Edad} años";
        }
    }

    // Nodo para la Lista Enlazada Circular
    public class Nodo
    {
        public Participante Dato { get; set; }
        public Nodo Siguiente { get; set; }

        public Nodo(Participante participante)
        {
            Dato = participante;
            Siguiente = null;
        }
    }

    // Estructura de Lista Enlazada Circular
    public class ListaCircular
    {
        private Nodo cabeza;
        public int Count { get; private set; }

        public ListaCircular()
        {
            cabeza = null;
            Count = 0;
        }

        // a. Registrar un nuevo participante en la lista circular
        public void Registrar(Participante p)
        {
            Nodo nuevo = new Nodo(p);
            if (cabeza == null)
            {
                cabeza = nuevo;
                cabeza.Siguiente = cabeza; // Apunta a sí mismo formando el círculo inicial
            }
            else
            {
                // Buscamos el último nodo (el que apunta a la cabeza)
                Nodo actual = cabeza;
                while (actual.Siguiente != cabeza)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
                nuevo.Siguiente = cabeza; // Cierra el círculo conectando al nuevo con la cabeza
            }
            Count++;
        }

        // b. Eliminar participante (en cualquier posición) buscando por DNI
        public bool Eliminar(string dni)
        {
            if (cabeza == null) return false;

            Nodo actual = cabeza;
            Nodo anterior = null;
            int contador = 0;

            while (contador < Count)
            {
                if (actual.Dato.Dni == dni)
                {
                    // Si es el único elemento en la lista
                    if (Count == 1)
                    {
                        cabeza = null;
                    }
                    else
                    {
                        // Encontramos el último nodo para actualizar su enlace si eliminamos la cabeza
                        Nodo ultimo = cabeza;
                        while (ultimo.Siguiente != cabeza)
                        {
                            ultimo = ultimo.Siguiente;
                        }

                        if (actual == cabeza)
                        {
                            cabeza = cabeza.Siguiente;
                            ultimo.Siguiente = cabeza;
                        }
                        else
                        {
                            anterior.Siguiente = actual.Siguiente;
                        }
                    }
                    Count--;
                    return true;
                }
                anterior = actual;
                actual = actual.Siguiente;
                contador++;
            }
            return false;
        }

        // c. Actualizar los datos de los chicos (modificar algún dato)
        public bool Actualizar(string dni, string nuevoNombre, string nuevoApellido, int nuevaEdad)
        {
            if (cabeza == null) return false;

            Nodo actual = cabeza;
            int contador = 0;
            while (contador < Count)
            {
                if (actual.Dato.Dni == dni)
                {
                    actual.Dato.Nombre = nuevoNombre;
                    actual.Dato.Apellido = nuevoApellido;
                    actual.Dato.Edad = nuevaEdad;
                    return true;
                }
                actual = actual.Siguiente;
                contador++;
            }
            return false;
        }

        // d. Agregar después del seleccionado
        public bool AgregarDespues(string dniReferencia, Participante nuevoParticipante)
        {
            if (cabeza == null) return false;

            Nodo actual = cabeza;
            int contador = 0;
            while (contador < Count)
            {
                if (actual.Dato.Dni == dniReferencia)
                {
                    Nodo nuevo = new Nodo(nuevoParticipante);
                    nuevo.Siguiente = actual.Siguiente;
                    actual.Siguiente = nuevo;
                    Count++;
                    return true;
                }
                actual = actual.Siguiente;
                contador++;
            }
            return false;
        }

        // e. Mostrar Listado de los chicos en forma circular
        public void MostrarListado()
        {
            if (cabeza == null)
            {
                Console.WriteLine("El listado está vacío.");
                return;
            }

            Nodo actual = cabeza;
            int i = 1;
            do
            {
                Console.WriteLine($"{i}. {actual.Dato}");
                actual = actual.Siguiente;
                i++;
            } while (actual != cabeza); // Recorre exactamente una vuelta completa del círculo
        }

        // Método auxiliar para verificar existencia por DNI
        public bool Existe(string dni)
        {
            if (cabeza == null) return false;
            Nodo actual = cabeza;
            int contador = 0;
            while (contador < Count)
            {
                if (actual.Dato.Dni == dni) return true;
                actual = actual.Siguiente;
                contador++;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaCircular torneo = new ListaCircular();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== TORNEO DEL PATO ÑATO (JARDÍN) ===");
                Console.WriteLine("a. Registrar un nuevo participante");
                Console.WriteLine("b. Eliminar participante (en cualquier posición)");
                Console.WriteLine("c. Actualizar datos de un participante");
                Console.WriteLine("d. Agregar después del seleccionado");
                Console.WriteLine("e. Mostrar listado de los chicos");
                Console.WriteLine("f. Salir");
                Console.Write("\nSeleccione una opción (a-f): ");

                string opcion = Console.ReadLine()?.ToLower().Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "a":
                        RegistrarParticipante(torneo);
                        break;
                    case "b":
                        EliminarParticipante(torneo);
                        break;
                    case "c":
                        ActualizarParticipante(torneo);
                        break;
                    case "d":
                        AgregarDespuesDeSeleccionado(torneo);
                        break;
                    case "e":
                        Console.WriteLine("--- LISTADO CIRCULAR DE PARTICIPANTES ---");
                        torneo.MostrarListado();
                        break;
                    case "f":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema del torneo. ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una letra entre a y f.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        // ==========================================
        // a. Registrar participante
        // ==========================================
        static void RegistrarParticipante(ListaCircular torneo)
        {
            Console.WriteLine("--- REGISTRAR NUEVO PARTICIPANTE ---");
            Console.Write("Ingrese DNI: ");
            string dni = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                Console.WriteLine("DNI inválido.");
                return;
            }

            if (torneo.Existe(dni))
            {
                Console.WriteLine("Ya existe un participante registrado con este DNI.");
                return;
            }

            Console.Write("Ingrese Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese Apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Ingrese Edad: ");
            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("Edad inválida.");
                return;
            }

            Participante p = new Participante
            {
                Dni = dni,
                Nombre = nombre,
                Apellido = apellido,
                Edad = edad
            };

            torneo.Registrar(p);
            Console.WriteLine("¡Participante registrado con éxito en la estructura circular!");
        }

        // ==========================================
        // b. Eliminar participante
        // ==========================================
        static void EliminarParticipante(ListaCircular torneo)
        {
            Console.WriteLine("--- ELIMINAR PARTICIPANTE ---");
            if (torneo.Count == 0)
            {
                Console.WriteLine("No hay participantes registrados.");
                return;
            }

            torneo.MostrarListado();
            Console.Write("\nIngrese el DNI del participante que desea eliminar: ");
            string dni = Console.ReadLine()?.Trim();

            if (torneo.Eliminar(dni))
            {
                Console.WriteLine("¡Participante eliminado correctamente de la lista circular!");
            }
            else
            {
                Console.WriteLine("No se encontró ningún participante con ese DNI.");
            }
        }

        // ==========================================
        // c. Actualizar datos
        // ==========================================
        static void ActualizarParticipante(ListaCircular torneo)
        {
            Console.WriteLine("--- ACTUALIZAR DATOS DE PARTICIPANTE ---");
            if (torneo.Count == 0)
            {
                Console.WriteLine("No hay participantes registrados.");
                return;
            }

            torneo.MostrarListado();
            Console.Write("\nIngrese el DNI del participante a modificar: ");
            string dni = Console.ReadLine()?.Trim();

            if (torneo.Existe(dni))
            {
                Console.Write("Nuevo Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Nuevo Apellido: ");
                string apellido = Console.ReadLine();
                Console.Write("Nueva Edad: ");
                if (int.TryParse(Console.ReadLine(), out int edad))
                {
                    torneo.Actualizar(dni, nombre, apellido, edad);
                    Console.WriteLine("¡Datos actualizados con éxito!");
                }
                else
                {
                    Console.WriteLine("Edad inválida.");
                }
            }
            else
            {
                Console.WriteLine("No se encontró un participante con ese DNI.");
            }
        }

        // ==========================================
        // d. Agregar después del seleccionado
        // ==========================================
        static void AgregarDespuesDeSeleccionado(ListaCircular torneo)
        {
            Console.WriteLine("--- AGREGAR DESPUÉS DEL SELECCIONADO ---");
            if (torneo.Count == 0)
            {
                Console.WriteLine("La lista está vacía. Registre un participante primero.");
                return;
            }

            torneo.MostrarListado();
            Console.Write("\nIngrese el DNI del participante de referencia: ");
            string dniRef = Console.ReadLine()?.Trim();

            if (torneo.Existe(dniRef))
            {
                Console.WriteLine("\nIngrese los datos del NUEVO participante:");
                Console.Write("DNI: ");
                string dni = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(dni) || torneo.Existe(dni))
                {
                    Console.WriteLine("DNI inválido o ya existente.");
                    return;
                }

                Console.Write("Nombre: ");
                string nombre = Console.ReadLine();
                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();
                Console.Write("Edad: ");
                if (!int.TryParse(Console.ReadLine(), out int edad))
                {
                    Console.WriteLine("Edad inválida.");
                    return;
                }

                Participante nuevo = new Participante
                {
                    Dni = dni,
                    Nombre = nombre,
                    Apellido = apellido,
                    Edad = edad
                };

                torneo.AgregarDespues(dniRef, nuevo);
                Console.WriteLine("¡Participante agregado con éxito después del seleccionado!");
            }
            else
            {
                Console.WriteLine("No se encontró el participante de referencia.");
            }
        }
    }
}