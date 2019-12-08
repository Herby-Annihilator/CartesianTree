using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab8
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Node<T>
    {
        /// <summary>
        /// Ключ
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Левый потомок
        /// </summary>
        public Node<T> LeftChild { get; set; }
        /// <summary>
        /// Правый потомок
        /// </summary>
        public Node<T> RightChild { get; set; }
        public T Data { get; set; }

        public Node()
        {
            X = 0;
            Y = 0;
            LeftChild = null;
            RightChild = null;
            Data = default(T);
        }
        public Node(int x, int y, T data) : this()
        {
            X = x;
            Y = y;
            Data = data;
        }
        public Node(Node<T> left, Node<T> right, int x, int y, T data) : this(x, y, data)
        {
            LeftChild = left;
            RightChild = right;
        }
    }
    /// <summary>
    /// Декартово дерево
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CartesianTree<T>
    {
        /// <summary>
        /// Корень дерева
        /// </summary>
        public Node<T> Root { get; protected set; }
        /// <summary>
        /// Высота дерева
        /// </summary>
        public int Height { get; protected set; }

        public CartesianTree()
        {
            Height = 0;
            Root = null;
        }

        public CartesianTree(Node<T> root)
        {
            Root = root;
            Height = 1;
        }

        public bool Add(int x, int y, T data)
        {
            //
            return true;
        }
        public bool Add(int x, int y)
        {
            T data = default(T);
            //
            return true;
        }
    }
}
