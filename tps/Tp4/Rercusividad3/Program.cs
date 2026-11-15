using System;

//Ejercico 2 : Calculo de factorial con metedo recursivo

namespace TrabajoFactorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ingrese un numero entero para calcular su factorial: ");
            
            // Validamos que la entrada sea un número entero válido
            if (int.TryParse(Console.ReadLine(), out int numero) && numero  >= 0)
            {
                Console.WriteLine("--- Procesos de llamadas recursivas");

                // Llamamos al método recursivo y guardamos el resultado
                long resultado = CalcularFactorial(numero);

                Console.WriteLine($"\n El resultado Final, el factorial de {numero} es: {resultado}");
            }
            else
            {
                Console.WriteLine("por favor ingrese un numero valido (mayor o igual que 0)");
            }
            
            static long CalcularFactorial(int n)
            {
                if (n == 0 || n == 1)
                {
                    Console.WriteLine("Factorial({n}) = 1");
                    return 1;
                }

                Console.WriteLine("Desglosamos: {n} * factorial {n - 1} ");
                return n * CalcularFactorial(n - 1);

            }
        }
    }
}