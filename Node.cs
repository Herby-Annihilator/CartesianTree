using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgLab8
{    
    /// <summary>
    /// Декартово дерево
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Node<T>
    {
        /// <summary>
        /// Правое поддерево
        /// </summary>
        public Node<T> RightSubTree { get; set; }
        /// <summary>
        /// Левое поддерево
        /// </summary>
        public Node<T> LeftSubTree { get; set; }
        /// <summary>
        /// Ключ
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Любые ваши данные
        /// </summary>
        public T Data { get; set; }

        public Node()
        {
            Data = default(T);
            X = 0;
            Y = 0;
            RightSubTree = null;
            LeftSubTree = null;
        }

        public Node(int x, int y, T data) : this()
        {
            Data = data;
            X = x;
            Y = y;
        }
        public Node(Node<T> left, Node<T> right, int x, int y, T data)
        {
            LeftSubTree = left;
            RightSubTree = right;
            X = x;
            Y = y;
            Data = data;
        }
        /// <summary>
        /// Добавляет элемент в декартово дерево
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public Node<T> Add(int x, int y, T data)
        {
            Node<T> left = new Node<T>();
            Node<T> right = new Node<T>();
            Node<T> middle = new Node<T>(x, y, data);
            Split(x, out left, out right);
            return Merge(Merge(left, middle), right);
        }
        /// <summary>
        /// Добавляет элемент в декартово дерево
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Node<T> Add(int x, int y)
        {
            T data = default(T);
            Node<T> left = new Node<T>();
            Node<T> right = new Node<T>();
            Node<T> middle = new Node<T>(x, y, data);
            Split(x, out left, out right);
            return Merge(Merge(left, middle), right);
        }
        /// <summary>
        /// Сливает два заданных дерева в одно
        /// Ключи в левом дереве должны быть меньше, чем ключи в правом дереве
        /// </summary>
        /// <param name="leftTree">первое дерево</param>
        /// <param name="rightTree">второе дерево</param>
        /// <returns></returns>
        public static Node<T> Merge(Node<T> leftTree, Node<T> rightTree)
        {
            if (leftTree == null)
                return rightTree;
            if (rightTree == null)
                return leftTree;
            if (leftTree.Y > rightTree.Y)
            {
                rightTree.LeftSubTree = Merge(rightTree.LeftSubTree, leftTree);
                return rightTree;
            }
            else
            {
                leftTree.RightSubTree = Merge(leftTree.RightSubTree, rightTree);
                return leftTree;
            }
        }
        /// <summary>
        /// Разделит текущее дерево на два новых по заданному ключу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="newLeft">Новое левое дерево (в нем ключи меньше заданного)</param>
        /// <param name="newRight">Новое правое дерево (в нем ключи больше заданного)</param>
        public void Split(int key, out Node<T> newLeft, out Node<T> newRight)
        {
            Node<T> tree = null;
            if (this.X <= key)
            {
                if (this.RightSubTree == null)
                    newRight = null;
                else
                    this.RightSubTree.Split(key, out tree, out newRight);
                newLeft = new Node<T>(LeftSubTree, tree, X, Y, Data);
                //newLeft.LeftSubTree = this.LeftSubTree;
                //newLeft.RightSubTree = tree;
                //newLeft.X = this.X;
                //newLeft.Y = this.Y;
            }
            else
            {
                if (this.LeftSubTree == null)
                    newLeft = null;
                else
                    this.LeftSubTree.Split(key, out newLeft, out tree);
                newRight = new Node<T>(tree, RightSubTree, X, Y, Data);
                //newRight.RightSubTree = this.RightSubTree;
                //newRight.LeftSubTree = tree;
                //newRight.X = this.X;
                //newRight.Y = this.Y;

            }
        }
        /// <summary>
        /// Удалит элемент с заданным ключом
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public Node<T> Delete(int x)
        {
            Node<T> left = new Node<T>();
            Node<T> right = new Node<T>();
            Node<T> middle = new Node<T>();   // то дерево, которое будет удалено

            Split(x - 1, out left, out right);
            right.Split(x, out middle, out right);
            return Merge(left, right);
        }
        public int FindMaxX()
        {
            if (RightSubTree == null)
                return this.X;
            Node<T> current = this.RightSubTree;
            while (current.RightSubTree != null)
                current = current.RightSubTree;
            return current.X;
        }
        /// <summary>
        /// Восстанавливает деревья из файла
        /// </summary>
        /// <param name="tree1"></param>
        /// <param name="tree2"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static int RestoreFromFile(out Node<T> tree1, out Node<T> tree2, string fileName)
        {
            StreamReader reader = new StreamReader(fileName);
            tree1 = null;
            tree2 = null;
            string first;
            int numberOfTrees = 0;          // 0 - если не получилось деревьев, 1 - если получилось одно деревео, 2 - если получилось 2 дерева
            while ((first = reader.ReadLine()) != null)
            {
                if (String.IsNullOrEmpty(first))
                    continue;
                if (first[0] == '$' && first[first.Length - 1] == '$')
                {
                    string[] pairs = first.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);  // разбил на пары х,у
                    if (numberOfTrees == 0)
                    {
                        for (int i = 1; i < pairs.Length - 1; i++)  // нужно исключить элементы [0] и [lenght - 1], т.к. в них заведомо некорректные данные
                        {
                            string[] XY = pairs[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            int x, y;
                            if (!Int32.TryParse(XY[0], out x))
                                throw new InvalidDataException("Неверный формат строки дерева");
                            if (!Int32.TryParse(XY[1], out y))
                                throw new InvalidDataException("Неверный формат строки дерева");
                            if (tree1 == null)
                                tree1 = new Node<T>(x, y, default(T));
                            tree1 = tree1.Add(x, y);
                        }
                        numberOfTrees++;
                    }
                    else
                    {
                        for (int i = 1; i < pairs.Length - 1; i++)  // нужно исключить элементы [0] и [lenght - 1], т.к. в них заведомо некорректные данные
                        {
                            string[] XY = pairs[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            int x, y;
                            if (!Int32.TryParse(XY[0], out x))
                                throw new InvalidDataException("Неверный формат строки дерева");
                            if (!Int32.TryParse(XY[1], out y))
                                throw new InvalidDataException("Неверный формат строки дерева");
                            if (tree2 == null)
                                tree2 = new Node<T>(x, y, default(T));
                            tree2 = tree2.Add(x, y);
                        }
                        numberOfTrees++;
                        break;
                    }
                }
            }
            reader.Close();
            return numberOfTrees;
        }
    }
}
