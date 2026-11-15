using System;
using System.Collections.Generic;
using System.IO;

namespace TP_CorteDeControl_ParteA
{
    class Program
    {
        static string rutaAlumnos = "alumnos.txt";
        static string rutaNotas = "notas.txt";

        static void Main(string[] args)
        {
            InicializarArchivos();

            int opcion;
            do
            {
                Console.Clear();
                Console.WriteLine("=== PARTE A: ORDENAMIENTO Y APAREO (TP 9) ===");
                Console.WriteLine("1. Ver contenido de alumnos.txt y notas.txt");
                Console.WriteLine("2. Ordenar ambos archivos por DNI");
                Console.WriteLine("3. Obtener promedio de notas por alumno (Apareo)");
                Console.WriteLine("0. Salir");
                Console.Write("\nSeleccione una opción: ");
                int.TryParse(Console.ReadLine(), out opcion);

                switch (opcion)
                {
                    case 1:
                        MostrarArchivos();
                        break;
                    case 2:
                        OrdenarArchivoPorDni(rutaAlumnos);
                        OrdenarArchivoPorDni(rutaNotas);
                        Console.WriteLine("\nArchivos ordenados por DNI correctamente.");
                        Console.ReadKey();
                        break;
                    case 3:
                        // Se ordenan primero para garantizar que el apareo funcione bien
                        OrdenarArchivoPorDni(rutaAlumnos);
                        OrdenarArchivoPorDni(rutaNotas);
                        ObtenerPromediosPorApareo();
                        Console.ReadKey();
                        break;
                }
            } while (opcion != 0);
        }

        // Genera los archivos con el formato del TP 9 si no existen en la PC
        static void InicializarArchivos()
        {
            if (!File.Exists(rutaAlumnos))
            {
                using (FileStream fs = new FileStream(rutaAlumnos, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("35111222;Carlos;Gomez");
                    sw.WriteLine("28444555;Ana;Perez");
                    sw.WriteLine("41999888;Lucia;Diaz");
                    sw.WriteLine("31222333;Juan;Lopez");
                }
            }

            if (!File.Exists(rutaNotas))
            {
                using (FileStream fs = new FileStream(rutaNotas, FileMode.Create))
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine("35111222;8;2026-10-01");
                    sw.WriteLine("28444555;9;2026-10-02");
                    sw.WriteLine("35111222;6;2026-10-15");
                    sw.WriteLine("31222333;10;2026-10-05");
                    sw.WriteLine("28444555;7;2026-10-20");
                    sw.WriteLine("31222333;8;2026-10-18");
                }
            }
        }

        static void MostrarArchivos()
        {
            Console.WriteLine("\n--- ALUMNOS.TXT (DNI;Nombre;Apellido) ---");
            using (StreamReader sr = new StreamReader(rutaAlumnos))
            {
                while (sr.Peek() > -1)
                    Console.WriteLine(sr.ReadLine());
            }

            Console.WriteLine("\n--- NOTAS.TXT (DNI;Nota;Fecha) ---");
            using (StreamReader sr = new StreamReader(rutaNotas))
            {
                while (sr.Peek() > -1)
                    Console.WriteLine(sr.ReadLine());
            }
            Console.ReadKey();
        }

        // Punto A.1: Ordena cualquier archivo separado por ';' usando el campo 0 (DNI)
        static void OrdenarArchivoPorDni(string ruta)
        {
            List<string> registros = new List<string>();

            using (FileStream fs = new FileStream(ruta, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (sr.Peek() > -1)
                {
                    string linea = sr.ReadLine();
                    if (!string.IsNullOrWhiteSpace(linea))
                        registros.Add(linea);
                }
            }

            // Método de burbuja comparando el DNI (posición 0 del Split)
            for (int i = 0; i < registros.Count - 1; i++)
            {
                for (int j = 0; j < registros.Count - 1 - i; j++)
                {
                    int dniActual = int.Parse(registros[j].Split(';')[0]);
                    int dniSiguiente = int.Parse(registros[j + 1].Split(';')[0]);

                    if (dniActual > dniSiguiente)
                    {
                        string aux = registros[j];
                        registros[j] = registros[j + 1];
                        registros[j + 1] = aux;
                    }
                }
            }

            using (FileStream fs = new FileStream(ruta, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (string reg in registros)
                    sw.WriteLine(reg);
            }
        }

        // Punto A.2: Apareo secuencial de ambos archivos ordenados
        static void ObtenerPromediosPorApareo()
        {
            Console.WriteLine("\n=== PROMEDIOS POR ALUMNO (APAREO) ===");

            using (FileStream fsAlu = new FileStream(rutaAlumnos, FileMode.Open))
            using (StreamReader srAlu = new StreamReader(fsAlu))
            using (FileStream fsNot = new FileStream(rutaNotas, FileMode.Open))
            using (StreamReader srNot = new StreamReader(fsNot))
            {
                // Leemos el primer registro de notas
                string lineaNota = srNot.Peek() > -1 ? srNot.ReadLine() : null;

                while (srAlu.Peek() > -1)
                {
                    string[] datosAlu = srAlu.ReadLine().Split(';');
                    int dniAlu = int.Parse(datosAlu[0]);
                    string nombreCompleto = $"{datosAlu[2]}, {datosAlu[1]}";

                    // Si hubiera notas de un DNI menor que no está en alumnos, avanzamos
                    while (lineaNota != null && int.Parse(lineaNota.Split(';')[0]) < dniAlu)
                    {
                        lineaNota = srNot.Peek() > -1 ? srNot.ReadLine() : null;
                    }

                    // Corte de control mientras la nota pertenezca al alumno actual
                    double suma = 0;
                    int cantidad = 0;

                    while (lineaNota != null && int.Parse(lineaNota.Split(';')[0]) == dniAlu)
                    {
                        string[] datosNota = lineaNota.Split(';');
                        suma += double.Parse(datosNota[1]);
                        cantidad++;

                        lineaNota = srNot.Peek() > -1 ? srNot.ReadLine() : null;
                    }

                    if (cantidad > 0)
                        Console.WriteLine($"DNI: {dniAlu} | {nombreCompleto,-15} | Notas: {cantidad} | Promedio: {suma / cantidad:F2}");
                    else
                        Console.WriteLine($"DNI: {dniAlu} | {nombreCompleto,-15} | Sin notas registradas");
                }
            }
        }
    }
}