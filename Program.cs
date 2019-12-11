using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

                            StreamWriter writer = new StreamWriter("input.dat");
                            writer.WriteLine("//==========Дерево 1============//\n");
                            writer.Close();

                            Subroutines.SaveTreeInFile(tree1, "input.dat");

                            writer = new StreamWriter("input.dat", true);
                            writer.WriteLine(writer.NewLine  + "\r\n//==========Дерево 2============//\n");
                            writer.Close();

                            Subroutines.SaveTreeInFile(tree2, "input.dat");
                            Console.WriteLine("Деревья сформированы. Нажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // b - восстановить деревья из файла input.dat
                    //
                    case 'b':
                        {
                            if (!tree1.IsEmpty())
                                tree1 = new CartesianTree<int>();
                            if (!tree2.IsEmpty())
                                tree2 = new CartesianTree<int>();
                            CartesianTree<int>.RestoreFromFile("input.dat", out tree1, out tree2);
                            Console.WriteLine("\nНажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // d - удалить узлы, принадлежащие заданному отрезку
                    //
                    case 'd':
                        {
                            int x, y;
                            do
                            {
                                do
                                {
                                    Console.Write("Задайте первую границу отрезка ");
                                } while (!Int32.TryParse(Console.ReadLine(), out x));

                                do
                                {
                                    Console.Write("Задайте второую границу отрезка ");
                                } while (!Int32.TryParse(Console.ReadLine(), out y));
                            } while (x > y);
                            if (tree1.FindMax() < tree2.FindMax())
                            {
                                StreamWriter writer = new StreamWriter("output.dat");
                                writer.WriteLine("##############################################");
                                writer.WriteLine("               Было     (Дерево 2)");
                                writer.WriteLine("##############################################");
                                writer.Close();

                                Subroutines.AddLinksTableToFile(tree2, "output.dat");
                                tree2.DeleteFromSegment(x, y);

                                writer = new StreamWriter("output.dat", true);
                                writer.WriteLine("\n\n##############################################");
                                writer.WriteLine("               Стало    (Дерево 2)");
                                writer.WriteLine("##############################################");
                                writer.Close();
                                Subroutines.AddLinksTableToFile(tree2, "output.dat");
                            }
                            else
                            {
                                StreamWriter writer = new StreamWriter("output.dat");
                                writer.WriteLine("##############################################");
                                writer.WriteLine("               Было     (Дерево 1)");
                                writer.WriteLine("##############################################");
                                writer.Close();

                                Subroutines.AddLinksTableToFile(tree1, "output.dat");
                                tree1.DeleteFromSegment(x, y);

                                writer = new StreamWriter("output.dat", true);
                                writer.WriteLine("\n\n##############################################");
                                writer.WriteLine("               Стало    (Дерево 1)");
                                writer.WriteLine("##############################################");
                                writer.Close();
                                Subroutines.AddLinksTableToFile(tree1, "output.dat");
                            }
                            Console.WriteLine("\nНажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // p - показать дерево (сделать обход, без связей)
                    //
                    case 'p':
                        {
                            int whatTree;
                            do
                            {
                                do
                                {
                                    Console.Write("\nКакое дерево показать? (1) или (2) ");
                                } while (!Int32.TryParse(Console.ReadLine(), out whatTree));
                            } while (whatTree < 1 || whatTree > 2);
                            if (whatTree == 1)
                            {
                                Console.WriteLine("\n\n==========Дерево 1============");
                                Console.WriteLine("\nКорень\n");
                                tree1.GetRootInfo();
                                Console.WriteLine("Высота = " + tree1.Height);
                                Console.WriteLine("MAX = " + tree1.FindMax());
                            }
                            else
                            {
                                Console.WriteLine("\n\n==========Дерево 2============");
                                Console.WriteLine("\nКорень\n");
                                tree2.GetRootInfo();
                                Console.WriteLine("Высота = " + tree2.Height);
                                Console.WriteLine("MAX = " + tree2.FindMax());
                            }
                            Console.WriteLine("\nНажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // r - показать таблицу связей дерева
                    //
                    case 'r':
                        {
                            int whatTree;
                            do
                            {
                                do
                                {
                                    Console.Write("\nКакое дерево показать? (1) или (2) ");
                                } while (!Int32.TryParse(Console.ReadLine(), out whatTree));
                            } while (whatTree < 1 || whatTree > 2);
                            if (whatTree == 1)
                            {
                                Console.WriteLine("\n\n==========Дерево 1============");
                                if (tree1.ShowTreeLinks() == false)
                                {
                                    Console.WriteLine("\nПроблемы! Нажмите что-нибудь");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\n\n==========Дерево 2============");
                                if (tree2.ShowTreeLinks() == false)
                                {
                                    Console.WriteLine("\nПроблемы! Нажмите что-нибудь");
                                    Console.ReadKey();
                                    break;
                                }
                            }
                            Console.WriteLine("\nНажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // h - Получить высоту дерева
                    //
                    case 'h':
                        {
                            int whatTree;
                            do
                            {
                                do
                                {
                                    Console.Write("\nКакое дерево показать? (1) или (2) ");
                                } while (!Int32.TryParse(Console.ReadLine(), out whatTree));
                            } while (whatTree < 1 || whatTree > 2);
                            if (whatTree == 1)
                            {
                                Console.WriteLine("\n\n==========Дерево 1============");
                                Console.WriteLine("\nВысота = " + tree1.Height + "\n");
                            }
                            else
                            {
                                Console.WriteLine("\n\n==========Дерево 2============");
                                Console.WriteLine("\nВысота = " + tree2.Height + "\n");
                            }
                            Console.WriteLine("\nНажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // v - получить информацию о корне
                    //
                    case 'v':
                        {
                            int whatTree;
                            do
                            {
                                do
                                {
                                    Console.Write("\nКакое дерево показать? (1) или (2) ");
                                } while (!Int32.TryParse(Console.ReadLine(), out whatTree));
                            } while (whatTree < 1 || whatTree > 2);
                            if (whatTree == 1)
                            {
                                Console.WriteLine("\n\n==========Дерево 1============");
                                Console.WriteLine("\nКорень\n");
                                tree1.GetRootInfo();
                            }
                            else
                            {
                                Console.WriteLine("\n\n==========Дерево 2============");
                                Console.WriteLine("\nКорень\n");
                                tree2.GetRootInfo();
                            }
                            Console.WriteLine("\nНажмите что-нибудь");
                            Console.ReadKey();
                            break;
                        }
                    //
                    // ESC - выход
                    //
                    case (char)27:
                        {
                            goOut = true;
                            break;
                        }
                }
            } while (goOut == false);
        }
    }
}
