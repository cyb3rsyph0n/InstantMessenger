using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Client
{
    public partial class frmSending : Form
    {
        public string FileName
        {
            set
            {
                lblFileName.Text = value;
            }
        }
        public frmSending()
        {
            InitializeComponent();
        }
    }
}
