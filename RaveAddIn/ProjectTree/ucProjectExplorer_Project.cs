﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RaveAddIn
{
    partial class ucProjectExplorer
    {
        private void BuildProjectCMS()
        {
            if (cmsProject != null)
                return;

            cmsProject = new ContextMenuStrip(components);
            cmsProject.Items.Add("Explore Project Folder", Properties.Resources.BrowseFolder, OnExplore);
            cmsProject.Items.Add("View Project MetaData", Properties.Resources.metadata, OnMetaData);
            cmsProject.Items.Add("Close Project", null, OnClose);
        }

        private void treProject_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
                return;

            TreeNode theNode = treProject.GetNodeAt(e.X, e.Y);
            if (theNode is TreeNode)
                treProject.SelectedNode = theNode;
        }

        public void OnExplore(object sender, EventArgs e)
        {
            RaveProject proj = (RaveProject)treProject.SelectedNode.Tag;
            System.Diagnostics.Process.Start(proj.ProjectFile.Directory.FullName);
        }

        public void OnClose(object sender, EventArgs e)
        {
            treProject.SelectedNode.Remove();
        }

        public void OnMetaData(object sender, EventArgs e)
        {
            RaveProject proj = (RaveProject)treProject.SelectedNode.Tag;

            MetaData.frmMetaData frm = new MetaData.frmMetaData("Riverscapes Project", proj.MetDataNode);

            //frm.MetaDataItems.Insert(0, new MetaData.MetaDataItem("Project Name", proj.Name));
            //frm.MetaDataItems.Insert(1, new MetaData.MetaDataItem("Project Type", proj.ProjectType));
            frm.MetaDataItems.Insert(2, new MetaData.MetaDataItem("Project File", proj.ProjectFile.FullName));
            frm.ShowDialog();
        }
    }
}