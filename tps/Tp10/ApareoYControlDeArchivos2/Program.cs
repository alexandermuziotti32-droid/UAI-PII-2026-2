using System;
using System.Collections.Generic;
using System.IO;

namespace TP_CorteDeControl_ParteB
{
    class Alumno
    {
        public int DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    class Nota
    {
        public int DNI { get; set; }
        public double Valor { get; set; }
        public string Fecha { get; set; }
    }

    class PromedioAlumno
    {
        public int DNI { get; set; }
        public string NombreCompleto { get; set; }
        public double Promedio { get; set; }

        public string ToLineaArchivo() => $"{DNI};{NombreCompleto};{Promedio:F2}";
        public override string ToString() => $"DNI: {DNI} | Alumno: {NombreCompleto,-15} | Promedio: {Promedio:F2}";
    }

    // Punto B.2: Gestor de Promedios
    class GestorPromedios
    {
        private string rutaArchivoPromedios = "promedios.txt";

        // B.2.a: Calcular promedio refactorizado (sin recorrer todo el vector de notas cada vez)
        public List<PromedioAlumno> CalcularPromedios(Alumno[] vectorAlumnos, Nota[] vectorNotas)
        {
            List<PromedioAlumno> listaPromedios = new List<PromedioAlumno>();

            // Refactorización (Punto B.1): el índice 'j' NO se reinicia a 0 en cada vuelta
            int j = 0;

            for (int i = 0; i < vectorAlumnos.Length; i++)
            {
                int dniActual = vectorAlumnos[i].DNI;
                double suma = 0;
                int contador = 0;

                // Avanzar si hay notas de un DNI menor
                while (j < vectorNotas.Length && vectorNotas[j].DNI < dniActual)
                {
                    j++;
                }

                // Sumar solo mientras coincida el DNI (corte de control)
                while (j < vectorNotas.Length && vectorNotas[j].DNI == dniActual)
                {
                    suma += vectorNotas[j].Valor;
                    contador++;
                    j++;
                }

                double promedio = contador > 0 ? suma / contador : 0;

                listaPromedios.Add(new PromedioAlumno
                {
                    DNI = dniActual,
                    NombreCompleto = $"{vectorAlumnos[i].Apellido}, {vectorAlumnos[i].Nombre}",
                    Promedio = promedio
                });
            }

            return listaPromedios;
        }

        // B.2.b: Guardar los promedios en un archivo
        public void GuardarPromedios(List<PromedioAlumno> promedios)
        {
            using (FileStream fs = new FileStream(rutaArchivoPromedios, FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                foreach (PromedioAlumno p in promedios)
                {
                    sw.WriteLine(p.ToLineaArchivo());
                }
            }
        }

        // B.2.c: Obtener lista de promedios desde el archivo
        public List<PromedioAlumno> ObtenerPromediosDesdeArchivo()
        {
            List<PromedioAlumno> lista = new List<PromedioAlumno>();

            if (!File.Exists(rutaArchivoPromedios))
                return lista;

            using (FileStream fs = new FileStream(rutaArchivoPromedios, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
            {
                while (sr.Peek() > -1)
                {
                    string linea = sr.ReadLine();
                    if (string.IsNullOrWhiteSpace(linea)) continue;

                    string[] datos = linea.Split(';');
                    lista.Add(new PromedioAlumno
                    {
                        DNI = int.Parse(datos[0]),
                        NombreCompleto = datos[1],
                        Promedio = double.Parse(datos[2])
                    });
                }
            }

            return lista;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GestorPromedios gestor = new GestorPromedios();

            // Punto B.2.c: Mostrar los promedios desde el archivo apenas inicia el programa
            Console.WriteLine("=== PROMEDIOS CARGADOS AL INICIAR EL PROGRAMA ===");
            List<PromedioAlumno> promediosGuardados = gestor.ObtenerPromediosDesdeArchivo();

            if (promediosGuardados.Count == 0)
            {
                Console.WriteLine("No hay promedios guardados previamente (se creará promedios.txt ahora).");
            }
            else
            {
                foreach (PromedioAlumno item in promediosGuardados)
                {
                    Console.WriteLine(item);
                }
            }

            // Vectores ordenados por DNI (simulando los datos cargados del material de la unidad)
            Alumno[] alumnos = new Alumno[]
            {
                new Alumno { DNI = 28444555, Nombre = "Ana", Apellido = "Perez" },
                new Alumno { DNI = 31222333, Nombre = "Juan", Apellido = "Lopez" },
                new Alumno { DNI = 35111222, Nombre = "Carlos", Apellido = "Gomez" },
                new Alumno { DNI = 41999888, Nombre = "Lucia", Apellido = "Diaz" }
            };

            Nota[] notas = new Nota[]
            {
                new Nota { DNI = 28444555, Valor = 9, Fecha = "2026-10-02" },
                new Nota { DNI = 28444555, Valor = 7, Fecha = "2026-10-20" },
                new Nota { DNI = 31222333, Valor = 10, Fecha = "2026-10-05" },
                new Nota { DNI = 31222333, Valor = 8, Fecha = "2026-10-18" },
                new Nota { DNI = 35111222, Valor = 8, Fecha = "2026-10-01" },
                new Nota { DNI = 35111222, Valor = 6, Fecha = "2026-10-15" }
            };

            Console.WriteLine("\nProcesando vectores y guardando archivo...");
            List<PromedioAlumno> nuevosPromedios = gestor.CalcularPromedios(alumnos, notas);
            gestor.GuardarPromedios(nuevosPromedios);

            Console.WriteLine("\n=== NUEVOS PROMEDIOS CALCULADOS Y GUARDADOS ===");
            foreach (PromedioAlumno p in nuevosPromedios)
            {
                Console.WriteLine(p);
            }

            Console.WriteLine("\nEjecuta el programa nuevamente para ver cómo lee promedios.txt al iniciar.");
            Console.ReadKey();
        }
    }
}