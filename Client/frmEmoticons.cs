using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Client
{
    public partial class frmEmoticons : Form
    {
        public delegate void EmoticonClickedDelegate(string EmoticonText);
        public event EmoticonClickedDelegate EmoticonClicked;

        public frmEmoticons(frmConversation Owner, List<Dictionary<string, string>> Emoticons)
        {
            InitializeComponent();
            this.Owner = Owner;

            //LOOP THROUGH EACH EMOTICON IN THE LIST WE WERE GIVEN AND ADD THEM TO OUR DISPLAY
            foreach (Dictionary<string, string> ImageFile in Emoticons)
            {
                PictureBox tmpPic = new PictureBox();

                //SETUP THE PICTURE BOXES PROPERTIES BEFORE ADDING IT TO THE DISPLAY
                tmpPic.ImageLocation = Path.Combine(Application.StartupPath, "Images\\Emoticons\\" + ImageFile["image"]);
                tmpPic.Size = new Size(28, 28);
                tmpPic.SizeMode = PictureBoxSizeMode.StretchImage;
                tmpPic.Tag = ImageFile["send"];
                tmpPic.Visible = true;

                //SETUP EVENT HANDLERS
                tmpPic.Click += new EventHandler(tmpPic_Click);
                tmpPic.MouseEnter += new EventHandler(tmpPic_MouseEnter);
                tmpPic.MouseLeave += new EventHandler(tmpPic_MouseLeave);

                //ADD THE CONTROL TO THE FLOW LAYOUT PANEL
                panelPicHolder.Controls.Add(tmpPic);
            }
        }

        void tmpPic_MouseLeave(object sender, EventArgs e)
        {
            //REMOVE THE BORDER FROM THE PICTURE BOX WHICH RAISED THIS EVENT
            ((PictureBox)sender).BorderStyle = BorderStyle.None;
        }

        void tmpPic_MouseEnter(object sender, EventArgs e)
        {
            //REMOVE ALL BORDERS TO CATCH WHEN THE EVENT DIDNT FIRE
            foreach (PictureBox tmpPic in panelPicHolder.Controls)
                tmpPic.BorderStyle = BorderStyle.None;

            //ADD THE BORDER TO THE PICTUREBOX WHICH RAISED THIS EVENT
            ((PictureBox)sender).BorderStyle = BorderStyle.FixedSingle;
        }

        void tmpPic_Click(object sender, EventArgs e)
        {
            //RAISE OUR EVENT SO THE OWNER FORM KNOWS AN EMOTICON WAS CLICKED
            if (EmoticonClicked != null)
                EmoticonClicked((string)((PictureBox)sender).Tag);

            //REFOCUS THE OWNER FORM
            Owner.Focus();
        }

        private void frmEmoticons_Resize(object sender, EventArgs e)
        {
            //ADJUST THE HEIGHT OF THE FORM TO ADJUST FOR THE EMOTICONS
            if (panelPicHolder.Controls.Count != 0)
                this.Height = panelPicHolder.Controls[panelPicHolder.Controls.Count - 1].Bounds.Bottom + panelPicHolder.Margin.Top + panelPicHolder.Margin.Bottom;
        }
    }
}
