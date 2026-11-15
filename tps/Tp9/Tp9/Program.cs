using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AbmConsola
{
    class Alumno
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string ToLinea() => $"{DNI};{Nombre};{Apellido}";
    }

    class Nota
    {
        public int DNI { get; set; }
        public decimal Calificacion { get; set; }
        public DateTime Fecha { get; set; }
        public string ToLinea() => $"{DNI};{Calificacion};{Fecha:yyyy-MM-dd}";
    }

    class Program
    {
        static string rutaAlumnos = "alumnos.txt";
        static string rutaNotas = "notas.txt";
        static List<Alumno> alumnos = new List<Alumno>();
        static List<Nota> notas = new List<Nota>();

        static void Main()
        {
            CargarDatos();
            int opcion;

            do
            {
                Console.Clear();
                Console.WriteLine("=== ABM ALUMNOS Y NOTAS ===");
                Console.WriteLine("1. Alta Alumno");
                Console.WriteLine("2. Modificar Alumno");
                Console.WriteLine("3. Baja Alumno");
                Console.WriteLine("4. Listar Alumnos");
                Console.WriteLine("5. Alta Nota");
                Console.WriteLine("6. Listar Notas");
                Console.WriteLine("0. Salir");
                Console.Write("\nOpción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1: AltaAlumno(); break;
                    case 2: ModificarAlumno(); break;
                    case 3: BajaAlumno(); break;
                    case 4: ListarAlumnos(); break;
                    case 5: AltaNota(); break;
                    case 6: ListarNotas(); break;
                }
            } while (opcion != 0);
        }

        static void CargarDatos()
        {
            if (File.Exists(rutaAlumnos))
            {
                foreach (var l in File.ReadAllLines(rutaAlumnos))
                {
                    var p = l.Split(';');
                    alumnos.Add(new Alumno { DNI = int.Parse(p[0]), Nombre = p[1], Apellido = p[2] });
                }
            }

            if (File.Exists(rutaNotas))
            {
                foreach (var l in File.ReadAllLines(rutaNotas))
                {
                    var p = l.Split(';');
                    notas.Add(new Nota { DNI = int.Parse(p[0]), Calificacion = decimal.Parse(p[1]), Fecha = DateTime.Parse(p[2]) });
                }
            }
        }

        static void GuardarTodo()
        {
            File.WriteAllLines(rutaAlumnos, alumnos.Select(a => a.ToLinea()));
            File.WriteAllLines(rutaNotas, notas.Select(n => n.ToLinea()));
        }

        static void AltaAlumno()
        {
            Console.Write("DNI: ");
            int dni = int.Parse(Console.ReadLine());
            if (alumnos.Any(a => a.DNI == dni))
            {
                Console.WriteLine("Ya existe ese DNI.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine();
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine();

            alumnos.Add(new Alumno { DNI = dni, Nombre = nombre, Apellido = apellido });
            GuardarTodo();
            Console.WriteLine("Alumno guardado.");
            Console.ReadKey();
        }

        static void ModificarAlumno()
        {
            Console.Write("DNI del alumno a modificar: ");
            int dni = int.Parse(Console.ReadLine());
            var alumno = alumnos.FirstOrDefault(a => a.DNI == dni);

            if (alumno == null)
            {
                Console.WriteLine("No encontrado.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nuevo Nombre: ");
            alumno.Nombre = Console.ReadLine();
            Console.Write("Nuevo Apellido: ");
            alumno.Apellido = Console.ReadLine();

            GuardarTodo();
            Console.WriteLine("Alumno modificado.");
            Console.ReadKey();
        }

        static void BajaAlumno()
        {
            Console.Write("DNI del alumno a borrar: ");
            int dni = int.Parse(Console.ReadLine());
            var alumno = alumnos.FirstOrDefault(a => a.DNI == dni);

            if (alumno == null || notas.Any(n => n.DNI == dni))
            {
                Console.WriteLine("No existe o tiene notas asociadas.");
                Console.ReadKey();
                return;
            }

            alumnos.Remove(alumno);
            GuardarTodo();
            Console.WriteLine("Alumno eliminado.");
            Console.ReadKey();
        }

        static void ListarAlumnos()
        {
            foreach (var a in alumnos)
                Console.WriteLine($"{a.DNI} - {a.Apellido}, {a.Nombre}");
            Console.ReadKey();
        }

        static void AltaNota()
        {
            Console.Write("DNI del alumno: ");
            int dni = int.Parse(Console.ReadLine());
            if (!alumnos.Any(a => a.DNI == dni))
            {
                Console.WriteLine("El alumno no existe.");
                Console.ReadKey();
                return;
            }

            Console.Write("Nota: ");
            decimal calif = decimal.Parse(Console.ReadLine());
            Console.Write("Fecha (dd/mm/aaaa): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            notas.Add(new Nota { DNI = dni, Calificacion = calif, Fecha = fecha });
            GuardarTodo();
            Console.WriteLine("Nota guardada.");
            Console.ReadKey();
        }

        static void ListarNotas()
        {
            foreach (var n in notas)
                Console.WriteLine($"DNI: {n.DNI} | Nota: {n.Calificacion} | Fecha: {n.Fecha:dd/MM/yyyy}");
            Console.ReadKey();
        }
    }
}