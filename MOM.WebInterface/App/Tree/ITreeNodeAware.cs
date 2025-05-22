using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App.Tree
{
    public interface ITreeNodeAware
    {
        TreeNode Node { get; set; }
    }

    public class Task : ITreeNodeAware<Task>
    {
        public bool Complete = false;

        private TreeNode<Task> _Node;

        public TreeNode<Task> Node
        {
            get { return _Node; }
            set
            {
                _Node = value;
                // do something when the Node changes
                // if non-null, maybe we can do some setup
            }
        }

        // recursive
        public void MarkComplete()
        {
            // mark all children, and their children, etc., complete
            foreach (TreeNode<Task> ChildTreeNode in Node.Children)
            {
                ChildTreeNode.Value.MarkComplete();
            }

            // now that all decendents are complete, mark this task complete
            Complete = true;
        }

    }

}