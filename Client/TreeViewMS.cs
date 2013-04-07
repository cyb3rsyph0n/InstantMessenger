using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Client
{
    internal class TreeViewMS : TreeView
    {
        private List<TreeNode> mSelectedNodes = new List<TreeNode>();
        private Dictionary<TreeNode, Color> oldForeColors = new Dictionary<TreeNode, Color>();
        private Dictionary<TreeNode, Color> oldBackColors = new Dictionary<TreeNode, Color>();

        public List<TreeNode> SelectedNodes
        {
            get
            {
                return mSelectedNodes;
            }
            set
            {
                mSelectedNodes = value;
            }
        }

        protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
        {
            base.OnBeforeSelect(e);

            bool Shift = (ModifierKeys == Keys.Shift);
            bool CTRL = (ModifierKeys == Keys.Control);

            if (e.Node.Tag == null)
            {
                e.Cancel = true;
                return;
            }

            if (CTRL && mSelectedNodes.Contains(e.Node))
            {
                e.Cancel = true;
                mSelectedNodes.Remove(e.Node);
                repaintNodes();
            }
        }

        protected override void OnAfterSelect(TreeViewEventArgs e)
        {
            base.OnAfterSelect(e);

            bool Shift = (ModifierKeys == Keys.Shift);
            bool CTRL = (ModifierKeys == Keys.Control);

            if (CTRL || mSelectedNodes.Count == 0)
                if (mSelectedNodes.Contains(e.Node))
                    mSelectedNodes.Remove(e.Node);
                else
                    mSelectedNodes.Add(e.Node);
            else
            {
                mSelectedNodes.Clear();
                mSelectedNodes.Add(e.Node);
            }

            this.SelectedNode = null;
            repaintNodes();
        }

        private void GetAllNodes(ref List<TreeNode> Nodes, TreeNode StartNode)
        {
            Nodes.Add(StartNode);

            foreach (TreeNode tmpNode in StartNode.Nodes)
            {
                GetAllNodes(ref Nodes, tmpNode);
            }
        }

        private void repaintNodes()
        {
            List<TreeNode> allNodes = new List<TreeNode>();

            foreach(TreeNode tmpNode in this.Nodes)
                GetAllNodes(ref allNodes, tmpNode);

            foreach(TreeNode tmpNode in allNodes)
            {
                Color hiBack = SystemColors.Highlight;
                Color hiFore = SystemColors.HighlightText;

                if (oldBackColors.ContainsKey(tmpNode) == false)
                {
                    oldBackColors.Add(tmpNode, tmpNode.BackColor);
                    oldForeColors.Add(tmpNode, tmpNode.ForeColor);
                }

                if (mSelectedNodes.Contains(tmpNode))
                {
                    tmpNode.BackColor = hiBack;
                    tmpNode.ForeColor = hiFore;
                }
                else
                {
                    tmpNode.BackColor = (oldBackColors.ContainsKey(tmpNode) ? oldBackColors[tmpNode] : SystemColors.Window);
                    tmpNode.ForeColor = (oldForeColors.ContainsKey(tmpNode) ? oldForeColors[tmpNode] : SystemColors.WindowText);

                    oldBackColors.Remove(tmpNode);
                    oldForeColors.Remove(tmpNode);
                }
            }
        }
    }
}
