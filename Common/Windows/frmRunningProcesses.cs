using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Messages;
using Common.Connections;

namespace Common.Windows
{
    public partial class frmRunningProcesses : Form
    {
        public Connection ThisConnection { get; set; }

        public frmRunningProcesses()
        {
            InitializeComponent();
        }

        private void killProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow tmpRow in gridRunningProcesses.SelectedRows)
            {
                //TODO: NEED TO TRACK WHICH USER THE RUNNING PROCESSES CAME FROM AND PROBABLY MOVE THIS FORM TO THE CLIENT
                new Message_KillProcess() { PID = (int)tmpRow.Cells["PID"].Value, UserID = "Chanceyman" }.Send(ThisConnection);
            }
        }
    }
}
