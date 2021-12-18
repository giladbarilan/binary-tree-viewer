
namespace BinaryTreeViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTree<int> tree = new BinaryTree<int>(1);
            tree.SetLeftNode(new BinaryTree<int>(2));
            tree.SetRightNode(new BinaryTree<int>(3));

            BTViewer.View(tree);
        }
    }
}
