using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Connections;

namespace Common.Interfaces
{
    public interface IContactsPlugin : IPlugin
    {
        ToolStripMenuItem Initialize(Form ContactsWindow, Connection ThisConnection);
    }
}
