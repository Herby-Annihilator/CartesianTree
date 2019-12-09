using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgLab8
{    
    /// <summary>
    /// Декартово дерево
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class CartesianTree<T>
    {
        /// <summary>
        /// Правое поддерево
        /// </summary>
        public CartesianTree<T> RightSubTree { get; set; }
        /// <summary>
        /// Левое поддерево
        /// </summary>
        public CartesianTree<T> LeftSubTree { get; set; }
        /// <summary>
        /// Ключ
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Приоритет
        /// </summary>
        public int Y { get; set; }

        public CartesianTree()
        {
            X = 0;
            Y = 0;
            RightSubTree = null;
            LeftSubTree = null;
        }

        public CartesianTree(int x, int y) : this()
        {
            X = x;
            Y = y;
        }
        public CartesianTree(CartesianTree<T> left, CartesianTree<T> right, int x, int y)
        {
            LeftSubTree = left;
            RightSubTree = right;
            X = x;
            Y = y;
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
        /// <summary>
        /// Сливает два заданных дерева в одно
        /// Ключи в левом дереве должны быть меньше, чем ключи в правом дереве
        /// </summary>
        /// <param name="leftTree">первое дерево</param>
        /// <param name="rightTree">второе дерево</param>
        /// <returns></returns>
        public static CartesianTree<T> Merge(CartesianTree<T> leftTree, CartesianTree<T> rightTree)
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
        public void Split(int key, CartesianTree<T> newLeft, CartesianTree<T> newRight)
        {
            CartesianTree<T> tree = null;
            if (this.X <= key)
            {
                if (this.RightSubTree == null)
                    newRight = null;
                else
                    this.RightSubTree.Split(key, tree, newRight);
                newLeft.LeftSubTree = this.LeftSubTree;
                newLeft.RightSubTree = tree;
                newLeft.X = this.X;
                newLeft.Y = this.Y;
            }
            else
            {
                if (this.LeftSubTree == null)
                    newLeft = null;
                else
                    this.LeftSubTree.Split(key, newLeft, tree);
                newRight.RightSubTree = this.RightSubTree;
                newRight.LeftSubTree = tree;
                newRight.X = this.X;
                newRight.Y = this.Y;

            }
        }
    }
}
