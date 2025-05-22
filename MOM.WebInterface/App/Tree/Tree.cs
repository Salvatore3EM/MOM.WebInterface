using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App.Tree
{
    public class Tree : TreeNode
    {
        public Tree() { }

        public Tree(T RootValue)
        {
            Value = RootValue;
        }

    }


}