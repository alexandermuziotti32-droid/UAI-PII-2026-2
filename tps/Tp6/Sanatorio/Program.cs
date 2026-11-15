using System;

namespace NatatorioColas
{
    // Clase que representa a la persona que va a la revisión médica
    public class Persona
    {
        public string Nombre { get; set; }
        public char Sexo { get; set; } // 'M' para Masculino/Hombre, 'F' para Femenino/Mujer

        public override string ToString()
        {
            string descSexo = (Sexo == 'M' || Sexo == 'm') ? "Hombre" : "Mujer";
            return $"[Nombre: {Nombre} | Sexo: {descSexo}]";
        }
    }

    // Nodo para construir nuestra propia Cola de forma dinámica
    public class NodoCola
    {
        public Persona Dato { get; set; }
        public NodoCola Siguiente { get; set; }

        public NodoCola(Persona persona)
        {
            Dato = persona;
            Siguiente = null;
        }
    }

    // Estructura de Cola personalizada codificando Enqueue() y Dequeue()
    public class ColaPersonas
    {
        private NodoCola frente; // Elemento que saldrá primero (el primero en llegar)
        private NodoCola fin;    // Elemento que acaba de llegar al final de la fila
        public int Count { get; private set; }

        public ColaPersonas()
        {
            frente = null;
            fin = null;
            Count = 0;
        }

        // ==========================================
        // Método ENQUEUE(): Inserta un elemento al final de la cola
        // ==========================================
        public void Enqueue(Persona persona)
        {
            NodoCola nuevoNodo = new NodoCola(persona);
            if (IsEmpty())
            {
                frente = nuevoNodo;
                fin = nuevoNodo;
            }
            else
            {
                fin.Siguiente = nuevoNodo;
                fin = nuevoNodo;
            }
            Count++;
        }

        // ==========================================
        // Método DEQUEUE(): Extrae y remueve el elemento del frente de la cola
        // ==========================================
        public Persona Dequeue()
        {
            if (IsEmpty())
            {
                return null;
            }

            Persona datoRetornado = frente.Dato;
            frente = frente.Siguiente;
            Count--;

            if (frente == null)
            {
                fin = null; // Si la cola queda vacía, el puntero 'fin' también se limpia
            }

            return datoRetornado;
        }

        // Método para verificar si la cola está vacía
        public bool IsEmpty()
        {
            return frente == null;
        }

        // Método para mostrar todos los elementos de la cola desde el frente hasta el fin
        public void MostrarCola()
        {
            if (IsEmpty())
            {
                Console.WriteLine("  (Cola vacía)");
                return;
            }

            NodoCola actual = frente;
            int pos = 1;
            while (actual != null)
            {
                Console.WriteLine($"  {pos}. {actual.Dato}");
                actual = actual.Siguiente;
                pos++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ColaPersonas colaInicial = new ColaPersonas();
            bool salir = false;

            // Cargamos algunos datos iniciales de prueba en la cola
            colaInicial.Enqueue(new Persona { Nombre = "Carlos", Sexo = 'M' });
            colaInicial.Enqueue(new Persona { Nombre = "Lucía", Sexo = 'F' });
            colaInicial.Enqueue(new Persona { Nombre = "Mateo", Sexo = 'M' });
            colaInicial.Enqueue(new Persona { Nombre = "Sofía", Sexo = 'F' });
            colaInicial.Enqueue(new Persona { Nombre = "Juan", Sexo = 'M' });

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== NATATORIO - REVISIÓN MÉDICA (DIVISIÓN DE COLAS) ===");
                Console.WriteLine("\nCola Inicial actual (Fila general):");
                colaInicial.MostrarCola();

                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Agregar nueva persona a la cola inicial (`Enqueue`)");
                Console.WriteLine("2. Dividir la cola en Hombres y Mujeres y mostrar resultados");
                Console.WriteLine("3. Salir");
                Console.Write("\nSeleccione una opción (1-3): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el nombre de la persona: ");
                        string nombre = Console.ReadLine();
                        Console.Write("Ingrese el sexo (M para Hombre / F para Mujer): ");
                        string sexoInput = Console.ReadLine()?.ToUpper().Trim();

                        if (!string.IsNullOrEmpty(nombre) && (sexoInput == "M" || sexoInput == "F"))
                        {
                            Persona nuevaPersona = new Persona
                            {
                                Nombre = nombre,
                                Sexo = sexoInput[0]
                            };
                            colaInicial.Enqueue(nuevaPersona);
                            Console.WriteLine("¡Persona encolada con éxito en la fila general!");
                        }
                        else
                        {
                            Console.WriteLine("Datos inválidos. Asegúrese de ingresar M o F.");
                        }
                        break;

                    case "2":
                        if (colaInicial.IsEmpty())
                        {
                            Console.WriteLine("La cola inicial está vacía. No hay datos para separar.");
                            break;
                        }

                        // Creamos las dos nuevas colas solicitadas por la consigna
                        ColaPersonas colaHombres = new ColaPersonas();
                        ColaPersonas colaMujeres = new ColaPersonas();

                        // Cola temporal para conservar los datos de la original mientras la procesamos
                        ColaPersonas colaTemporal = new ColaPersonas();

                        // Desapilamos (desencolamos) de la inicial para clasificar
                        while (!colaInicial.IsEmpty())
                        {
                            Persona p = colaInicial.Dequeue();

                            // Separamos según el atributo de sexo
                            if (p.Sexo == 'M' || p.Sexo == 'm')
                            {
                                colaHombres.Enqueue(p);
                            }
                            else
                            {
                                colaMujeres.Enqueue(p);
                            }

                            // Guardamos temporalmente para luego restaurar la cola original
                            colaTemporal.Enqueue(p);
                        }

                        // Restauramos la cola inicial desde la temporal
                        while (!colaTemporal.IsEmpty())
                        {
                            colaInicial.Enqueue(colaTemporal.Dequeue());
                        }

                        // Mostramos los resultados en consola tal como pide el ejercicio
                        Console.WriteLine("==================================================");
                        Console.WriteLine("            RESULTADO DE LA SEPARACIÓN            ");
                        Console.WriteLine("==================================================");

                        Console.WriteLine("\n1. Cola Inicial (Fila general conservada):");
                        colaInicial.MostrarCola();

                        Console.WriteLine("\n2. Nueva Cola de HOMBRES[cite: 14]:");
                        colaHombres.MostrarCola();

                        Console.WriteLine("\n3. Nueva Cola de MUJERES[cite: 14]:");
                        colaMujeres.MostrarCola();
                        Console.WriteLine("==================================================");
                        break;

                    case "3":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
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