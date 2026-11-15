using System;

namespace ColaFiltradoTickets
{
    // Clase que representa a la persona en la cola
    public class PersonaTicket
    {
        public string Nombre { get; set; }
        public bool TieneTicket { get; set; } // true si tiene ticket, false si es un "colado"

        public override string ToString()
        {
            string estadoTicket = TieneTicket ? "Con Ticket" : "Sin Ticket (Colado)";
            return $"[Persona: {Nombre} | Estado: {estadoTicket}]";
        }
    }

    // Nodo para construir la Cola de forma dinámica
    public class NodoColaTicket
    {
        public PersonaTicket Dato { get; set; }
        public NodoColaTicket Siguiente { get; set; }

        public NodoColaTicket(PersonaTicket persona)
        {
            Dato = persona;
            Siguiente = null;
        }
    }

    // Estructura de Cola personalizada codificando Enqueue() y Dequeue()
    public class ColaPersonasTicket
    {
        private NodoColaTicket frente;
        private NodoColaTicket fin;
        public int Count { get; private set; }

        public ColaPersonasTicket()
        {
            frente = null;
            fin = null;
            Count = 0;
        }

        // Método Enqueue(): Agrega al final de la cola
        public void Enqueue(PersonaTicket persona)
        {
            NodoColaTicket nuevoNodo = new NodoColaTicket(persona);
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

        // Método Dequeue(): Remueve y devuelve el elemento del frente
        public PersonaTicket Dequeue()
        {
            if (IsEmpty()) return null;

            PersonaTicket retornado = frente.Dato;
            frente = frente.Siguiente;
            Count--;

            if (frente == null)
            {
                fin = null;
            }

            return retornado;
        }

        // Método para verificar si está vacía
        public bool IsEmpty()
        {
            return frente == null;
        }

        // Método para mostrar todos los elementos de la cola
        public void MostrarCola()
        {
            if (IsEmpty())
            {
                Console.WriteLine("  (Cola vacía)");
                return;
            }

            NodoColaTicket actual = frente;
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
            ColaPersonasTicket colaInicial = new ColaPersonasTicket();
            bool salir = false;

            // Cargamos datos de prueba iniciales (mezclando personas con y sin ticket)
            colaInicial.Enqueue(new PersonaTicket { Nombre = "Martín", TieneTicket = true });
            colaInicial.Enqueue(new PersonaTicket { Nombre = "Lucas", TieneTicket = false }); // Colado
            colaInicial.Enqueue(new PersonaTicket { Nombre = "Valeria", TieneTicket = true });
            colaInicial.Enqueue(new PersonaTicket { Nombre = "Esteban", TieneTicket = false }); // Colado
            colaInicial.Enqueue(new PersonaTicket { Nombre = "Camila", TieneTicket = true });

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== CONTROL DE FILA - FILTRADO DE 'COLADOS' (TICKETS) ===");
                Console.WriteLine("\nCola Inicial actual (Fila con personas mezcladas):");
                colaInicial.MostrarCola();

                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Agregar nueva persona a la fila (`Enqueue`)");
                Console.WriteLine("2. Procesar fila: Filtrar colados y mostrar resultados");
                Console.WriteLine("3. Salir");
                Console.Write("\nSeleccione una opción (1-3): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el nombre de la persona: ");
                        string nombre = Console.ReadLine();
                        Console.Write("¿Tiene ticket? (s/n): ");
                        string respuesta = Console.ReadLine()?.ToLower().Trim();

                        if (!string.IsNullOrEmpty(nombre) && (respuesta == "s" || respuesta == "n"))
                        {
                            PersonaTicket p = new PersonaTicket
                            {
                                Nombre = nombre,
                                TieneTicket = (respuesta == "s")
                            };
                            colaInicial.Enqueue(p);
                            Console.WriteLine("¡Persona agregada a la cola inicial con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("Datos inválidos.");
                        }
                        break;

                    case "2":
                        if (colaInicial.IsEmpty())
                        {
                            Console.WriteLine("La cola inicial está vacía.");
                            break;
                        }

                        // Colas para cumplir con la consigna
                        ColaPersonasTicket colaFinalValida = new ColaPersonasTicket();
                        ColaPersonasTicket colaRetirados = new ColaPersonasTicket();

                        // Cola auxiliar para conservar la cola inicial original en memoria
                        ColaPersonasTicket colaBackup = new ColaPersonasTicket();

                        // Procesamos siguiendo la nota: desencolamos cada elemento
                        while (!colaInicial.IsEmpty())
                        {
                            PersonaTicket actual = colaInicial.Dequeue();

                            if (actual.TieneTicket)
                            {
                                // Si tiene ticket, se vuelve a encolar en la cola final válida[cite: 16]
                                colaFinalValida.Enqueue(actual);
                            }
                            else
                            {
                                // Si no tiene ticket, se retira[cite: 16]
                                colaRetirados.Enqueue(actual);
                            }

                            // Respaldamos en el backup para no perder la cola original
                            colaBackup.Enqueue(actual);
                        }

                        // Restauramos la cola inicial desde el backup
                        while (!colaBackup.IsEmpty())
                        {
                            colaInicial.Enqueue(colaBackup.Dequeue());
                        }

                        // Mostramos los resultados pedidos por el enunciado[cite: 16]
                        Console.WriteLine("==================================================");
                        Console.WriteLine("               RESULTADOS DEL PROCESO             ");
                        Console.WriteLine("==================================================");

                        Console.WriteLine("\n1. COLA INICIAL (Fila original conservada)[cite: 16]:");
                        colaInicial.MostrarCola();

                        Console.WriteLine("\n2. ELEMENTOS RETIRADOS (Personas 'coladas' sin ticket)[cite: 16]:");
                        colaRetirados.MostrarCola();

                        Console.WriteLine("\n3. COLA FINAL (Personas válidas con ticket atendidas)[cite: 16]:");
                        colaFinalValida.MostrarCola();
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