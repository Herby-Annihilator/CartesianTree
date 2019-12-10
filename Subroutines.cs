using System;
using System.Collections.Generic;
using System.IO;

namespace AlgLab8
{
    class Subroutines
    {
        /// <summary>
        /// Печатает меню 
        /// </summary>
        /// <returns></returns>
        public static char PrintMenu()
        {
            char symbol;
            do
            {
                Console.Clear();
                Console.WriteLine("* c - создать деревья и заполнить их случайными величинами (по х)");
                Console.WriteLine("* b - восстановить деревья из файла input.dat");
                Console.WriteLine("* d - удалить узлы, принадлежащие заданному отрезку");
                Console.WriteLine("* p - показать дерево (сделать обход, без связей)");
                Console.WriteLine("* r - показать таблицу связей дерева");
                Console.WriteLine("* h - Получить высоту дерева");
                Console.WriteLine("* v - получить информацию о корне");
                Console.WriteLine("* ESC - выход");
                Console.Write("Ваш выбор - ");
                symbol = Convert.ToChar(Console.ReadKey(true).KeyChar);
            } while (symbol != 'c' && symbol != 'b' && symbol != 'p' && symbol != 'r' && symbol != 'h' && symbol != 'v' && symbol != 27 && symbol != 'd');
            return symbol;
        }
        //
        // Сохранить в input.dat (нерекурсивный алгоритм обхода дерева по принципу лево-корень-право)
        //
        /// <summary>
        /// Сохранит дерево в одну строку в указанный файл (добавление)
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="fileName"></param>
        public static void SaveTreeInFile(CartesianTree<int> tree, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName, true);
            Stack<Node<int>> stack = new Stack<Node<int>>();
            Node<int> currentNode = tree.Root;
            writer.Write("\n$;");
            while (!(currentNode == null && stack.Count == 0))
            {
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftSubTree;
                }
                else
                {
                    currentNode = stack.Pop();
                    writer.Write(currentNode.X + " " + currentNode.Y + ";");
                    currentNode = currentNode.RightSubTree;
                }
            }
            writer.Write("$\n");
            writer.Close();
        }
        public static void AddLinksTableToFile(CartesianTree<int> tree, string fileName)
        {
            if (tree.Root == null)
                return;
            StreamWriter writer = new StreamWriter(fileName, true);
            if (writer == null)
                throw new FileNotFoundException("Файл " + fileName + " не найден");
            Stack<Node<int>> stack = new Stack<Node<int>>();
            Node<int> currentNode = tree.Root;
            writer.WriteLine("\n");
            writer.WriteLine("==========Корень данного дерева==========\n");
            if (tree.Root == null)
            {
                Console.WriteLine("NULL");
                return;
            }
            else
            {
                Console.WriteLine("Поле data = " + tree.Root.Data);
                Console.WriteLine("Ключ Х = " + tree.Root.X);
                Console.WriteLine("Приоритет = " + tree.Root.Y);
                Console.Write("Потомок слева = ");
                if (tree.Root.LeftSubTree != null)
                    Console.WriteLine("ключ Х = " + tree.Root.LeftSubTree.X + "приоритет Y = " + tree.Root.LeftSubTree.Y);
                else
                    Console.WriteLine("не существует");
                Console.Write("Потомок справа = ");
                if (tree.Root.RightSubTree != null)
                    Console.WriteLine("ключ Х = " + tree.Root.RightSubTree.X + "приоритет Y = " + tree.Root.RightSubTree.Y + "\n\n");
                else
                    Console.WriteLine("не существует\n\n");
            }
            writer.WriteLine("\n");
            writer.WriteLine("================Таблица ссылок в данном экземпляре Декартова дерева==================\n");
            writer.WriteLine("| Ключ + (приоритет)|  Левый потомок|  Правый потомок|\n");
            while (!(currentNode == null && stack.Count == 0))
            {
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftSubTree;
                }
                else
                {
                    currentNode = stack.Pop();
                    writer.Write("  " + currentNode.X + " \t" + "(" + currentNode.Y + ")" + " \t\t");
                    if (currentNode.LeftSubTree != null)
                        writer.Write("  " + currentNode.LeftSubTree.X + " \t" + "(" + currentNode.LeftSubTree.Y + ")" + " \t\t");
                    else
                        writer.Write("\t нет\t\t");
                    if (currentNode.RightSubTree != null)
                        writer.Write("  " + currentNode.RightSubTree.X + " \t" + "(" + currentNode.RightSubTree.Y + ")" + " \n\n" + writer.NewLine);
                    else
                        writer.Write("\t нет\n\n" + writer.NewLine);
                    currentNode = currentNode.RightSubTree;
                }
            }
            writer.Close();
        }
        //
        // Переписать из файла в файл
        //
        /// <summary>
        /// Переписывает содержимое одного файла в другой
        /// </summary>
        /// <param name="fileNameFrom">откуда переписывать</param>
        /// <param name="fileNameTo">куда переписывать</param>
        public static void WriteFromFileToFile(string fileNameFrom, string fileNameTo)
        {
            if (fileNameTo == fileNameFrom)
            {
                throw new Exception("Невозможно переписать из файла в тот же файл");
            }
            StreamReader reader = new StreamReader(fileNameFrom);
            if (reader == null)
            {
                throw new Exception("Ошибка открытия файла для чтения");
            }
            StreamWriter writer = new StreamWriter(fileNameTo);
            if (writer == null)
            {
                throw new Exception("Ошибка открытия файла для записи");
            }
            string toRewrite;
            while ((toRewrite = reader.ReadLine()) != null)
            {
                writer.WriteLine(toRewrite);
            }
            writer.Close();
            reader.Close();
        }
        //
        // Дополнить содержание одного файла содержаением другого
        //
        /// <summary>
        /// Из первого файла дописывает данные во второй файл
        /// </summary>
        /// <param name="fileNameFrom">откуда переписывать</param>
        /// <param name="fileNameTo">куда переписывать</param>
        public static void AddFromFileToFile(string fileNameFrom, string fileNameTo)
        {
            if (fileNameTo == fileNameFrom)
            {
                throw new Exception("Невозможно переписать из файла в тот же файл");
            }
            StreamReader reader = new StreamReader(fileNameFrom);
            StreamWriter writer = new StreamWriter(fileNameTo, true);
            string toRewrite;
            while ((toRewrite = reader.ReadLine()) != null)
            {
                writer.WriteLine(toRewrite);
            }
            writer.Close();
            reader.Close();
        }
        //
        // Получить целое число
        //
        public static int GetInt()
        {
            int number;
            string strNum;
            do
            {
                strNum = Console.ReadLine();
            } while (Int32.TryParse(strNum, out number) == false);
            return number;
        }
        /// <summary>
        /// Сравнивает указанные строки-следы. Вернет 0, если эти следы эдентичны,
        /// -1, если искомый след находится в левом поддереве, 1, если искомый
        /// след находится в правом поддереве.
        /// </summary>
        /// <param name="currentTrace">текущий след</param>
        /// <param name="desiredTrace">след, который надо найти</param>
        /// <returns></returns>
        public static int CompareTraces(string currentTrace, string desiredTrace)
        {
            if (currentTrace == null || desiredTrace == null)
                throw new NullReferenceException("Несуществующая строка!");
            else if (currentTrace.Length == 0 || desiredTrace.Length == 0)
                throw new FormatException("Длина одной из строк == 0");
            if (currentTrace.Length > desiredTrace.Length)
                throw new Exception("Текущая строка не может быть длиннее заданной строки");

            int isEquals = 0;
            if (currentTrace == desiredTrace)
                isEquals = 0;              // данные следы идентичны
            else
            {
                if (desiredTrace.Substring(0, currentTrace.Length) == currentTrace)
                {
                    if (desiredTrace[currentTrace.Length] == '1')
                        isEquals = 1;                     // идем вправо
                    else if (desiredTrace[currentTrace.Length] == '0')
                        isEquals = -1;               // идем влево
                }
                else
                    return -2;     // значит такого следа нет вообще
            }
            return isEquals;
        }
        /// <summary>
        /// Указывет, является ли данная строка следом
        /// </summary>
        /// <param name="trace"></param>
        /// <returns></returns>
        public static bool IsTrace(string trace)
        {
            if (trace == null || trace.Length == 0)
                return false;
            if (trace[0] != '1')
                return false;
            int lenght = trace.Length;
            for (int i = 1; i < lenght; i++)
                if (trace[i] < '0' || trace[i] > '1')
                    return false;
            return true;
        }

        
    }
}
