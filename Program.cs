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
            CartesianTree<int> cartesianTree = new CartesianTree<int>();
            cartesianTree = cartesianTree.Add(5 , 6);
            cartesianTree = cartesianTree.Add(4 , 6);
            cartesianTree = cartesianTree.Add(3 , 7);
            cartesianTree = cartesianTree.Add(2 , 1);
            cartesianTree = cartesianTree.Add(10 , 10);
            cartesianTree = cartesianTree.Add(1 , 4);
        }
    }
}
