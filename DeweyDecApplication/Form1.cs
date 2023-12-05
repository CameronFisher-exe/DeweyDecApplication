using DeweyDecApplication.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//-------------------------------------------------------o0START0o------------------------------------------------//

namespace DeweyDecApplication
{
    public partial class Form1 : Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;

        public Form1()
        {
            InitializeComponent();

            //Set the FormBorderStyle to FixedSingle to disable resizing
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            //Disable the maximize button
            this.MaximizeBox = false;

            random = new Random();
        }

        //----------------------------------------------------------------------------------------------------------------//

        private Color SelectThemeColour()
        {
            int index = random.Next(ColourTheme.ColourList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ColourTheme.ColourList.Count);
            }
            tempIndex = index;
            string colour = ColourTheme.ColourList[tempIndex];
            return ColorTranslator.FromHtml(colour);
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if(currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColour();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitle.BackColor = color;
                    panelLogo.BackColor = ColourTheme.ChangeColorBrightness(color, -0.3);
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if(previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(111, 143, 175);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }

        //----------------------------------------------------------------------------------------------------------------//

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelHomeDesktop.Controls.Add(childForm);
            this.panelHomeDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        //----------------------------------------------------------------------------------------------------------------//

        #region Buttons
        //The buttons in the navigation bar
        private void btnHomePage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.Home(), sender);
        }

        private void btnGamePage_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.DeweyGame(), sender);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnIdentifyAreas_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.IdentifyArea(), sender);
        }

        private void btnFindCallNum_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Forms.FindingCallNum(), sender);
        }
        #endregion
    }

}
//---------------------------------------------------------o0END0o--------------------------------------------------//
