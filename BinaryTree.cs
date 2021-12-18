﻿
namespace BinaryTreeViewer
{
    /// <summary>
    /// Represents a Binary Tree class used for the BinaryTreeViewer.
    /// </summary>
    /// <typeparam name="T">The tree node's type.</typeparam>
    public partial class BinaryTree<T>
    {
        private BinaryTree<T>? rightNode; // right node of the binary tree.
        private BinaryTree<T>? leftNode; // left node of the binary tree.
        private BinaryTree<T>? father; // the father of the node.
        public T value { get; set; } // the value of the current node.

        public BinaryTree(T value)
        {
            this.value = value;
            this.rightNode = null;
            this.leftNode = null;
            this.father = null;
        }

        public BinaryTree(T value, BinaryTree<T>? left, BinaryTree<T>? right) : this(value)
        {
            this.rightNode = right;
            this.leftNode = left;
            this.father = null;
        }

        public void SetLeftNode(BinaryTree<T> node)
        {
            this.leftNode = node;
            this.leftNode.father = this;
        }

        public void SetRightNode(BinaryTree<T> node)
        {
            this.rightNode = node;
            this.rightNode.father = this;
        }

        internal BinaryTree<T>? GetParent() => this.father;

        public BinaryTree<T>? GetRightNode() => this.rightNode;
        public BinaryTree<T>? GetLeftNode() => this.leftNode;

        public override string? ToString() => this.value?.ToString();
    }
}
