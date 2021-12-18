using System.Diagnostics;

namespace BinaryTreeViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTree<int> t = new BinaryTree<int>(1);
            t.SetRightNode(new BinaryTree<int>(2));
            t.SetLeftNode(new BinaryTree<int>(3));

            BTViewer.View(t);
        }
    }
}
