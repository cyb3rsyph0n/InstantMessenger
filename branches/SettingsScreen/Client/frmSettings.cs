using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Client.SettingsControls;
using Common.Interfaces;
using System.Reflection;
using System.IO;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace Client
{
    public partial class frmSettings : Form
    {
        private Control mCurrentPropertyPage = null;
        private Dictionary<string, IPlugin> Plugins = new Dictionary<string, IPlugin>();

        private Control CurrentPropertyPage
        {
            get
            {
                return mCurrentPropertyPage;
            }
            set
            {
                if (mCurrentPropertyPage != null)
                {
                    pnlPageHolder.Controls.Remove(mCurrentPropertyPage);
                    mCurrentPropertyPage = null;
                }

                if (value != null)
                {
                    mCurrentPropertyPage = value;
                    pnlPageHolder.Controls.Add(mCurrentPropertyPage);
                    mCurrentPropertyPage.Dock = DockStyle.Fill;
                    mCurrentPropertyPage.Visible = true;
                }
            }
        }

        public frmSettings()
        {
            InitializeComponent();
            CreatePluginList();
        }

        private void CreatePluginList()
        {
            RadImageButtonElement tmpImageButton;

            //CREATE A BUTTON FOR EACH OF THE PLUGINS WHICH ARE INTERNAL TO THE APPLICATION STARTUP / CONVERSATIONS / SOUNDS
            tmpImageButton = new RadImageButtonElement() { Text = "Startup", Image= Properties.Resources.media_play_green, DisplayStyle = DisplayStyle.ImageAndText, TextImageRelation = TextImageRelation.ImageAboveText, Tag = new propertyPageStartup() };
            carouselSettings.Items.Add(tmpImageButton);

            tmpImageButton = new RadImageButtonElement() { Text = "Conversations", Image = Properties.Resources.user1_message, DisplayStyle = DisplayStyle.ImageAndText, TextImageRelation = TextImageRelation.ImageAboveText, Tag = new propertyPageConversations() };
            carouselSettings.Items.Add(tmpImageButton);

            tmpImageButton = new RadImageButtonElement() { Text = "Sounds", Image = Properties.Resources.loudspeaker, DisplayStyle = DisplayStyle.ImageAndText, TextImageRelation= TextImageRelation.ImageAboveText, Tag = new propertyPageSounds() };
            carouselSettings.Items.Add(tmpImageButton);

            //LOAD THE REST FROM THE PLUGIN FOLDER NOW
            foreach (FileInfo tmpDLL in new DirectoryInfo(Path.Combine(Application.StartupPath, "Plugins")).GetFiles("*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    Assembly tmpAssembly = System.Reflection.Assembly.LoadFrom(tmpDLL.FullName);
                    foreach (Type tmpType in tmpAssembly.GetTypes())
                    {
                        try
                        {
                            IPlugin tmpPlugin = (IPlugin)tmpAssembly.CreateInstance(tmpType.FullName);
                            tmpImageButton = new RadImageButtonElement() { Image = tmpPlugin.PropertyPageImage, Text = tmpPlugin.Name, DisplayStyle = DisplayStyle.ImageAndText, TextImageRelation = TextImageRelation.ImageAboveText, Tag = tmpPlugin.PropertyPage };

                            carouselSettings.Items.Add(tmpImageButton);

                            //ADD THE PLUGIN TO THE LIST OF LOADED PLUGINS
                            Plugins.Add(tmpPlugin.Name, tmpPlugin);
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                }
            }
            //carouselSettings.SelectedIndex = carouselSettings.Items.Count / 2;
        }

        private void carouselSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CHANGE THE PROPERTY PAGE WHICH IS BEING DISPLAYED TO THE MOST CURRENT PROPERTY PAGE
            this.CurrentPropertyPage = (Control)((RadImageButtonElement)carouselSettings.Items[carouselSettings.SelectedIndex]).Tag;
        }

        private void frmSettings_Shown(object sender, EventArgs e)
        {
            //LOOP THROUGH JUST TO ELIMINATE A BUG WITH THE CAROSEL NOT DISPLAYING PROPERLY
            for (int i = carouselSettings.Items.Count - 1; i >= 0; i--)
            {
                carouselSettings.SelectedIndex = i;
                Application.DoEvents();
                System.Threading.Thread.Sleep(20);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //TRY TO SAVE THE SETTINGS FOR EACH PLUGIN WHICH WE HAVE LOADED
            foreach (RadItem tmpItem in carouselSettings.Items)
            {
                try
                {
                    ((IPropertyPage)tmpItem.Tag).SaveSettings();
                }
                catch
                {
                }
            }

            //CLOSE THE WINDOW ONCE THE SETTINGS HAVE BEEN SAVED
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //APPLY THE CURRENT PAGES SETTINGS TO THE CONFIG FILE
            if (this.CurrentPropertyPage != null)
            {
                try
                {
                    ((IPropertyPage)this.CurrentPropertyPage).SaveSettings();
                    MessageBox.Show("Settings applied");
                }
                catch
                {
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //CLOSE THIS WINDOW WITHOUT SAVING ANY OF THE PROPERTIES
            this.Close();
        }
    }
}
