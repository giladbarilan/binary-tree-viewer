using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryViewer
{
    public partial class BinaryTree<T>
    {
        internal static BinaryTree<T> max_left_node;

        /// <summary>
        /// Finds the max left offset from the starting node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="head">The beginning of the tree we want to draw.</param>
        /// <param name="left_offset"></param>
        /// <param name="max_offset"></param>
        internal static void LeftNode(BinaryTree<T> head, int left_offset, ref int max_offset)
        {
            if (head.leftNode != null)
            {
                left_offset += 1;

                if(left_offset > max_offset)
                {
                    max_left_node = head.leftNode;
                }

                LeftNode(head.leftNode, left_offset, ref max_offset);
            }
            if (head.rightNode != null)
            {
                left_offset -= 1;
                LeftNode(head.rightNode, left_offset, ref max_offset);
            }

            if(left_offset > max_offset)
            {
                max_offset = left_offset;
            }
        }

        /// <summary>
        /// Finds the max offset from the top of the <b>tree we want to write</b>.
        /// </summary>
        /// <param name="left_node">the max left node.</param>
        /// <param name="top">the top of the tree we want to write.</param>
        /// <returns></returns>
        internal static int RowsFromTop(BinaryTree<T> left_node, BinaryTree<T> top)
        {
            int rows = 0;

            while(left_node != top)
            {
                left_node = left_node.father;
                rows++;
            }

            return rows;
        }
    }
}
