
namespace BinaryTreeViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BinaryTree<string> tree = new BinaryTree<string>("1");
            tree.SetRight(new BinaryTree<string>("3"));
            tree.GetRight().SetRight(new BinaryTree<string>("3"));
            tree.GetRight().SetLeft(new BinaryTree<string>("3"));
            tree.GetRight().GetLeft().SetLeft(new BinaryTree<string>("3"));

            BTViewer.View(tree);
        }
    }
}
