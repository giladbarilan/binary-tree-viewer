using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryViewer
{
    /// <summary>
    /// Represents a Binary Tree class used for the BinaryTreeViewer.
    /// </summary>
    /// <typeparam name="T">The tree node's type.</typeparam>
    public partial class BinaryTree<T>
    {
        public BinaryTree<T>? rightNode { get; private set; } // right node of the binary tree.
        public BinaryTree<T>? leftNode { get; private set; } // left node of the binary tree.
        private BinaryTree<T>? father; // the father of the node;
        public T value { get; set; } // the value of the current node.

        public BinaryTree(T value)
        {
            this.value = value;
            this.leftNode = null;
            this.rightNode = null;
            this.father = null;
        }

        public BinaryTree(T value, BinaryTree<T>? left, BinaryTree<T>? right) : this(value)
        {
            this.rightNode = right;
            this.leftNode = left;
            this.father = null;
        }

        public void SetLeft(BinaryTree<T> node)
        {
            this.leftNode = node;
            this.leftNode.father = this;
        }

        public void SetRight(BinaryTree<T> node)
        {
            this.rightNode = node;
            this.rightNode.father = this;
        }

        internal BinaryTree<T>? GetParent() => this.father;

        public BinaryTree<T>? GetRight() => this.rightNode;
        public BinaryTree<T>? GetLeft() => this.leftNode;

        public override string ToString() => this.value.ToString();
    }
}
