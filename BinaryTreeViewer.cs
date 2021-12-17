using System.Diagnostics;
using System.Text.RegularExpressions;

namespace BinaryTreeViewer
{
    /// <summary>
    /// Shows in an HTML document a graph of the tree.
    /// RECOMMENDATION: Use break-point on the line of the BinaryTreeViewer.View .
    /// </summary>
    public static class BTViewer
    {
        private static int StartingTempCount = 1; //the starting temp count so we know how many
        //trees we've created.
        private static int tempCount = 1; // the number of temporary files we've created.
        private static string fileName => $"BINTREE{tempCount}.html"; //name structure of BINTREE files.

        /// <summary>
        /// Sets the value of tempCount according to the previous saved_trees.
        /// </summary>
        static BTViewer()
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

            StartingTempCount = tempCount;
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
                
                //If the operating System is windows
                if(OperatingSystem.IsWindows())
                    Process.Start(@"cmd.exe", "/c " + fileName);

                //If the operating System is Linux
                if (OperatingSystem.IsLinux())
                    Process.Start(@"xdg-open " + fileName);
                tempCount++;
                return;
            }

            //how much left we take from the beginning (max value). -> max_left_offset
            (BinaryTree<T>? leftNode, int max_left_offset) = tree.LeftNode(); // the max left node.
            int rows = BinaryTree<T>.RowsFromTop(leftNode, tree); //how much offset we take from the top to the max left node.

            // we start by finding the position of the head of the tree.
            (int x, int y) head_position = (0, 0);
            head_position.x = max_left_offset * (100 + 50); //the size of every circle + offset between circles.
            head_position.x = rows * (100 + 50); //the size of every circle + offset between circles.

            InitializeFileStructure();
            DrawTree(tree, head_position);

            File.AppendAllText(fileName, "</html>"); //finishes the document.

            //shows the tree to the user. (opens the HTML file on browser).
            Process run_process = Process.Start(@"cmd.exe", "/c " + fileName);
            run_process.WaitForExit();

            tempCount++;
        }

        /// <summary>
        /// Deletes the trees we want to clear.
        /// </summary>
        public static void ClearTrees(TreesToClear treesToClear)
        {
            string directory = Directory.GetCurrentDirectory();
            Regex reg = new Regex(@"BINTREE\d+\.html"); //the structure of a BINTREE runtime file.
            Regex findCount = new Regex(@"\d+");

            List<string> fileNames = Directory.GetFiles(directory).ToList();
            fileNames = reg.Matches(string.Join(" ", fileNames)).Select(x => x.Value).ToList();

            if (((int)treesToClear & 0b1) != 0) //current run.
            {
                fileNames.Where(x => int.Parse(findCount.Match(x).Value) >= StartingTempCount)
                         .ToList().ForEach(x => File.Delete(x));
            }

            if(((int)treesToClear & 0b10) != 0) //other runs.
            {
                fileNames.Where(x => int.Parse(findCount.Match(x).Value) < StartingTempCount)
                         .ToList().ForEach(x => File.Delete(x));
            }
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
            //DIAMOND COLLISION MEANING -> 
            /* 
                    ( )
                    / \
                  ( ) ( )
                    \ /
                    ( ) -> collision (two different nodes placed on the same place in the graph
                                      because the distance between each node to his father is
                                      equal).
             */
            //In a case of diamond collision one node might override the other node on a graph.
            //Because we want to see both nodes on the graph then we color nodes that
            //comes from right and nodes that comes from left
            //with two different colors -> Red & Blue -> so we'll be able to see the differences.
            string color = "red"; //Red -> left side node, Blue -> right side node.

            if (node.GetParent()?.rightNode == node)
                color = "blue";

            File.AppendAllText(fileName, $"\n<div class ='b' id = 'circle' style='border: 1px solid {color};position: absolute; left: {position.x}px; top: {position.y}px;'></div>");
            File.AppendAllText(fileName, $"\n<div style='color:{color};position: absolute; left: {position.x - (node.ToString().Length / 2) * 4 + 32}px; top: {position.y + 28}px;'>{node.value}</div>");
        }

        /// <summary>
        /// Creating a BINTREE file structre.
        /// </summary>
        /// <returns></returns>
        private static string InitializeFileStructure()
        {
            //The basic content of a BINTREE file.
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

public enum TreesToClear
{
    CurrentRun = 0b1,
    PreviousRuns = 0b10
}
