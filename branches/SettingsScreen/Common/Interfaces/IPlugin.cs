using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Enumerations;
using System.Windows.Forms;
using System.Drawing;
using Common.Connections;

namespace Common.Interfaces
{
    public interface IPlugin : IDisposable
    {
        string Name { get; }

        Version VersionInfo { get; }
        PluginType PluginType { get; }

        Image PropertyPageImage { get; }
        Control PropertyPage { get; }

        ToolStripMenuItem Initialize(Form ParentWindow, Connection ThisConnection);
    }
}
