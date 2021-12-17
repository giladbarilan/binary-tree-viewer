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

## How is Data Saved?

The binary-tree HTML files are saved in chronological order by using a counter.
The file name structure for the trees is "BINTREE<SomeNumber>.html".

### How to delete the data?

If you want to delete the temporary binary trees -> use the function ClearTrees inside BTViewer as the following:

```csharp
BTViewer.ClearTrees(TreesToClear treesToClear);
```

#### There are three options to delete:
Delete the files that were created on the current run -> treesToClear.CurrentRun
Delete the files that were created on previous runs -> treesToClear.PreviousRuns
Delete all of the BINTREE temporary files -> (treesToClear.PreviousRuns | treesToClear.CurrentRun)
