using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab8
{
    class Program
    {
        static void Main(string[] args)
        {
            bool goOut = false;
            CartesianTree<int> tree1 = new CartesianTree<int>();
            CartesianTree<int> tree2 = new CartesianTree<int>();
            do
            {
                switch (Subroutines.PrintMenu())
                {
                    //
                    // c - создать деревья и заполнить их случайными величинами (по х)
                    //
                    case 'c':
                        {
                            if (!tree1.IsEmpty())
                                tree1 = new CartesianTree<int>();
                            if (!tree2.IsEmpty())
                                tree2 = new CartesianTree<int>();
                            int elemNum = 0;
                            do
                            {
                                Console.Write("Сколько элементов будет в первом дереве ");
                            } while (!Int32.TryParse(Console.ReadLine(), out elemNum));
                            Random random = new Random();
                            for (int i = 0; i < elemNum; i++)
                                tree1.Add(random.Next(0, 100), random.Next(0, 10000));

                            do
                            {
                                Console.Write("Сколько элементов будет во втором дереве ");
                            } while (!Int32.TryParse(Console.ReadLine(), out elemNum));
                            for (int i = 0; i < elemNum; i++)
                                tree2.Add(random.Next(0, 100), random.Next(0, 10000));

                            Console.WriteLine("Деревья сформированы. Нажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                }
            } while (goOut == false);
        }
    }
}
