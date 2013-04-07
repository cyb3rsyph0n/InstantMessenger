using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Enumerations;

namespace Common.Delegates
{
    //USED TO CREATE A WINDOW ON THE CLIENT APPLICATION
    public delegate void CreateClientWindow(string Sender, string ConversationID, ref Form NewWindow, WindowType WindowType);
}
