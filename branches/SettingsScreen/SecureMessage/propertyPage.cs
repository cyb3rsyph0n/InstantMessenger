using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Interfaces;

namespace SecureMessage
{
    public partial class propertyPage : UserControl, IPropertyPage
    {
        public propertyPage()
        {
            InitializeComponent();
        }

        public void SaveSettings()
        {
            throw new NotImplementedException();
        }

        public void LoadSettings()
        {
            throw new NotImplementedException();
        }

        public bool HavePropertiesChanged
        {
            get { throw new NotImplementedException(); }
        }
    }
}
