using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Interfaces
{
    public interface IConversationWindow
    {
        string ConversationID { get; set; }
        void ReceiveMessage(Messages.Message_Private IncomingMessage);
        void EnteredText(string UserName);
        void ClearMessages();
    }
}
