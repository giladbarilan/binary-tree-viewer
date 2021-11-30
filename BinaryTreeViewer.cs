using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BinaryViewer
{
    /// <summary>
    /// Shows in an HTML document a graph of the tree.
    /// RECOMMENDATION: Use break-point on the line of the BinaryTreeViewer.View .
    /// </summary>
    public static class BinaryTreeViewer
    {
        private static int tempCount = 1; // the number of temporary files we've created.
        private static string fileName => $"BINTREE{tempCount}.html";

        /// <summary>
        /// Sets the value of tempCount according to the previous saved_trees.
        /// </summary>
        static BinaryTreeViewer()
        {
            string directory = Directory.GetCurrentDirectory();
            Regex reg = new Regex(@"BINTREE\d+\.html"); //we check what is the latest binary tree file.

            List<string> fileNames = Directory.GetFiles(directory).ToList();
            fileNames = reg.Matches(string.Join(" ", fileNames)).Select(x => x.Value).ToList(); //Get the BINTREE files on the directory.

            if (fileNames.Count > 0)
            {
                //we find the next fileName as -> the latest file name count (BINTREE*Number*) + 1
                tempCount = fileNames.Select(x => int.Parse(new Regex(@"\d+").Match(x).Value)).Max() + 1; //the next tree to draw.
            }
            else
                tempCount = 1;
        }

        /// <summary>
        /// Writes the full tree into a file by the head.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tree">The starting of the tree.</param>
        public static void View<T>(BinaryTree<T> tree)
        {
            // in case they entered invalid tree.
            if (tree == null)
                return;

            // in case there is only one node on the tree (only the head).
            if(tree.rightNode == null && tree.leftNode == null)
            {
                InitializeFileStructure(); // we initialize the file structure.
                DrawElement(tree, (0, 0));      
                File.AppendAllText(fileName, "</html>");
                Process.Start(@"cmd.exe", "/c " + fileName);
                tempCount++;
                return;
            }

            int max_left_node = 0; //how much left we take from the beginning (max value).
            BinaryTree<T>.LeftNode(tree, 0, ref max_left_node); // the max left node.
            BinaryTree<T> most_left = BinaryTree<T>.max_left_node;
            int rows = BinaryTree<T>.RowsFromTop(most_left, tree); //how much offset we take from the top to the max left node.

            // we start by finding the position of the head of the tree.
            (int x, int y) head_position = (0, 0);
            head_position.x = max_left_node * (100 + 50); //the size of every circle + offset between circles.
            head_position.x = rows * (100 + 50); //the size of every circle + offset between circles.

            InitializeFileStructure();
            DrawTree(tree, head_position);

            File.AppendAllText(fileName, "</html>"); //finishes the document.

            //show tree
            Process.Start(@"cmd.exe", "/c " + fileName);

            tempCount++; //for the next temp file.
        }

        /// <summary>
        /// Deletes all of the temporary tree files that were created.
        /// </summary>
        public static void ClearTrees()
        {
            string directory = Directory.GetCurrentDirectory();
            Regex reg = new Regex(@"BINTREE\d+\.html");

            List<string> fileNames = Directory.GetFiles(directory).ToList();
            fileNames = reg.Matches(string.Join(" ", fileNames)).Select(x => x.Value).ToList();
            fileNames.ForEach(x => File.Delete(x)); // we delete all of the temporary files.
        }

        /// <summary>
        /// Draws the full tree to the file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="head">The head of the tree.</param>
        /// <param name="position">The starting position to draw the tree.</param>
        private static void DrawTree<T>(BinaryTree<T> head, (int x, int y) position)
        {
            DrawElement(head, position);

            if(head.rightNode != null)
            {
                DrawLine(position, (position.x + 150, position.y + 150));
                DrawTree(head.rightNode, (position.x + 150, position.y + 150));
            }

            if(head.leftNode != null)
            {
                DrawLine(position, (position.x - 150, position.y + 150));
                DrawTree(head.leftNode, (position.x - 150, position.y + 150));
            }
        }

        /// <summary>
        /// Draws line between the nodes.
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private static void DrawLine((int x, int y) p1, (int x, int y) p2)
        {
            double left;

            if (p2.x < p1.x)
            {
                left = Math.Min(p1.x, p2.x);
            }
            else
            {
                left = p2.x - 75;
            }

            string line = $"\n<div class = 'line' style = 'left:{left}px;top:{p1.y}px;transform:rotate({45 * (Math.Sign(p2.x - p1.x))}deg);'></div>";
            File.AppendAllText(fileName, line);
        }
        
        /// <summary>
        /// Draws a node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node"></param>
        /// <param name="position"></param>
        private static void DrawElement<T>(BinaryTree<T> node, (double x, double y) position)
        {
            string color = "red"; //Red -> left side node, Blue -> right side node.

            if (node.GetParent()?.rightNode == node)
                color = "blue";

            File.AppendAllText(fileName, $"\n<div class ='b' id = 'circle' style='border: 1px solid {color};position: absolute; left: {position.x}px; top: {position.y}px;'></div>");
            File.AppendAllText(fileName, $"\n<div style='color:{color};position: absolute; left: {position.x - (node.ToString().Length / 2) * 4 + 32}px; top: {position.y + 28}px;'>{node.value}</div>");
        }

        /// <summary>
        /// Creating a File.
        /// </summary>
        /// <returns></returns>
        private static string InitializeFileStructure()
        {
            string content = @"<html>

<style>
	#circle{
		border-radius: 50%;
		display: inline-block;
		border: 1px solid black;
	}


	.a{
		padding: 50px;
	}

	.b{
		width: 70px;
		height: 70px;
	}

	 .line{
width: 150px;
height: 150px;
border-bottom: 1px solid black;
position: absolute;
}
</style>";

            File.WriteAllText(fileName, content);
            return fileName;
        }
    }
}
