using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AlgLab8
{
    class CartesianTree<T>
    {
        public Node<T> Root { get; private set; }
        private int height;
        public int Height
        {
            get
            {
                return GetHeight(Root);
            }
            private set
            {
                height = value;
            }
        }

        public CartesianTree()
        {
            Root = null;
        }
        public CartesianTree(Node<T> root)
        {
            Root = root;
        }
        /// <summary>
        /// Добавит элемент в декартово дерево
        /// </summary>
        /// <param name="x">ключ</param>
        /// <param name="y">приоритет</param>
        /// <param name="data"></param>
        public void Add(int x, int y, T data = default(T))
        {
            if (Root == null)
            {
                Root = new Node<T>(x, y, data);
                return;
            }
            Root = Root.Add(x, y, data);
            return;
        }
        /// <summary>
        /// Удалит элемент из декартова дерева
        /// </summary>
        /// <param name="x">ключ элемента</param>
        public void DeleteElement(int x)
        {
            if (Root == null)
                return;
            else
                Root = Root.Delete(x);
        }
        /// <summary>
        /// Восстанавливает из файла два дерева
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="tree1"></param>
        /// <param name="tree2"></param>
        /// <returns></returns>
        public static int RestoreFromFile(string fileName, out CartesianTree<T> tree1, out CartesianTree<T> tree2)
        {
            tree1 = new CartesianTree<T>();
            tree2 = new CartesianTree<T>();
            try
            {
                Node<T> root1;
                Node<T> root2;
                int a = Node<T>.RestoreFromFile(out root1, out root2, fileName);

                tree1.Root = root1;
                tree2.Root = root2;
                return a;
            }
            catch(InvalidDataException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return -1;
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return -1;
            }
        }
        /// <summary>
        /// Получает выстоту дерева
        /// </summary>
        /// <param name="currentNode"></param>
        /// <returns></returns>
        private int GetHeight(Node<T> currentNode)
        {
            int height = 0;
            int leftHeight = 0;
            int rightHeight = 0;
            if (currentNode != null)
            {
                height = 1;
                if (currentNode.LeftSubTree != null)
                {
                    leftHeight = GetHeight(currentNode.LeftSubTree);
                }
                if (currentNode.RightSubTree != null)
                {
                    rightHeight = GetHeight(currentNode.RightSubTree);
                }
            }
            if (leftHeight > rightHeight)
                return leftHeight + height;
            else
                return rightHeight + height;
        }
        /// <summary>
        /// Проверит дерево на пустоту (true - если пусто)
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            if (Root == null)
                return true;
            return false;
        }
        /// <summary>
        /// Удалит элементы декартова дерева из заданного промежутка
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        public void DeleteFromSegment(int x1, int x2)
        {
            if (Root == null)
                return;
            Root = DeleteFromSegment(Root, ref x1, ref x2);
        }
        /// <summary>
        /// Удалит элементы декартова дерева из заданного промежутка (для использования внутри публичного метода)
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <returns></returns>
        private Node<T> DeleteFromSegment(Node<T> currentNode, ref int x1, ref int x2)
        {
            if (currentNode == null)
                return null;
            while (currentNode.X >= x1 && currentNode.X <= x2)
            {
                currentNode = currentNode.Delete(currentNode.X);
                if (currentNode == null)
                    break;
            }
            if (currentNode != null)
            {
                if (currentNode.X < x1)
                    currentNode.RightSubTree = DeleteFromSegment(currentNode.RightSubTree, ref x1, ref x2);
                else if (currentNode.X > x2)
                    currentNode.LeftSubTree = DeleteFromSegment(currentNode.LeftSubTree, ref x1, ref x2);
            }
            return currentNode;
        }
        /// <summary>
        /// Найдет максимальный элемент в дереве
        /// </summary>
        /// <returns></returns>
        public int FindMax()
        {
            return this.Root.FindMaxX();
        }
        /// <summary>
        /// Сделает обход по принципу лево-право-корень
        /// </summary>
        public void ShowTree()
        {
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> lastVisitedNode = null;
            Node<T> peekNode = null;
            Node<T> currentNode = Root;

            while (stack.Count != 0 || currentNode != null)
            {
                if (currentNode != null)
                {
                    stack.Push(currentNode);
                    currentNode = currentNode.LeftSubTree;
                }
                else
                {
                    peekNode = stack.Peek();
                    // если правый потомок существует и обход пришёл из левого потомка, двигаемся вправо
                    if (peekNode.RightSubTree != null && lastVisitedNode != peekNode.RightSubTree)
                        currentNode = peekNode.RightSubTree;
                    else
                    {
                        // Visit
                        Console.Write("X = " + currentNode.X + " Y = " + currentNode.Y + " ");
                        lastVisitedNode = stack.Pop();
                    }
                }
            }
        }
        /// <summary>
        /// Получает информацию о корне
        /// </summary>
        public void GetRootInfo()
        {
            if (Root == null)
            {
                Console.WriteLine("NULL");
                return;
            }
            else
            {
                Console.WriteLine("Поле data = " + Root.Data);
                Console.WriteLine("Ключ Х = " + Root.X);
                Console.WriteLine("Приоритет = " + Root.Y);
                Console.Write("Потомок слева = ");
                if (Root.LeftSubTree != null)
                    Console.WriteLine("ключ Х = " + Root.LeftSubTree.X + " приоритет Y = " + Root.LeftSubTree.Y);
                else
                    Console.WriteLine(" не существует");
                Console.Write("Потомок справа = ");
                if (Root.RightSubTree != null)
                    Console.WriteLine("ключ Х = " + Root.RightSubTree.X + " приоритет Y = " + Root.RightSubTree.Y + "\n\n");
                else
                    Console.WriteLine(" не существует\n\n");
            }
        }
        /// <summary>
        /// Покажет таблицу ссылок в данном экземпляре дерева
        /// </summary>
        /// <returns></returns>
        public bool ShowTreeLinks()
        {
            if (this.Root == null)
                return false;
            Stack<Node<T>> stack = new Stack<Node<T>>();
            Node<T> currentNode = Root;
            Console.WriteLine("\n");
            GetRootInfo();
            Console.WriteLine("\nВысота = " + Height);
            Console.WriteLine("MAX = " + FindMax());
            Console.WriteLine("\n");
            Console.WriteLine("================Таблица ссылок в данном экземпляре Декартова дерева==================\n");
            Console.WriteLine("| Ключ + (приоритет)|  Левый потомок|  Правый потомок|\n");
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
                    Console.Write("  " + currentNode.X + " \t" + "(" + currentNode.Y + ")" + " \t\t");
                    if (currentNode.LeftSubTree != null)
                        Console.Write("  " + currentNode.LeftSubTree.X + " \t" + "(" + currentNode.LeftSubTree.Y + ")" + " \t\t");
                    else
                        Console.Write("\t нет\t\t");
                    if (currentNode.RightSubTree != null)
                        Console.Write("  " + currentNode.RightSubTree.X + " \t" + "(" + currentNode.RightSubTree.Y + ")" + " \n\n");
                    else
                        Console.Write("\t нет\n\n");
                    currentNode = currentNode.RightSubTree;
                }
            }
            return true;
        }
    }
}
