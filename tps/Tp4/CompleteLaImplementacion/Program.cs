using System;

namespace EjercicioIntercambioNodos
{
    // Clase que representa el dato almacenado en el nodo
    public class Elemento
    {
        public int Valor { get; set; }

        public override string ToString()
        {
            return $"[Valor: {Valor}]";
        }
    }

    // Nodo para la Lista Doblemente Enlazada
    public class NodoDoble
    {
        public Elemento Dato { get; set; }
        public NodoDoble Siguiente { get; set; }
        public NodoDoble Anterior { get; set; }

        public NodoDoble(Elemento elemento)
        {
            Dato = elemento;
            Siguiente = null;
            Anterior = null;
        }
    }

    // Estructura de Lista Doblemente Enlazada con los métodos solicitados
    public class ListaDobleIntercambio
    {
        private NodoDoble cabeza;
        private NodoDoble cola;
        public int Count { get; private set; }

        public ListaDobleIntercambio()
        {
            cabeza = null;
            cola = null;
            Count = 0;
        }

        // Método auxiliar para agregar elementos al final
        public void Agregar(int valor)
        {
            NodoDoble nuevo = new NodoDoble(new Elemento { Valor = valor });
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

        // Método auxiliar para buscar un nodo por su valor
        private NodoDoble BuscarNodo(int valor)
        {
            NodoDoble actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Valor == valor) return actual;
                actual = actual.Siguiente;
            }
            return null;
        }

        // =========================================================================
        // 1. public void IntercambiarDerecha(int numero)[cite: 10]
        // Intercambia el nodo actual con el nodo que está a su derecha (siguiente)
        // =========================================================================
        public void IntercambiarDerecha(int numero)
        {
            NodoDoble nodo1 = BuscarNodo(numero);
            if (nodo1 == null || nodo1.Siguiente == null)
            {
                Console.WriteLine("No se puede realizar el intercambio hacia la derecha (nodo no encontrado o es el último).");
                return;
            }

            NodoDoble nodo2 = nodo1.Siguiente; // El nodo de la derecha

            // Realizamos el intercambio de datos (la forma más segura y limpia en listas enlazadas)
            Elemento temp = nodo1.Dato;
            nodo1.Dato = nodo2.Dato;
            nodo2.Dato = temp;

            Console.WriteLine($"¡Intercambio a la derecha exitoso entre {nodo2.Dato.Valor} y {nodo1.Dato.Valor}!");
        }

        // =========================================================================
        // 2. public void IntercambiarIzquierda(int numero)[cite: 10]
        // Intercambia el nodo actual con el nodo que está a su izquierda (anterior)
        // =========================================================================
        public void IntercambiarIzquierda(int numero)
        {
            NodoDoble nodo1 = BuscarNodo(numero);
            if (nodo1 == null || nodo1.Anterior == null)
            {
                Console.WriteLine("No se puede realizar el intercambio hacia la izquierda (nodo no encontrado o es el primero).");
                return;
            }

            NodoDoble nodo2 = nodo1.Anterior; // El nodo de la izquierda

            // Intercambiamos los datos entre ambos nodos
            Elemento temp = nodo1.Dato;
            nodo1.Dato = nodo2.Dato;
            nodo2.Dato = temp;

            Console.WriteLine($"¡Intercambio a la izquierda exitoso entre {nodo2.Dato.Valor} y {nodo1.Dato.Valor}!");
        }

        // =========================================================================
        // 3. public void Intercambiar(int numero1, int numero2)[cite: 10]
        // Intercambia de posición dos nodos específicos de la lista
        // =========================================================================
        public void Intercambiar(int numero1, int numero2)
        {
            if (numero1 == numero2)
            {
                Console.WriteLine("Los números a intercambiar son iguales.");
                return;
            }

            NodoDoble n1 = BuscarNodo(numero1);
            NodoDoble n2 = BuscarNodo(numero2);

            if (n1 == null || n2 == null)
            {
                Console.WriteLine("Uno o ambos números no fueron encontrados en la lista.");
                return;
            }

            // Intercambiamos los contenidos de los nodos
            Elemento temp = n1.Dato;
            n1.Dato = n2.Dato;
            n2.Dato = temp;

            Console.WriteLine($"¡Intercambio general exitoso entre los valores {numero1} y {numero2}!");
        }

        // Método para mostrar el listado actual
        public void MostrarListado()
        {
            if (cabeza == null)
            {
                Console.WriteLine("La lista está vacía.");
                return;
            }

            NodoDoble actual = cabeza;
            int i = 1;
            Console.Write("Estado de la lista: [ ");
            while (actual != null)
            {
                Console.Write($"{actual.Dato.Valor} ");
                actual = actual.Siguiente;
            }
            Console.WriteLine("]");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ListaDobleIntercambio lista = new ListaDobleIntercambio();

            // Cargamos algunos valores iniciales para probar
            lista.Agregar(10);
            lista.Agregar(20);
            lista.Agregar(30);
            lista.Agregar(40);
            lista.Agregar(50);

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== EJERCICIO 7: INTERCAMBIO DE NODOS ===");
                lista.MostrarListado();
                Console.WriteLine("\nMenú de opciones:");
                Console.WriteLine("1. Intercambiar Derecha (`IntercambiarDerecha`)");
                Console.WriteLine("2. Intercambiar Izquierda (`IntercambiarIzquierda`)");
                Console.WriteLine("3. Intercambiar dos valores específicos (`Intercambiar`)");
                Console.WriteLine("4. Agregar nuevo valor a la lista");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una opción (1-5): ");

                string op = Console.ReadLine();
                Console.WriteLine();

                switch (op)
                {
                    case "1":
                        Console.Write("Ingrese el valor del nodo a intercambiar con su derecho: ");
                        if (int.TryParse(Console.ReadLine(), out int valDer))
                        {
                            lista.IntercambiarDerecha(valDer);
                        }
                        break;
                    case "2":
                        Console.Write("Ingrese el valor del nodo a intercambiar con su izquierdo: ");
                        if (int.TryParse(Console.ReadLine(), out int valIzq))
                        {
                            lista.IntercambiarIzquierda(valIzq);
                        }
                        break;
                    case "3":
                        Console.Write("Ingrese el primer valor: ");
                        int.TryParse(Console.ReadLine(), out int v1);
                        Console.Write("Ingrese el segundo valor: ");
                        int.TryParse(Console.ReadLine(), out int v2);
                        lista.Intercambiar(v1, v2);
                        break;
                    case "4":
                        Console.Write("Ingrese el valor entero a agregar: ");
                        if (int.TryParse(Console.ReadLine(), out int nuevoVal))
                        {
                            lista.Agregar(nuevoVal);
                            Console.WriteLine("¡Valor agregado con éxito!");
                        }
                        break;
                    case "5":
                        salir = true;
                        Console.WriteLine("¡Trabajo práctico finalizado con éxito! Saliendo...");
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