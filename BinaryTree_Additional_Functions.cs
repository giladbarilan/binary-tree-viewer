using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeViewer
{
    public partial class BinaryTree<T>
    {
        /// <summary>
        /// Finds the max left offset from the starting node.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="head">The beginning of the tree we want to draw.</param>
        /// <param name="left_offset"></param>
        /// <param name="max_offset"></param>
        /// 

        internal int GetMaxLeft()
        {
            int max_offset = 0;

            GetMaxLeft(this, 0, ref max_offset);
            return max_offset;
        }

        private void GetMaxLeft(BinaryTree<T> head, int left_offset, ref int max_offset)
        {
            if (head.GetLeftNode() != null)
            {
                left_offset += 1;

                GetMaxLeft(head.GetLeftNode(), left_offset, ref max_offset);
            }
            if (head.GetRightNode() != null)
            {
                left_offset -= 1;
                GetMaxLeft(head.GetRightNode(), left_offset, ref max_offset);
            }

            if(left_offset > max_offset)
            {
                max_offset = left_offset;
            }
        }
    }
}
