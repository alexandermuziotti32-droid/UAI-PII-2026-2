using System;
using System.Collections.Generic;

namespace SistemaAlumnos
{
    // Clase que define la estructura del Alumno con los atributos solicitados
    public class Alumno
    {
        public string Dni { get; set; }
        public string Nombres { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }

        // Sobrescribimos ToString para mostrar la información del alumno de forma limpia
        public override string ToString()
        {
            return $"[DNI: {Dni}] {Apellido}, {Nombres} | F. Nac: {FechaNacimiento} | Dir: {Direccion} | Tel: {Telefono}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Creamos la lista genérica para almacenar los alumnos en memoria
            List<Alumno> listaAlumnos = new List<Alumno>();
            bool salir = false;

            // Bucle principal del menú interactivo
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE REGISTRO DE ALUMNOS ===");
                Console.WriteLine("a. Registrar un nuevo alumno");
                Console.WriteLine("b. Eliminar alumno (en cualquier posición)");
                Console.WriteLine("c. Actualizar alumno (modificar algún dato)");
                Console.WriteLine("d. Agregar después del seleccionado");
                Console.WriteLine("e. Agregar antes del seleccionado");
                Console.WriteLine("f. Mostrar Listado de los alumnos actuales");
                Console.WriteLine("g. Salir");
                Console.Write("\nSeleccione una opción (a-g): ");

                string opcion = Console.ReadLine()?.ToLower().Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "a":
                        RegistrarAlumno(listaAlumnos);
                        break;
                    case "b":
                        EliminarAlumno(listaAlumnos);
                        break;
                    case "c":
                        ActualizarAlumno(listaAlumnos);
                        break;
                    case "d":
                        AgregarDespuesDeSeleccionado(listaAlumnos);
                        break;
                    case "e":
                        AgregarAntesDeSeleccionado(listaAlumnos);
                        break;
                    case "f":
                        MostrarListado(listaAlumnos);
                        break;
                    case "g":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, elija una letra entre a y g.");
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
        // a. Registrar un nuevo alumno
        // ==========================================
        static void RegistrarAlumno(List<Alumno> lista)
        {
            Console.WriteLine("--- REGISTRAR NUEVO ALUMNO ---");

            Console.Write("Ingrese DNI del alumno: ");
            string dni = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(dni))
            {
                Console.WriteLine("El DNI no puede estar vacío. Operación cancelada.");
                return;
            }

            // Validamos que el DNI no esté repetido
            if (lista.Exists(a => a.Dni == dni))
            {
                Console.WriteLine("Error: Ya existe un alumno registrado con ese DNI.");
                return;
            }

            Console.Write("Ingrese nombres: ");
            string nombres = Console.ReadLine();

            Console.Write("Ingrese apellido: ");
            string apellido = Console.ReadLine();

            Console.Write("Ingrese fecha de nacimiento (ej: DD/MM/AAAA): ");
            string fechaNac = Console.ReadLine();

            Console.Write("Ingrese dirección: ");
            string direccion = Console.ReadLine();

            Console.Write("Ingrese teléfono: ");
            string telefono = Console.ReadLine();

            Alumno nuevoAlumno = new Alumno
            {
                Dni = dni,
                Nombres = nombres,
                Apellido = apellido,
                FechaNacimiento = fechaNac,
                Direccion = direccion,
                Telefono = telefono
            };

            lista.Add(nuevoAlumno);
            Console.WriteLine("¡Alumno registrado exitosamente!");
        }

        // ==========================================
        // b. Eliminar alumno (en cualquier posición)
        // ==========================================
        static void EliminarAlumno(List<Alumno> lista)
        {
            Console.WriteLine("--- ELIMINAR ALUMNO ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay alumnos registrados en el sistema.");
                return;
            }

            MostrarListado(lista);
            Console.Write("\nIngrese el DNI del alumno que desea eliminar: ");
            string dni = Console.ReadLine()?.Trim();

            Alumno alumnoEncontrado = lista.Find(a => a.Dni == dni);
            if (alumnoEncontrado != null)
            {
                lista.Remove(alumnoEncontrado);
                Console.WriteLine("¡Alumno eliminado correctamente de la lista!");
            }
            else
            {
                Console.WriteLine("No se encontró ningún alumno con ese DNI.");
            }
        }

        // ==========================================
        // c. Actualizar alumno (modificar algún dato)
        // ==========================================
        static void ActualizarAlumno(List<Alumno> lista)
        {
            Console.WriteLine("--- ACTUALIZAR DATOS DE ALUMNO ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay alumnos registrados.");
                return;
            }

            Console.Write("Ingrese el DNI del alumno a modificar: ");
            string dni = Console.ReadLine()?.Trim();

            Alumno a = lista.Find(x => x.Dni == dni);
            if (a != null)
            {
                Console.WriteLine($"\nAlumno encontrado: {a}");
                Console.WriteLine("¿Qué dato desea actualizar?");
                Console.WriteLine("1. Nombres");
                Console.WriteLine("2. Apellido");
                Console.WriteLine("3. Fecha de nacimiento");
                Console.WriteLine("4. Dirección");
                Console.WriteLine("5. Teléfono");
                Console.Write("Elija una opción (1-5): ");
                string subOpt = Console.ReadLine();

                switch (subOpt)
                {
                    case "1":
                        Console.Write("Ingrese nuevo nombre: ");
                        a.Nombres = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Ingrese nuevo apellido: ");
                        a.Apellido = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Ingrese nueva fecha de nacimiento: ");
                        a.FechaNacimiento = Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Ingrese nueva dirección: ");
                        a.Direccion = Console.ReadLine();
                        break;
                    case "5":
                        Console.Write("Ingrese nuevo teléfono: ");
                        a.Telefono = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opción de actualización no válida.");
                        return;
                }
                Console.WriteLine("¡Datos del alumno actualizados con éxito!");
            }
            else
            {
                Console.WriteLine("No se encontró ningún alumno con ese DNI.");
            }
        }

        // ==========================================
        // d. Agregar después del seleccionado
        // ==========================================
        static void AgregarDespuesDeSeleccionado(List<Alumno> lista)
        {
            Console.WriteLine("--- AGREGAR ALUMNO DESPUÉS DE OTRO ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("La lista está vacía. Se registrará el alumno normalmente.");
                RegistrarAlumno(lista);
                return;
            }

            MostrarListado(lista);
            Console.Write("\nIngrese el DNI del alumno de referencia (después de quién desea insertar): ");
            string dniRef = Console.ReadLine()?.Trim();

            int index = lista.FindIndex(a => a.Dni == dniRef);
            if (index != -1)
            {
                Console.WriteLine("\nIngrese los datos del NUEVO alumno:");
                Console.Write("DNI: ");
                string dniNuevo = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(dniNuevo) || lista.Exists(a => a.Dni == dniNuevo))
                {
                    Console.WriteLine("DNI inválido o ya existente. Operación cancelada.");
                    return;
                }

                Console.Write("Nombres: ");
                string nombres = Console.ReadLine();
                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();
                Console.Write("Fecha de nacimiento: ");
                string fechaNac = Console.ReadLine();
                Console.Write("Dirección: ");
                string direccion = Console.ReadLine();
                Console.Write("Teléfono: ");
                string telefono = Console.ReadLine();

                Alumno nuevo = new Alumno
                {
                    Dni = dniNuevo,
                    Nombres = nombres,
                    Apellido = apellido,
                    FechaNacimiento = fechaNac,
                    Direccion = direccion,
                    Telefono = telefono
                };

                // Insertamos exactamente en la posición siguiente (index + 1)
                lista.Insert(index + 1, nuevo);
                Console.WriteLine("¡Nuevo alumno insertado con éxito después del seleccionado!");
            }
            else
            {
                Console.WriteLine("No se encontró el alumno de referencia con ese DNI.");
            }
        }

        // ==========================================
        // e. Agregar antes del seleccionado
        // ==========================================
        static void AgregarAntesDeSeleccionado(List<Alumno> lista)
        {
            Console.WriteLine("--- AGREGAR ALUMNO ANTES DE OTRO ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("La lista está vacía. Se registrará el alumno normalmente.");
                RegistrarAlumno(lista);
                return;
            }

            MostrarListado(lista);
            Console.Write("\nIngrese el DNI del alumno de referencia (antes de quién desea insertar): ");
            string dniRef = Console.ReadLine()?.Trim();

            int index = lista.FindIndex(a => a.Dni == dniRef);
            if (index != -1)
            {
                Console.WriteLine("\nIngrese los datos del NUEVO alumno:");
                Console.Write("DNI: ");
                string dniNuevo = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(dniNuevo) || lista.Exists(a => a.Dni == dniNuevo))
                {
                    Console.WriteLine("DNI inválido o ya existente. Operación cancelada.");
                    return;
                }

                Console.Write("Nombres: ");
                string nombres = Console.ReadLine();
                Console.Write("Apellido: ");
                string apellido = Console.ReadLine();
                Console.Write("Fecha de nacimiento: ");
                string fechaNac = Console.ReadLine();
                Console.Write("Dirección: ");
                string direccion = Console.ReadLine();
                Console.Write("Teléfono: ");
                string telefono = Console.ReadLine();

                Alumno nuevo = new Alumno
                {
                    Dni = dniNuevo,
                    Nombres = nombres,
                    Apellido = apellido,
                    FechaNacimiento = fechaNac,
                    Direccion = direccion,
                    Telefono = telefono
                };

                // Insertamos exactamente en el índice del seleccionado (lo desplaza hacia abajo)
                lista.Insert(index, nuevo);
                Console.WriteLine("¡Nuevo alumno insertado con éxito antes del seleccionado!");
            }
            else
            {
                Console.WriteLine("No se encontró el alumno de referencia con ese DNI.");
            }
        }

        // ==========================================
        // f. Mostrar Listado de los alumnos actuales
        // ==========================================
        static void MostrarListado(List<Alumno> lista)
        {
            Console.WriteLine("--- LISTADO DE ALUMNOS ACTUALES ---");
            if (lista.Count == 0)
            {
                Console.WriteLine("La lista de alumnos está vacía.");
                return;
            }

            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {lista[i]}");
            }
        }
    }
}