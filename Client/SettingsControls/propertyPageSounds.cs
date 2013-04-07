using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Interfaces;

namespace Client.SettingsControls
{
    public partial class propertyPageSounds : UserControl, IPropertyPage
    {
        public propertyPageSounds()
        {
            InitializeComponent();
        }

        #region IPropertyPage Members

        public bool HavePropertiesChanged
        {
            get { throw new NotImplementedException(); }
        }

        public void SaveSettings()
        {
            throw new NotImplementedException();
        }

        public void LoadSettings()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
