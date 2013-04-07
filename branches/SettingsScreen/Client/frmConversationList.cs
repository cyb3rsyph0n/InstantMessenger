using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Interfaces;
using Common.Messages;
using Common.Connections;
using Common.Windows;
using Telerik.WinControls.UI;

namespace Client
{
    public partial class frmConversationList : Form, IRecentConversationsWindow
    {
        public frmConversationList()
        {
            InitializeComponent();
        }

        #region IRecentConversationsWindow Members

        public Connection ThisConnection { get; set; }

        public object[] RecentList
        {
            set
            {
                gridConversations.DataSource = value;
                gridConversations.MasterGridViewTemplate.Columns[0].FormatString = "{0:MM/dd/yy}";
            }
        }

        #endregion

        private void GetConversation(string ConversationID)
        {
            bool FoundWindow = false;
            frmConversation tmpWindow = null;

            //DOUBLE CHECK TO MAKE SURE THE WINDOW IS NOT ALREADY OPEN
            foreach (Form openWindow in Application.OpenForms)
            {
                try
                {
                    //TRY TO CONVERT EVERY WINDOW INTO A CONVERSATION WINDOW
                    tmpWindow = (frmConversation)openWindow;

                    //CHECK THE TAG TO SEE IF THIS IS THE CONVERSATION THE USER IS LOOKING FOR
                    if ((string)tmpWindow.Tag == ConversationID)
                        FoundWindow = true;
                }
                catch
                {
                    //THIS IS TO CATCH THE OTHER WINDOWS WHICH ARE NOT OF TYPE FRMCONVERSATION
                }
            }

            //IF THE WINDOW WAS NOT FOUND THEN ASK THE SERVER FOR THE CONVERSATION
            if (!FoundWindow)
            {
                frmConversation tmpConversation = new frmConversation(ThisConnection, ConversationID);
                tmpConversation.Show();

                new Message_GetConversation() { ConversationID = ConversationID }.Send(ThisConnection);
            }
            else
            {
                //THE WINDOW WAS FOUND SO WE NEED TO JUST BRING IT TO THE FRONT AND SHOW IT
                tmpWindow.Show();
                tmpWindow.BringToFront();
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            //LOOP THROUGH ALL OF THE SELECTED CONVERSATIONS AND GET THEM FROM THE SERVER
            foreach (GridViewRowInfo tmpRow in gridConversations.SelectedRows)
                GetConversation((string)tmpRow.Cells["ConversationID"].Value);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //CLOSE THIS WINDOW
            this.Close();
        }
    }
}
