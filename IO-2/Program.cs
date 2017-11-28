using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //należy każde zadanie uruchamiać osobno//
            //Zadanie6();
            //Zadanie7();
            Zadanie8();
            Console.ReadKey();
        }

        static void Zadanie6()
        {
            var fs = new FileStream("a.txt", FileMode.Open);
            var size = fs.Length;
            var buffer = new byte[size];
            var are = new AutoResetEvent(false);
            fs.BeginRead(buffer, 0, buffer.Length, Zadanie6AsyncCallback, new object[] { fs, buffer, are });
            are.WaitOne();
        }
        static void Zadanie6AsyncCallback(IAsyncResult state)
        {
            var fs = (FileStream)((object[])state.AsyncState)[0];
            var buffer = (byte[])((object[])state.AsyncState)[1];
            var are = (AutoResetEvent)((object[])state.AsyncState)[2];
            fs.Close();
            Console.WriteLine(Encoding.ASCII.GetString(buffer));
            are.Set();
        }

        static void Zadanie7()
        {
            var fs = new FileStream("a.txt", FileMode.Open);
            var size = fs.Length;
            var buffer = new byte[size];
            var result = fs.BeginRead(buffer, 0, buffer.Length, null, null);
            fs.EndRead(result);
            fs.Close();
            Console.WriteLine(Encoding.ASCII.GetString(buffer));
        }

        static void Zadanie8()
        {
            DelegateType recursiveFactorial = new DelegateType(RecursiveFactorial);
            var ar = recursiveFactorial.BeginInvoke(10, null, null);
            Console.WriteLine("Recursive factorial: " + recursiveFactorial.EndInvoke(ar));

            DelegateType iterativeFactorial = new DelegateType(IterativeFactorial);
            var ar2 = iterativeFactorial.BeginInvoke(10, null, null);
            Console.WriteLine("Iterative factorial: " + iterativeFactorial.EndInvoke(ar2));

            DelegateType recursivefib = new DelegateType(RecursiveFib);
            var ar3 = recursivefib.BeginInvoke(10, null, null);
            Console.WriteLine("Recursive fibonacci: " + recursivefib.EndInvoke(ar3));

            DelegateType interativefib = new DelegateType(IterativeFib);
            var ar4 = interativefib.BeginInvoke(10, null, null);
            Console.WriteLine("Interative fibonacci: " + interativefib.EndInvoke(ar4));
        }

        delegate int DelegateType(object argument);
        static int RecursiveFactorial(object argument)
        {
            var n = (int)argument;
            if (n < 2)
                return 1;
            else
                return n * RecursiveFactorial(n - 1);
        }

        static int IterativeFactorial(object argument)
        {
            var n = (int)argument;
            int result = 1;
            for (int i = 2; i <= n; ++i)
            {
                result *= i;
            }
            return result;
        }

        static int RecursiveFib(object argument)
        {
            var n = (int)argument;
            if ((n == 1) || (n == 2))
                return 1;
            else
                return RecursiveFib(n - 1) + RecursiveFib(n - 2);
        }

        static int IterativeFib(object argument)
        {
            var n = (int)argument;
            int a = 0;
            int b = 1;

            for (int i = 0; i < n; i++)
            {
                int temp = a;
                a = b;
                b = temp + b;
            }
            return a;
        }
    }
}