using System;

// ORDENAMIENTO RECURSIVO DE UN VECTOR

using System;

namespace TrabajoPracticoOrdenamiento
{
    class Program
    {
        static void Main(string[] args)
        { 

            // 1. Definimos un vector (array) de números enteros desordenados
            int[] vector = { 34, 12, 5, 67, 23, 9, 50 };

            // Mostramos el vector original (desordenado)
            Console.WriteLine("\nVector DESORDENADO:");
            ImprimirVector(vector);

            // 2. Llamamos a la función recursiva pasando el vector y su longitud total
            Console.WriteLine("\n--- Iniciando proceso de ordenamiento recursivo ---");
            OrdenarVectorRecursivo(vector, vector.Length);

            // 3. Mostramos el vector resultante ya ordenado
            Console.WriteLine("\nVector ORDENADO con éxito:");
            ImprimirVector(vector);

            // Pausa antes de salir
            Console.WriteLine("\nPresiona cualquier tecla para salir del programa...");
            Console.ReadKey();
        }

        
        // Función recursiva que ordena un vector paso a paso.    
        static void OrdenarVectorRecursivo(int[] v, int n)
        {
            // CASO BASE: Si el tamaño del segmento a evaluar es 1, ya está ordenado y detenemos la recursividad.
            if (n <= 1)
            {
                return;
            }

            // Realizamos una pasada del método burbuja para ubicar el elemento mayor al final del vector actual.
            for (int i = 0; i < n - 1; i++)
            {
                if (v[i] > v[i + 1])
                {
                    // Intercambio de valores (Swap)
                    int aux = v[i];
                    v[i] = v[i + 1];
                    v[i + 1] = aux;
                }
            }

            // Imprimimos cómo va cambiando el vector en cada nivel
            Console.Write($"-> Estado parcial (n={n - 1}): ");
            ImprimirVector(v);

            // Invocamos a la función reduciendo 'n' en 1, 
            // ya que el último elemento de este nivel ya quedó en su posición correcta y definitiva.
            OrdenarVectorRecursivo(v, n - 1);
        }

        
        // Método auxiliar para imprimir los elementos del vector en formato de lista.
        
        static void ImprimirVector(int[] v)
        {
            Console.WriteLine("[ " + string.Join(", ", v) + " ]");
        }
    }
}