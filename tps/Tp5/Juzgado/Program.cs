using System;

namespace JuzgadoExpedientesPilas
{
    // Clase que define los datos del Expediente
    public class Expediente
    {
        public string Fecha { get; set; }
        public int NroExpediente { get; set; }
        public string Caratula { get; set; }

        public override string ToString()
        {
            return $"[Nº Exp: {NroExpediente}] Fecha: {Fecha} | Carátula: {Caratula}";
        }
    }

    // Nodo para construir nuestra propia Pila de forma dinámica
    public class NodoPilaExpediente
    {
        public Expediente Dato { get; set; }
        public NodoPilaExpediente Siguiente { get; set; }

        public NodoPilaExpediente(Expediente expediente)
        {
            Dato = expediente;
            Siguiente = null;
        }
    }

    // Estructura de Pila Ordenada con métodos Push() y Pop() personalizados
    public class PilaExpedientesOrdenada
    {
        private NodoPilaExpediente cima;
        public int Count { get; private set; }

        public PilaExpedientesOrdenada()
        {
            cima = null;
            Count = 0;
        }

        // ==========================================
        // Método PUSH(): Agrega un elemento a la cima
        // ==========================================
        public void Push(Expediente expediente)
        {
            NodoPilaExpediente nuevoNodo = new NodoPilaExpediente(expediente);
            nuevoNodo.Siguiente = cima;
            cima = nuevoNodo;
            Count++;
        }

        // ==========================================
        // Método POP(): Remueve y devuelve el elemento de la cima
        // ==========================================
        public Expediente Pop()
        {
            if (IsEmpty())
            {
                return null;
            }

            Expediente datoRetornado = cima.Dato;
            cima = cima.Siguiente;
            Count--;
            return datoRetornado;
        }

        // Método Peek(): Permite ver el elemento de la cima sin sacarlo
        public Expediente Peek()
        {
            if (IsEmpty()) return null;
            return cima.Dato;
        }

        // Método para verificar si la pila está vacía
        public bool IsEmpty()
        {
            return cima == null;
        }

       
        public void PushOrdenado(Expediente nuevoExpediente)
        {
            PilaExpedientesOrdenada pilaAuxiliar = new PilaExpedientesOrdenada();

            // Desapilamos de la principal a la auxiliar mientras los elementos de la cima 
            // sean mayores que el nuevo expediente (para ordenar de menor a mayor número).
            while (!IsEmpty() && Peek().NroExpediente > nuevoExpediente.NroExpediente)
            {
                pilaAuxiliar.Push(Pop());
            }

            // Insertamos el nuevo expediente en su posición ordenada correcta
            Push(nuevoExpediente);

            // Regresamos los elementos desde la pila auxiliar de vuelta a la principal
            while (!pilaAuxiliar.IsEmpty())
            {
                Push(pilaAuxiliar.Pop());
            }
        }

        // Método para mostrar el contenido de la pila desde la cima hacia abajo
        public void MostrarPila()
        {
            if (IsEmpty())
            {
                Console.WriteLine("(Mesa de entrada sin expedientes)");
                return;
            }

            NodoPilaExpediente actual = cima;
            int posicion = 1;
            while (actual != null)
            {
                Console.WriteLine($"  [{posicion}] {actual.Dato}");
                actual = actual.Siguiente;
                posicion++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            PilaExpedientesOrdenada mesaEntrada = new PilaExpedientesOrdenada();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== JUZGADO - CONTROL DE EXPEDIENTES (PILAS ORDENADAS) ===");
                Console.WriteLine("Estado actual de la pila de expedientes (Cima arriba):");
                mesaEntrada.MostrarPila();

                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Ingresar nuevo expediente (inserción ordenada por Nro)[cite: 13]");
                Console.WriteLine("2. Retirar expediente de la cima (`Pop`)");
                Console.WriteLine("3. Ver expediente en la cima (`Peek`)");
                Console.WriteLine("4. Salir");
                Console.Write("\nSeleccione una opción (1-4): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese la fecha del expediente (ej: 10/11/2026):[cite: 13] ");
                        string fecha = Console.ReadLine();

                        Console.Write("Ingrese el número de expediente (entero):[cite: 13] ");
                        if (int.TryParse(Console.ReadLine(), out int nroExp))
                        {
                            Console.Write("Ingrese la carátula / nombre del expediente:[cite: 13] ");
                            string caratula = Console.ReadLine();

                            Expediente nuevoExp = new Expediente
                            {
                                Fecha = string.IsNullOrEmpty(fecha) ? "S/F" : fecha,
                                NroExpediente = nroExp,
                                Caratula = string.IsNullOrEmpty(caratula) ? "Sin Carátula" : caratula
                            };

                            // Llamamos a nuestro método de inserción ordenada
                            mesaEntrada.PushOrdenado(nuevoExp);
                            Console.WriteLine("\n¡Expediente apilado y ordenado con éxito!");
                        }
                        else
                        {
                            Console.WriteLine("Número de expediente inválido.");
                        }
                        break;

                    case "2":
                        Expediente retirado = mesaEntrada.Pop();
                        if (retirado != null)
                        {
                            Console.WriteLine($"Expediente retirado de la cima: {retirado}");
                        }
                        else
                        {
                            Console.WriteLine("La pila está vacía, no hay expedientes para retirar.");
                        }
                        break;

                    case "3":
                        Expediente cima = mesaEntrada.Peek();
                        if (cima != null)
                        {
                            Console.WriteLine($"Expediente en la cima: {cima}");
                        }
                        else
                        {
                            Console.WriteLine("La mesa de entrada está vacía.");
                        }
                        break;

                    case "4":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema del juzgado. ¡Hasta luego!");
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