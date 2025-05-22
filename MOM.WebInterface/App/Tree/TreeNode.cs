using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MOM.WebInterface.App.Tree
{
    public class TreeNode : IDisposable
    {

        #region ' ctor '

        public TreeNode(T Value)
        {
            this.Value = Value;
            Parent = null;
            Children = new TreeNodeList(this);
        }

        public TreeNode(T Value, TreeNode Parent)
        {
            this.Value = Value;
            this.Parent = Parent;
            Children = new TreeNodeList(this);
        }

        #endregion



        private TreeNode _Parent;

        public TreeNode Parent
        {
            get { return _Parent; }

            set
            {
                if (value == _Parent)
                {
                    return;
                }
                if (_Parent != null)
                {
                    _Parent.Children.Remove(this);
                }

                if (value != null && !value.Children.Contains(this))
                {
                    value.Children.Add(this);
                }
                _Parent = value;
            }
        }

        public TreeNode Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;
                TreeNode node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        private TreeNodeList _Children;

        public TreeNodeList Children
        {
            get { return _Children; }
            private set { _Children = value; }
        }

        private T _Value;
        private bool disposedValue;

        public T Value
        {
            get { return _Value; }
            set
            {
                _Value = value;
                if (_Value != null && _Value is ITreeNodeAware)
                {
                    (_Value as ITreeNodeAware).Node = this;
                }
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~TreeNode()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }


}