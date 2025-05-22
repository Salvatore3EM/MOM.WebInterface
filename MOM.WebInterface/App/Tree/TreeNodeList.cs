﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App.Tree
{
    public class TreeNodeList : List<TreeNode>
    {
        public TreeNode Parent;

        public TreeNodeList(TreeNode Parent)
        {
            this.Parent = Parent;
        }

        public new TreeNode Add(TreeNode Node)
        {
            base.Add(Node);
            Node.Parent = Parent;
            return Node;
        }
        public TreeNode Add(T Value)
        {
            return Add(new TreeNode(Value));
        }

        public override string ToString()
        {
            return "Count =" +Count.ToString();
        }

    }

}