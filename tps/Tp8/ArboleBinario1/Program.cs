using System;

namespace ArbolBinarioApp
{
    // Clase que define la estructura de cada nodo del árbol
    public class NodoArbol
    {
        public int Valor { get; set; }
        public NodoArbol Izquierdo { get; set; }
        public NodoArbol Derecho { get; set; }

        public NodoArbol(int valor)
        {
            Valor = valor;
            Izquierdo = null;
            Derecho = null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Construimos manualmente el árbol binario completo y equilibrado de 4 niveles
            // Raíz
            NodoArbol raiz = new NodoArbol(8);

            // Nivel 2
            raiz.Izquierdo = new NodoArbol(4);
            raiz.Derecho = new NodoArbol(12);

            // Nivel 3
            raiz.Izquierdo.Izquierdo = new NodoArbol(2);
            raiz.Izquierdo.Derecho = new NodoArbol(6);
            raiz.Derecho.Izquierdo = new NodoArbol(10);
            raiz.Derecho.Derecho = new NodoArbol(14);

            // Nivel 4 (Hojas)
            raiz.Izquierdo.Izquierdo.Izquierdo = new NodoArbol(1);
            raiz.Izquierdo.Izquierdo.Derecho = new NodoArbol(3);
            raiz.Izquierdo.Derecho.Izquierdo = new NodoArbol(5);
            raiz.Izquierdo.Derecho.Derecho = new NodoArbol(7);

            raiz.Derecho.Izquierdo.Izquierdo = new NodoArbol(9);
            raiz.Derecho.Izquierdo.Derecho = new NodoArbol(11);
            raiz.Derecho.Derecho.Izquierdo = new NodoArbol(13);
            raiz.Derecho.Derecho.Derecho = new NodoArbol(15);

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== ÁRBOL BINARIO (NIVEL 4 - COMPLETO Y EQUILIBRADO) ===");
                Console.WriteLine("1. Mostrar recorrido Preorden (Raíz -> Izq -> Der)");
                Console.WriteLine("2. Mostrar recorrido Inorden (Izq -> Raíz -> Der)");
                Console.WriteLine("3. Mostrar recorrido Postorden (Izq -> Der -> Raíz)");
                Console.WriteLine("4. Mostrar todos los recorridos juntos");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una opción (1-5): ");

                string op = Console.ReadLine();
                Console.WriteLine();

                switch (op)
                {
                    case "1":
                        Console.Write("Recorrido Preorden: [ ");
                        PreOrden(raiz);
                        Console.WriteLine("]");
                        break;
                    case "2":
                        Console.Write("Recorrido Inorden: [ ");
                        InOrden(raiz);
                        Console.WriteLine("]");
                        break;
                    case "3":
                        Console.Write("Recorrido Postorden: [ ");
                        PostOrden(raiz);
                        Console.WriteLine("]");
                        break;
                    case "4":
                        Console.Write("Preorden:  [ "); PreOrden(raiz); Console.WriteLine("]");
                        Console.Write("Inorden:   [ "); InOrden(raiz); Console.WriteLine("]");
                        Console.Write("Postorden: [ "); PostOrden(raiz); Console.WriteLine("]");
                        break;
                    case "5":
                        salir = true;
                        Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
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

        // ==========================================
        // MÉTODOS DE RECORRIDO (RECURSIVOS)
        // ==========================================

        // 1. Preorden: Raíz -> Izquierda -> Derecha
        static void PreOrden(NodoArbol nodo)
        {
            if (nodo == null) return;

            Console.Write($"{nodo.Valor} "); // Visita la raíz
            PreOrden(nodo.Izquierdo);        // Recorre subárbol izquierdo
            PreOrden(nodo.Derecho);          // Recorre subárbol derecho
        }

        // 2. Inorden: Izquierda -> Raíz -> Derecha
        static void InOrden(NodoArbol nodo)
        {
            if (nodo == null) return;

            InOrden(nodo.Izquierdo);         // Recorre subárbol izquierdo
            Console.Write($"{nodo.Valor} "); // Visita la raíz
            InOrden(nodo.Derecho);           // Recorre subárbol derecho
        }

        // 3. Postorden: Izquierda -> Derecha -> Raíz
        static void PostOrden(NodoArbol nodo)
        {
            if (nodo == null) return;

            PostOrden(nodo.Izquierdo);       // Recorre subárbol izquierdo
            PostOrden(nodo.Derecho);         // Recorre subárbol derecho
            Console.Write($"{nodo.Valor} "); // Visita la raíz
        }
    }
}