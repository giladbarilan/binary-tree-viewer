# Binary Tree Viewer

BinaryTreeViewer is a C# library that draws binary trees on runtime into HTML files.

## Usage

To use the BinaryTreeViewer, you need to use the BinaryTreeViewer.BinaryTree class as your Binary Tree structure.

For example:
```csharp
BinaryTree<int> tree = new BinaryTree<int>(1);
tree.SetLeft(new BinaryTree<int>(5));
```

To show the tree -> pass the head of the tree as an argument to BinaryTreeViewer.BTViewer as the following:
```csharp
BTViewer.View(tree);
```
