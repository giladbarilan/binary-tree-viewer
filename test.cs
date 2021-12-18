using System.Diagnostics;

namespace BinaryTreeViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTree<int> bintree = new BinaryTree<int>(1);
            bintree.SetRightNode(new BinaryTree<int>(2));
            bintree.GetRightNode().SetLeftNode(new BinaryTree<int>(3));
            bintree.GetRightNode().GetLeftNode().SetLeftNode(new BinaryTree<int>(4));
            bintree.SetLeftNode(new BinaryTree<int>(5));

            BTViewer.View(bintree);
        }
    }
}
