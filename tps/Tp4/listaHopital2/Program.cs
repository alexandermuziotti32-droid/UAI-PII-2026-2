using System;

namespace HospitalListaDoblementeEnlazada
{
    // Clase que define los datos del Paciente
    public class Paciente
    {
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        public override string ToString()
        {
            return $"[Código: {Codigo}] {Apellido}, {Nombres} | Dirección: {Direccion} | Tel: {Telefono}";
        }
    }

    // Nodo para la Lista Doblemente Enlazada
    public class NodoDoble
    {
        public Paciente Dato { get; set; }
        public NodoDoble Siguiente { get; set; }
        public NodoDoble Anterior { get; set; }

        public NodoDoble(Paciente paciente)
        {
            Dato = paciente;
            Siguiente = null;
            Anterior = null;
        }
    }

    // Estructura de Lista Doblemente Enlazada
    public class ListaDoblementeEnlazada
    {
        private NodoDoble cabeza;
        private NodoDoble cola;
        public int Count { get; private set; }

        public ListaDoblementeEnlazada()
        {
            cabeza = null;
            cola = null;
            Count = 0;
        }

        // a. Registrar un nuevo paciente
        public void Registrar(Paciente p)
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

        // b. Eliminar paciente (en cualquier posición) buscando por código
        public bool Eliminar(int codigo)
        {
            if (cabeza == null) return false;

            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    // Si es el único elemento
                    if (cabeza == cola)
                    {
                        cabeza = null;
                        cola = null;
                    }
                    // Si es el primer elemento (cabeza)
                    else if (actual == cabeza)
                    {
                        cabeza = cabeza.Siguiente;
                        cabeza.Anterior = null;
                    }
                    // Si es el último elemento (cola)
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

        // c. Actualizar los datos del paciente
        public bool Actualizar(int codigo, string nuevoNombre, string nuevoApellido, string nuevaDireccion, string nuevoTelefono)
        {
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    actual.Dato.Nombres = nuevoNombre;
                    actual.Dato.Apellido = nuevoApellido;
                    actual.Dato.Direccion = nuevaDireccion;
                    actual.Dato.Telefono = nuevoTelefono;
                    return true;
                }
                actual = actual.Siguiente;
            }
            return false;
        }

        // d. Mostrar Listado (recorrido normal desde la cabeza hacia la cola)
        public void MostrarListado()
        {
            if (cabeza == null)
            {
                Console.WriteLine("El listado de pacientes está vacío.");
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

        // Método auxiliar para verificar existencia por código
        public bool Existe(int codigo)
        {
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo) return true;
                actual = actual.Siguiente;
            }
            return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaDoblementeEnlazada listaPacientes = new ListaDoblementeEnlazada();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE HOSPITAL (LISTA DOBLEMENTE ENLAZADA) ===");
                Console.WriteLine("a. Registrar un nuevo paciente");
                Console.WriteLine("b. Eliminar paciente (en cualquier posición)");
                Console.WriteLine("c. Actualizar Pacientes (modificar algún dato)");
                Console.WriteLine("d. Mostrar Listado");
                Console.WriteLine("e. Salir");
                Console.Write("\nSeleccione una opción (a-e): ");

                string opcion = Console.ReadLine()?.ToLower().Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "a":
                        RegistrarPaciente(listaPacientes);
                        break;
                    case "b":
                        EliminarPaciente(listaPacientes);
                        break;
                    case "c":
                        ActualizarPaciente(listaPacientes);
                        break;
                    case "d":
                        Console.WriteLine("--- LISTADO DE PACIENTES ---");
                        listaPacientes.MostrarListado();
                        break;
                    case "e":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
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

        // ==========================================
        // a. Registrar un nuevo paciente
        // ==========================================
        static void RegistrarPaciente(ListaDoblementeEnlazada lista)
        {
            Console.WriteLine("--- REGISTRAR NUEVO PACIENTE ---");
            Console.Write("Ingrese código del paciente (número entero): ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido.");
                return;
            }

            if (lista.Existe(codigo))
            {
                Console.WriteLine("Ya existe un paciente registrado con ese código.");
                return;
            }

            Console.Write("Ingrese nombres: ");
            string nombres = Console.ReadLine();
            Console.Write("Ingrese apellido: ");
            string apellido = Console.ReadLine();
            Console.Write("Ingrese dirección: ");
            string direccion = Console.ReadLine();
            Console.Write("Ingrese teléfono: ");
            string telefono = Console.ReadLine();

            Paciente p = new Paciente
            {
                Codigo = codigo,
                Nombres = nombres,
                Apellido = apellido,
                Direccion = direccion,
                Telefono = telefono
            };

            lista.Registrar(p);
            Console.WriteLine("¡Paciente registrado con éxito en la lista doblemente enlazada!");
        }

        // ==========================================
        // b. Eliminar paciente en cualquier posición
        // ==========================================
        static void EliminarPaciente(ListaDoblementeEnlazada lista)
        {
            Console.WriteLine("--- ELIMINAR PACIENTE ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            lista.MostrarListado();
            Console.Write("\nIngrese el código del paciente que desea eliminar: ");
            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                if (lista.Eliminar(codigo))
                {
                    Console.WriteLine("¡Paciente eliminado correctamente de la lista!");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún paciente con ese código.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
            }
        }

        // ==========================================
        // c. Actualizar pacientes
        // ==========================================
        static void ActualizarPaciente(ListaDoblementeEnlazada lista)
        {
            Console.WriteLine("--- ACTUALIZAR DATOS DE PACIENTE ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            lista.MostrarListado();
            Console.Write("\nIngrese el código del paciente a modificar: ");
            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                if (lista.Existe(codigo))
                {
                    Console.Write("Nuevo nombre: ");
                    string nombres = Console.ReadLine();
                    Console.Write("Nuevo apellido: ");
                    string apellido = Console.ReadLine();
                    Console.Write("Nueva dirección: ");
                    string direccion = Console.ReadLine();
                    Console.Write("Nuevo teléfono: ");
                    string telefono = Console.ReadLine();

                    lista.Actualizar(codigo, nombres, apellido, direccion, telefono);
                    Console.WriteLine("¡Datos actualizados con éxito!");
                }
                else
                {
                    Console.WriteLine("No se encontró un paciente con ese código.");
                }
            }
            else
            {
                Console.WriteLine("Código inválido.");
            }
        }
    }
}