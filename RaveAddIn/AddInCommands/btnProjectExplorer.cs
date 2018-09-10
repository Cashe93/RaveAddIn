﻿using RaveAddIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAddIn.AddInCommands
{
    class btnProjectExplorer : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        protected override void OnClick()
        {
            try
            {
                ShowProjectExplorer(true);
            }
            catch (Exception ex)
            {
                GCDCore.GCDException.HandleException(ex);
            }

            ArcMap.Application.CurrentTool = null;
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        public static void ShowProjectExplorer(bool bVisible)
        {
            // Note that the IsVisible property on the dockable window does not
            // seem to track the state of the window properly. Especially if the user
            // clicks the "Pin" icon to close the dockable window. This seems to be a reliable
            // method for re-opening the dockable window when this happens.
            // http://forums.arcgis.com/threads/11692-ArcGIS10-Add-Ins-and-Dockable-Window
            //
            ESRI.ArcGIS.esriSystem.IUID pUI = new ESRI.ArcGIS.esriSystem.UID();
            pUI.Value = ThisAddIn.IDs.ucProjectExplorer;

            ESRI.ArcGIS.Framework.IDockableWindow docWin = ArcMap.DockableWindowManager.GetDockableWindow((ESRI.ArcGIS.esriSystem.UID)pUI);
            if (docWin is ESRI.ArcGIS.Framework.IDockableWindow)
            {
                docWin.Show(bVisible);

                if (bVisible)
                {
                    try
                    {
                        // Try and refresh the project window.
                        ucProjectExplorer.AddinImpl winImpl = ESRI.ArcGIS.Desktop.AddIns.AddIn.FromID<ucProjectExplorer.AddinImpl>(ThisAddIn.IDs.ucProjectExplorer);
                        winImpl.UI.LoadTree();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Error attempting to open project explorer {0}", ex.Message));
                        // Do nothing if it fails.
                    }
                }
            }
        }
    }
}