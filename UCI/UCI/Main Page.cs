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


        private void button1_Click(object sender, EventArgs e)
        {
            if (radioPeople.Checked)
            {
                tabControl.SelectedTab = tabPeople;
            }
            if (radioCalendar.Checked)
            {
                tabControl.SelectedTab = tabCalendar;
                MessageBox.Show("This page is under construction, please return to the main page.");
            }
            if (radioSecurity.Checked)
            {
                tabControl.SelectedTab = tabSecurity;
                MessageBox.Show("Access is restricted to normal users.");
            }
            if (radioSupervisor.Checked)
            {
                tabControl.SelectedTab = tabSupervisor;
                MessageBox.Show("Access is restricted to normal users.");
            }
            if (radioDoors.Checked)
            {
                tabControl.SelectedTab = tabDoors;
            }


        }

        private void returnButton1_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabMain;
        }

        private void buttonReturn2_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabMain;
        }

   


    }


}
