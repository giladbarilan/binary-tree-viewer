using System;
using Microsoft.Win32;

namespace BinaryViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTree<string> tree = new BinaryTree<string>("1");
            tree.SetLeft(new BinaryTree<string>("2"));
            tree.SetRight(new BinaryTree<string>("3"));

            BinaryTreeViewer.View(tree);
        }
    }
}
