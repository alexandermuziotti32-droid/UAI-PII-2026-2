using System;

namespace ArbolBinarioTDAApp
{
    // ==========================================
    // TDA: NODO DEL ÁRBOL BINARIO
    // ==========================================
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

    // ==========================================
    // TDA: ÁRBOL BINARIO (GESTIÓN Y MANTENIMIENTO)
    // ==========================================
    public class ArbolBinarioTDA
    {
        public NodoArbol Raiz { get; private set; }

        public ArbolBinarioTDA()
        {
            Raiz = null;
        }

        // 1. Agregar nodo raíz
        public bool AgregarRaiz(int valor)
        {
            if (Raiz != null)
            {
                Console.WriteLine("Error: Ya existe un nodo raíz en el árbol.");
                return false;
            }
            Raiz = new NodoArbol(valor);
            Console.WriteLine($"¡Nodo raíz [{valor}] agregado con éxito!");
            return true;
        }

        // Método auxiliar para buscar un nodo por su valor en el árbol
        public NodoArbol BuscarNodo(NodoArbol actual, int valorBuscado)
        {
            if (actual == null) return null;
            if (actual.Valor == valorBuscado) return actual;

            // Busca recursivamente en la rama izquierda
            NodoArbol encontrado = BuscarNodo(actual.Izquierdo, valorBuscado);
            if (encontrado != null) return encontrado;

            // Si no está en la izquierda, busca en la rama derecha
            return BuscarNodo(actual.Derecho, valorBuscado);
        }

        // 2. Agregar nodo en la rama derecha del nodo seleccionado[cite: 16]
        public bool AgregarDerecha(int valorPadre, int valorNuevo)
        {
            NodoArbol padre = BuscarNodo(Raiz, valorPadre);
            if (padre == null)
            {
                Console.WriteLine($"No se encontró el nodo padre con valor [{valorPadre}].");
                return false;
            }

            if (padre.Derecho != null)
            {
                Console.WriteLine($"Error: El nodo [{valorPadre}] ya tiene ocupada su rama derecha.");
                return false;
            }

            padre.Derecho = new NodoArbol(valorNuevo);
            Console.WriteLine($"¡Nodo [{valorNuevo}] agregado a la derecha de [{valorPadre}] con éxito!");
            return true;
        }

        // 3. Agregar nodo en la rama izquierda del nodo seleccionado[cite: 16]
        public bool AgregarIzquierda(int valorPadre, int valorNuevo)
        {
            NodoArbol padre = BuscarNodo(Raiz, valorPadre);
            if (padre == null)
            {
                Console.WriteLine($"No se encontró el nodo padre con valor [{valorPadre}].");
                return false;
            }

            if (padre.Izquierdo != null)
            {
                Console.WriteLine($"Error: El nodo [{valorPadre}] ya tiene ocupada su rama izquierda.");
                return false;
            }

            padre.Izquierdo = new NodoArbol(valorNuevo);
            Console.WriteLine($"¡Nodo [{valorNuevo}] agregado a la izquierda de [{valorPadre}] con éxito!");
            return true;
        }

        // Recorrido Preorden (Raíz -> Izquierda -> Derecha)[cite: 16]
        public void PreOrden(NodoArbol nodo)
        {
            if (nodo == null) return;
            Console.Write($"{nodo.Valor} ");
            PreOrden(nodo.Izquierdo);
            PreOrden(nodo.Derecho);
        }

        // Recorrido Inorden (Izquierda -> Raíz -> Derecha)[cite: 16]
        public void InOrden(NodoArbol nodo)
        {
            if (nodo == null) return;
            InOrden(nodo.Izquierdo);
            Console.Write($"{nodo.Valor} ");
            InOrden(nodo.Derecho);
        }

        // Recorrido Posorden (Izquierda -> Derecha -> Raíz)[cite: 16]
        public void PosOrden(NodoArbol nodo)
        {
            if (nodo == null) return;
            PosOrden(nodo.Izquierdo);
            PosOrden(nodo.Derecho);
            Console.Write($"{nodo.Valor} ");
        }

        // Método para visualizar la estructura en consola (simulando vista jerárquica)
        public void MostrarEstructura(NodoArbol nodo, string indentacion, bool esUltimo)
        {
            if (nodo == null) return;

            Console.Write(indentacion);
            if (esUltimo)
            {
                Console.Write("└── R-- ");
                indentacion += "    ";
            }
            else
            {
                Console.Write("├── ├── ");
                indentacion += "|   ";
            }
            Console.WriteLine(nodo.Valor);

            MostrarEstructura(nodo.Izquierdo, indentacion, false);
            MostrarEstructura(nodo.Derecho, indentacion, true);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArbolBinarioTDA miArbol = new ArbolBinarioTDA();
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE GESTIÓN DE ÁRBOL BINARIO (TDA) ===");
                Console.WriteLine("Estructura actual del árbol:");
                if (miArbol.Raiz == null)
                {
                    Console.WriteLine("  (Árbol vacío)");
                }
                else
                {
                    miArbol.MostrarEstructura(miArbol.Raiz, "", true);
                }

                Console.WriteLine("\n--- MENÚ DE OPERACIONES ---");
                Console.WriteLine("1. Agregar nodo raíz[cite: 16]");
                Console.WriteLine("2. Agregar nodo en rama DERECHA del nodo seleccionado[cite: 16]");
                Console.WriteLine("3. Agregar nodo en rama IZQUIERDA del nodo seleccionado[cite: 16]");
                Console.WriteLine("4. Realizar recorrido Preorden[cite: 16]");
                Console.WriteLine("5. Realizar recorrido Inorden[cite: 16]");
                Console.WriteLine("6. Realizar recorrido Posorden[cite: 16]");
                Console.WriteLine("7. Salir");
                Console.Write("\nSeleccione una opción (1-7): ");

                string opcion = Console.ReadLine();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Ingrese el valor entero para la raíz: ");
                        if (int.TryParse(Console.ReadLine(), out int valRaiz))
                        {
                            miArbol.AgregarRaiz(valRaiz);
                        }
                        else
                        {
                            Console.WriteLine("Valor inválido.");
                        }
                        break;

                    case "2":
                        if (miArbol.Raiz == null)
                        {
                            Console.WriteLine("El árbol está vacío. Debe agregar una raíz primero.");
                            break;
                        }
                        Console.Write("Ingrese el valor del nodo padre existente: ");
                        if (int.TryParse(Console.ReadLine(), out int padreDer))
                        {
                            Console.Write("Ingrese el valor del NUEVO nodo derecho: ");
                            if (int.TryParse(Console.ReadLine(), out int nuevoDer))
                            {
                                miArbol.AgregarDerecha(padreDer, nuevoDer);
                            }
                        }
                        break;

                    case "3":
                        if (miArbol.Raiz == null)
                        {
                            Console.WriteLine("El árbol está vacío. Debe agregar una raíz primero.");
                            break;
                        }
                        Console.Write("Ingrese el valor del nodo padre existente: ");
                        if (int.TryParse(Console.ReadLine(), out int padreIzq))
                        {
                            Console.Write("Ingrese el valor del NUEVO nodo izquierdo: ");
                            if (int.TryParse(Console.ReadLine(), out int nuevoIzq))
                            {
                                miArbol.AgregarIzquierda(padreIzq, nuevoIzq);
                            }
                        }
                        break;

                    case "4":
                        Console.Write("Recorrido Preorden: [ ");
                        miArbol.PreOrden(miArbol.Raiz);
                        Console.WriteLine("]");
                        break;

                    case "5":
                        Console.Write("Recorrido Inorden: [ ");
                        miArbol.InOrden(miArbol.Raiz);
                        Console.WriteLine("]");
                        break;

                    case "6":
                        Console.Write("Recorrido Posorden: [ ");
                        miArbol.PosOrden(miArbol.Raiz);
                        Console.WriteLine("]");
                        break;

                    case "7":
                        salir = true;
                        Console.WriteLine("Saliendo del programa. ¡Mucho éxito con tu entrega!");
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