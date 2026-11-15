using System;
using System.Collections.Generic;

namespace HospitalSystem
{
    // Clase que define la estructura del Paciente con los atributos solicitados
    public class Paciente
    {
        public int Codigo { get; set; }
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // Sobrescribimos ToString para mostrar la información del paciente de forma limpia
        public override string ToString()
        {
            return $"[Código: {Codigo}] {Apellido}, {Nombres} | Dirección: {Direccion} | Tel: {Telefono}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Creamos la lista genérica para almacenar los pacientes en memoria
            List<Paciente> hospitalPacientes = new List<Paciente>();
            bool salir = false;

            // Bucle principal del menú interactivo
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE REGISTRO DE PACIENTES - HOSPITAL ===");
                Console.WriteLine("a. Registrar un nuevo paciente");
                Console.WriteLine("b. Eliminar paciente");
                Console.WriteLine("c. Actualizar Pacientes (modificar algún dato)");
                Console.WriteLine("d. Agregar después del seleccionado");
                Console.WriteLine("e. Mostrar Listado");
                Console.WriteLine("f. Salir");
                Console.Write("\nSeleccione una opción (a-f): ");

                string opcion = Console.ReadLine()?.ToLower().Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "a":
                        RegistrarPaciente(hospitalPacientes);
                        break;
                    case "b":
                        EliminarPaciente(hospitalPacientes);
                        break;
                    case "c":
                        ActualizarPaciente(hospitalPacientes);
                        break;
                    case "d":
                        AgregarDespuesDeSeleccionado(hospitalPacientes);
                        break;
                    case "e":
                        MostrarListado(hospitalPacientes);
                        break;
                    case "f":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
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
        // a. Registrar un nuevo paciente
        // ==========================================
        static void RegistrarPaciente(List<Paciente> lista)
        {
            Console.WriteLine("--- REGISTRAR NUEVO PACIENTE ---");

            Console.Write("Ingrese código del paciente (número entero): ");
            if (!int.TryParse(Console.ReadLine(), out int codigo))
            {
                Console.WriteLine("Código inválido. Operación cancelada.");
                return;
            }

            // Validamos que el código no esté repetido
            if (lista.Exists(p => p.Codigo == codigo))
            {
                Console.WriteLine("Error: Ya existe un paciente registrado con ese código.");
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

            // Creamos la instancia y la agregamos al final de la lista
            Paciente nuevoPaciente = new Paciente
            {
                Codigo = codigo,
                Nombres = nombres,
                Apellido = apellido,
                Direccion = direccion,
                Telefono = telefono
            };

            lista.Add(nuevoPaciente);
            Console.WriteLine("¡Paciente registrado exitosamente!");
        }

        // ==========================================
        // b. Eliminar paciente (en cualquier posición)
        // ==========================================
        static void EliminarPaciente(List<Paciente> lista)
        {
            Console.WriteLine("--- ELIMINAR PACIENTE ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados en el sistema.");
                return;
            }

            MostrarListado(lista);
            Console.Write("\nIngrese el código del paciente que desea eliminar: ");

            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                Paciente pacienteEncontrado = lista.Find(p => p.Codigo == codigo);
                if (pacienteEncontrado != null)
                {
                    lista.Remove(pacienteEncontrado);
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
        // c. Actualizar Pacientes (modificar datos)
        // ==========================================
        static void ActualizarPaciente(List<Paciente> lista)
        {
            Console.WriteLine("--- ACTUALIZAR DATOS DE PACIENTE ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay pacientes registrados.");
                return;
            }

            Console.Write("Ingrese el código del paciente a modificar: ");
            if (int.TryParse(Console.ReadLine(), out int codigo))
            {
                Paciente p = lista.Find(x => x.Codigo == codigo);
                if (p != null)
                {
                    Console.WriteLine($"\nPaciente encontrado: {p}");
                    Console.WriteLine("¿Qué dato desea actualizar?");
                    Console.WriteLine("1. Nombres");
                    Console.WriteLine("2. Apellido");
                    Console.WriteLine("3. Dirección");
                    Console.WriteLine("4. Teléfono");
                    Console.Write("Elija una opción (1-4): ");
                    string subOpt = Console.ReadLine();

                    switch (subOpt)
                    {
                        case "1":
                            Console.Write("Ingrese nuevo nombre: ");
                            p.Nombres = Console.ReadLine();
                            break;
                        case "2":
                            Console.Write("Ingrese nuevo apellido: ");
                            p.Apellido = Console.ReadLine();
                            break;
                        case "3":
                            Console.Write("Ingrese nueva dirección: ");
                            p.Direccion = Console.ReadLine();
                            break;
                        case "4":
                            Console.Write("Ingrese nuevo teléfono: ");
                            p.Telefono = Console.ReadLine();
                            break;
                        default:
                            Console.WriteLine("Opción de actualización no válida.");
                            return;
                    }
                    Console.WriteLine("¡Datos del paciente actualizados con éxito!");
                }
                else
                {
                    Console.WriteLine("No se encontró ningún paciente con ese código.");
                }
            }
            else
            {
                Console.WriteLine("Código inválido.");
            }
        }

        // ==========================================
        // d. Agregar después del seleccionado
        // ==========================================
        static void AgregarDespuesDeSeleccionado(List<Paciente> lista)
        {
            Console.WriteLine("--- AGREGAR PACIENTE DESPUÉS DE OTRO ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("La lista está vacía. Se registrará el paciente normalmente.");
                RegistrarPaciente(lista);
                return;
            }

            MostrarListado(lista);
            Console.Write("\nIngrese el código del paciente de referencia (después de quién desea insertar): ");

            if (int.TryParse(Console.ReadLine(), out int codigoRef))
            {
                // Buscamos el índice del elemento seleccionado
                int index = lista.FindIndex(p => p.Codigo == codigoRef);
                if (index != -1)
                {
                    Console.WriteLine("\nIngrese los datos del NUEVO paciente:");
                    Console.Write("Código: ");
                    if (!int.TryParse(Console.ReadLine(), out int codigoNuevo))
                    {
                        Console.WriteLine("Código inválido.");
                        return;
                    }

                    if (lista.Exists(p => p.Codigo == codigoNuevo))
                    {
                        Console.WriteLine("Ya existe un paciente con ese código.");
                        return;
                    }

                    Console.Write("Nombres: ");
                    string nombres = Console.ReadLine();
                    Console.Write("Apellido: ");
                    string apellido = Console.ReadLine();
                    Console.Write("Dirección: ");
                    string direccion = Console.ReadLine();
                    Console.Write("Teléfono: ");
                    string telefono = Console.ReadLine();

                    Paciente nuevo = new Paciente
                    {
                        Codigo = codigoNuevo,
                        Nombres = nombres,
                        Apellido = apellido,
                        Direccion = direccion,
                        Telefono = telefono
                    };

                    // Insertamos exactamente en la posición siguiente (index + 1) usando el método Insert de las listas
                    lista.Insert(index + 1, nuevo);
                    Console.WriteLine("¡Nuevo paciente insertado con éxito en la posición posterior!");
                }
                else
                {
                    Console.WriteLine("No se encontró el paciente de referencia con ese código.");
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida.");
            }
        }

        // ==========================================
        // e. Mostrar Listado
        // ==========================================
        static void MostrarListado(List<Paciente> lista)
        {
            Console.WriteLine("--- LISTADO DE PACIENTES ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("La lista de pacientes está vacía.");
                return;
            }

            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {lista[i]}");
            }
        }
    }
}
