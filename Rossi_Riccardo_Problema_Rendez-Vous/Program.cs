using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rossi_Riccardo_Problema_Rendez_Vous
{
    class Program
    {
        static int[] v = new int[1000];
        static int[] w = new int[1000];
        static int minimo = int.MaxValue;
        static double media = 0;

        static SemaphoreSlim n = new SemaphoreSlim(1);
        static SemaphoreSlim n1 = new SemaphoreSlim(1);

        static Random r = new Random();

        static void Main(string[] args)
        {
            Thread t1 = new Thread(() => Metodo1());
            t1.Start();
            Thread t2 = new Thread(() => Metodo2());
            t2.Start();

            while (t1.IsAlive) { }
            while (t2.IsAlive) { }

            Console.WriteLine($"il minimo è: {minimo}");
            Console.WriteLine($"la media è: {media}");

            Console.ReadLine();
        }
        static void Metodo1()
        {
            for (int i = 0; i < v.Length; i++)
            {
                v[i] = r.Next(0, 1000);
                if (v[i] < minimo)
                    minimo = v[i];
            }
            n1.Release();
            n.Wait();
            for (int i = 0; i < w.Length; i++)
            {
                if (w[i] < minimo)
                    minimo = w[i];
            }
        }

        static void Metodo2()
        {
            for (int i = 0; i < w.Length; i++)
            {
                w[i] = r.Next(0, 1000);
                media += w[i];
            }
            n.Release();

            n1.Wait();
            for (int i = 0; i < v.Length; i++)
                media += v[i];
            media = media / 2000;
        }

    }
}