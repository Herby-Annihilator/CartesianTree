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
                Console.WriteLine("* c - создать дерево и заполнить его случайными величинами");
                Console.WriteLine("* b - добавить элементы в дерево вручную");
                Console.WriteLine("* d - удалить узелы, принадлежащие заданному отрезку");
                Console.WriteLine("* p - показать дерево (сделать обход, без связей)");
                Console.WriteLine("* r - показать таблицу связей дерева");
                Console.WriteLine("* h - Получить высоту дерева");
                Console.WriteLine("* v - получить информацию о корне");
                Console.WriteLine("* n - установить правильные следы в дереве");
                Console.WriteLine("* ESC - выход");
                Console.Write("Ваш выбор - ");
                symbol = Convert.ToChar(Console.ReadKey(true).KeyChar);
            } while (symbol != 'c' && symbol != 'b' && symbol != 'p' && symbol != 'r' && symbol != 'h' && symbol != 'v' && symbol != 27 && symbol != 'd' && symbol != 'n');
            return symbol;
        }
        //
        // Сохранить в input.dat (нерекурсивный алгоритм обхода дерева по принципу лево-корень-право)
        //
        /// <summary>
        /// Сохранит дерево в одну строку в указанный файл
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="fileName"></param>
        //public static void SaveTreeInFile(AVLTree<int> tree, string fileName)
        //{
        //    StreamWriter writer = new StreamWriter(fileName);
        //    tree.PutRightBalance(tree.Root);
        //    Stack<AVLTree<int>.Node<int>> stack = new Stack<AVLTree<int>.Node<int>>();
        //    AVLTree<int>.Node<int> currentNode = tree.Root;
        //    if (currentNode.IsLeaf())
        //        writer.Write(currentNode.Key + " ");
        //    while (!(currentNode == null && stack.Count == 0))
        //    {
        //        if (currentNode != null)
        //        {
        //            stack.Push(currentNode);
        //            currentNode = currentNode.LeftChild;
        //        }
        //        else
        //        {
        //            currentNode = stack.Pop();
        //            writer.Write(currentNode.Key + " // ");
        //            currentNode = currentNode.RightChild;
        //        }
        //    }
        //    writer.Close();
        //}
        //public static void AddLinksTableToFile(AVLTree<int> tree, string fileName)
        //{
        //    if (tree.Root == null)
        //        return;
        //    StreamWriter writer = new StreamWriter(fileName, true);
        //    if (writer == null)
        //        throw new FileNotFoundException("Файл " + fileName + " не найден");
        //    Stack<AVLTree<int>.Node<int>> stack = new Stack<AVLTree<int>.Node<int>>();
        //    AVLTree<int>.Node<int> currentNode = tree.Root;
        //    writer.WriteLine("\n");
        //    writer.WriteLine("==========Корень данного дерева==========\n");
        //    writer.WriteLine("Значение ключа = " + tree.Root.Key + " Данные = " + tree.Root.Data);
        //    if (tree.Root.LeftChild.IsExist())
        //        writer.WriteLine("Потомок слева существует: ключ = " + tree.Root.LeftChild.Key + " Данные = " + tree.Root.LeftChild.Data);
        //    else
        //        writer.WriteLine("Потомков слева нет");
        //    if (tree.Root.RightChild.IsExist())
        //        writer.WriteLine("Потомок справа существует: ключ = " + tree.Root.RightChild.Key + " Данные = " + tree.Root.RightChild.Data);
        //    else
        //        writer.WriteLine("Потомков справа нет");
        //    writer.WriteLine("\n");
        //    writer.WriteLine("================Таблица ссылок в данном экземпляре авл-дерева==================\n");
        //    writer.WriteLine("| Значение в узле + след|  Левый потомок|  Правый потомок|\n");
        //    while (!(currentNode == null && stack.Count == 0))
        //    {
        //        if (currentNode != null)
        //        {
        //            stack.Push(currentNode);
        //            currentNode = currentNode.LeftChild;
        //        }
        //        else
        //        {
        //            currentNode = stack.Pop();
        //            writer.Write("  " + currentNode.Key + " \t" + "\"" + currentNode.Trace + "\"" + " \t\t");
        //            if (currentNode.LeftChild != null)
        //                writer.Write("  " + currentNode.LeftChild.Key + " \t" + "\"" + currentNode.LeftChild.Trace + "\"" + " \t\t");
        //            else
        //                writer.Write("\t нет\t\t");
        //            if (currentNode.RightChild != null)
        //                writer.Write("  " + currentNode.RightChild.Key + " \t" + "\"" + currentNode.RightChild.Trace + "\"" + " \n\n" + writer.NewLine);
        //            else
        //                writer.Write("\t нет\n\n" + writer.NewLine);
        //            currentNode = currentNode.RightChild;
        //        }
        //    }
        //    writer.Close();
        //}
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
