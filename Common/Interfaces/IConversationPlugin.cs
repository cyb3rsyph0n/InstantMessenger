using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Connections;
using System.Windows.Forms;
using Common.Messages;

namespace Common.Interfaces
{
    public interface IConversationPlugin : IPlugin
    {
        bool SupportsSending { get; }
        bool SupportsReceiving { get; }

        int SendOrder { get; }
        int ReceiveOrder { get; }

        void OutgoingMessage(ref Message_Private OutgoingMessage, ref bool ContinueToSend);
        void IncomingMessage(ref Message_Private IncomingMessage, ref bool DisplayMessage);
    }
}
