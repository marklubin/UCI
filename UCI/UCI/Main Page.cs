using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCI
{
    public partial class Mainpage : Form
    {
        public Mainpage()
        {
            InitializeComponent();
             
        }

        private Boolean passwordEntryMode = false;

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioPeople.Checked)
            {
                TabNav.SelectedTab = tabPeople;
            }
            if (radioCalendar.Checked)
            {
                TabNav.SelectedTab = tabCalendar;
                MessageBox.Show("This page is under construction, please return to the main page.");
            }
            if (radioSecurity.Checked)
            {
                TabNav.SelectedTab = tabSecurity;
                MessageBox.Show("Access is restricted to normal users.");
            }
            if (radioSupervisor.Checked)
            {
                TabNav.SelectedTab = tabSupervisor;
                MessageBox.Show("Access is restricted to normal users.");
            }
            if (radioDoors.Checked)
            {
                TabNav.SelectedTab = tabDoors;
            }
            if(radioAccessPanel.Checked)
            {
               TabNav.SelectedTab = AccessPanelTab;
            }

        }

        private void returnButton1_Click(object sender, EventArgs e)
        {
            TabNav.SelectedTab = tabMain;
        }

        private void buttonReturn2_Click(object sender, EventArgs e)
        {
            TabNav.SelectedTab = tabMain;
        }

        private void keypadDigitEntered(String text) 
        {
            if (passwordEntryMode)
                ControlPanelDisplay.Text += text;
        }

        private void KeypadGroupBox_Enter(object sender, EventArgs e)
        {

        }

        private void keypadButton1_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton1.Text);
        }

        private void keypadButton9_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton9.Text);
        }

        private void keypadButton8_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton8.Text);
        }

        private void keypadButton7_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton7.Text);
        }

        private void keypadButton6_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton6.Text);
        }

        private void keypadButton5_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton5.Text);
        }

        private void keypadButton4_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton4.Text);
        }

        private void keypadButton3_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton3.Text);
        }

        private void keypadButton2_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton2.Text);
        }

        private void keypadButton0_Click(object sender, EventArgs e)
        {
            keypadDigitEntered(keypadButton0.Text);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            ControlPanelDisplay.Text = "";
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            ControlPanelDisplay.Text = "Validating Access......";
        }

        private void DoorPersonSelectionButton_Click(object sender, EventArgs e)
        {
            passwordEntryMode = true;
            ControlPanelDisplay.Text = "";
        }

        private void DoorOpenButton_Click(object sender, EventArgs e)
        {
            DoorStatusDisplay.Text = "Opened";
            DoorStatusDisplay.ForeColor = Color.Lime;
        }

        private void DoorCloseButton_Click(object sender, EventArgs e)
        {
            DoorStatusDisplay.Text = "Closed";
            DoorStatusDisplay.ForeColor = Color.GhostWhite;
        }

        private void AccessPanelTab_Click(object sender, EventArgs e)
        {

        }

        private void Mainpage_Load(object sender, EventArgs e)
        {

        }


   


    }


}
