using System;

namespace TrabajoPracticoRecursividad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- EJERCICIO 1: RECURSIVIDAD INTERACTIVA ---");
            Console.WriteLine("Se mostrará la cuenta regresiva del 10 al 1 paso a paso.");
            Console.WriteLine("Presiona cualquier tecla para comenzar...\n");

            // Pausa inicial para que el usuario inicie cuando quiera
            Console.ReadKey();

            // Llamamos al método recursivo con el valor inicial 10
            ImprimirPasoAPaso(10);

            Console.WriteLine("\n\n¡Cuenta regresiva finalizada!");
            Console.WriteLine("Presiona cualquier tecla para salir del programa...");
            Console.ReadKey();
        }

        static void ImprimirPasoAPaso(int n)
        {
            // Condición de corte obligatoria para evitar un desbordamiento de pila (StackOverflow)
            if (n <= 0)
            {
                return;
            }

            //  Esperamos a que el usuario presione una tecla para revelar el número actual
            Console.WriteLine($"-> Presiona una tecla para ver el siguiente número...");
            Console.ReadKey();

            // Mostramos el número actual en pantalla
            Console.Write($"Número: {n}  ");

            //  El método se invoca a sí mismo restándole 1 al parámetro
            ImprimirPasoAPaso(n - 1);
        }
    }
}