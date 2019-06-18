using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практика8
{
    class Program
    {
        static Random rnd = new Random();
        static int[,] RandomMatrix()
        {
            int choose = rnd.Next(1,4);
            switch (choose)
            {
                case 1:
                    {
                        int [,] matrix = {
                            { 0, 1, 1, 0, 0, 0 },
                            { 1, 0, 0, 1, 0, 1 },
                            { 1, 0, 0, 1, 1, 0 },
                            { 0, 1, 1, 0, 1, 1 },
                            { 0, 0, 1, 1, 0, 1 },
                            { 0, 1, 0, 1, 1, 0 }                            
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
                case 2:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 1, 0, 0, 0, 0 },
                            { 1, 0, 1, 0, 1, 0, 0, 0 },
                            { 1, 1, 0, 1, 1, 1, 0, 0 },
                            { 1, 0, 1, 0, 0, 1, 1, 0 },
                            { 0, 1, 1, 0, 0, 1, 0, 1 },
                            { 0, 0, 1, 1, 1, 0, 1, 1 },
                            { 0, 0, 0, 1, 0, 1, 0, 1 },
                            { 0, 0, 0, 0, 1, 1, 1, 0 }
                        };
                        ShowArray(matrix);
                        return matrix;
                    }                   
                case 3:
                    {
                        int[,] matrix = {
                            { 0, 1, 1, 0, 0, 0, 0 },
                            { 1, 0, 0, 1, 0, 1, 0 },
                            { 1, 0, 0, 0, 1, 1, 1 },
                            { 0, 1, 0, 0, 1, 1, 0 },
                            { 0, 0, 1, 1, 0, 0, 1 },
                            { 0, 1, 1, 1, 0, 0, 0 },
                            { 0, 0, 1, 0, 1, 0, 0,},
                        };
                        ShowArray(matrix);
                        return matrix;
                    }
            }
            return null;
        }

        static List<List<int>> FindMinColors (int [,] m)
        {
            int l = m.GetLength(0);
            bool[] check = new bool[m.GetLength(0)];
            List<List<int>> colors = new List<List<int>>();
            for (int i = 0; i< m.GetLength(0); i++)
            {
                for (int j=0; j<m.GetLength(1); j++)
                {
                    List<int> NewColor = new List<int>();
                    if (i!=j && m[i,j] == 0 && !check[i] && !check[j])
                    {                         
                        NewColor.Add(i);
                        NewColor.Add(j);
                        check[i] = true; check[j] = true;
                        colors.Add(NewColor);
                    }
                    if (j == m.GetLength(1) - 1 && !check[i] && !check[j])
                    {
                        NewColor.Add(i);
                        check[i] = true;
                        colors.Add(NewColor);
                    }
                }
            }
            return colors;
        }
        static List<List<int>> MakeMoreColors (List<List<int>> c, int v, int k)
        {
            int count = c.Count;
            List<List<int>> c1 = new List<List<int>>();
            foreach (List<int> a in c)
            {
                if (a.Count == 2 && count != k)
                {
                    List<int> NC1 = new List<int>();
                    NC1.Add(a[0]);
                    c1.Add(NC1);
                    List<int> NC2 = new List<int>();
                    NC2.Add(a[1]);
                    c1.Add(NC2);
                    count++;
                }
                else c1.Add(a);
            }            
            return c1;            
        }

        static void ShowList (List<List<int>> c)
        {
            Console.WriteLine("\nЦвета   Вершины");
            int i = 1;
            foreach (List<int> a in c)
            {                
                Console.Write(i + " цвет: ");
                foreach (int b in a)
                {
                    Console.Write(b + " ");
                }
                i++;
                Console.WriteLine();
            }
        }

        static void ShowArray(int[,] arr)
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                    Console.Write(string.Format("{0,3}", arr[i, j]));
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            bool ok = false;
            int k = 0;
            do
            {
                try
                {
                    Console.WriteLine("Введите k (кол-во цветов):");
                    k = Convert.ToInt32(Console.ReadLine());
                    ok = true;
                    if (k<=0 || k>=10)
                    {
                        ok = false;
                        Console.WriteLine("0<k<10");
                    }
                }
                catch
                {
                    ok = false;
                    Console.WriteLine("Ошибка!");
                }
            }
            while (!ok);
            
            int[,] matrix = RandomMatrix();
            if (k > matrix.GetLength(0)) Console.WriteLine("Число вершин меньше K");
            else
            {
                List<List<int>> colors = FindMinColors(matrix);
                if (k < colors.Count) Console.WriteLine("Не достаточно цветов, чтобы программа могла правильно раскрасить граф");
                if (k == colors.Count) ShowList(colors);
                if (k>colors.Count)
                {
                    int vertex = matrix.GetLength(0);
                    colors = MakeMoreColors(colors, vertex, k);
                    ShowList(colors);
                }
            }
            
            Console.ReadKey();
        }
    }
}
