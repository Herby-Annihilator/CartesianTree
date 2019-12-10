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

        public void DeleteElement(int x)
        {
            if (Root == null)
                return;
            else
                Root = Root.Delete(x);
        }

        public static int RestoreFromFile(string fileName, out CartesianTree<T> tree1, out CartesianTree<T> tree2)
        {
            tree1 = new CartesianTree<T>();
            tree2 = new CartesianTree<T>();
            try
            {
                return Node<T>.RestoreFromFile(tree1.Root, tree2.Root, fileName);
            }
            catch(InvalidDataException e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return -1;
            }
        }
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

        public bool IsEmpty()
        {
            if (Root == null)
                return true;
            return false;
        }
    }
}
