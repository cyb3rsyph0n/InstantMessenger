using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Common.Interfaces
{
    public interface IPropertyPage
    {
        bool HavePropertiesChanged { get; }
        void SaveSettings();
        void LoadSettings();
    }
}
