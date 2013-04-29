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
        

        private void button1_Click(object sender, EventArgs e) // Goto navigation button
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

        private void returnButton1_Click(object sender, EventArgs e) //Return to mainpage navigation button
        {
            tabControl.SelectedTab = tabMain;
        }

        private void buttonReturn2_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = tabMain;
        }
      
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            if (textPassword.Text != textPassword2.Text)
            {

                MessageBox.Show("Your passwords do not match, please reenter password.");

            }
            else
            {

                Person newPerson = new Person(idglobal.num, textFname.Text, textLname.Text, comboGroup.Text, textPassword.Text);
                idglobal.num = idglobal.num + 1;
                ListViewItem item = new ListViewItem(newPerson.GrouptoString());
                item.SubItems.Add(newPerson.IDtoString());
                item.SubItems.Add(newPerson.NametoString());
                listPeople.Items.Add(item);
                
            }
        }

        private void buttonSecPrint_Click(object sender, EventArgs e)
        {

        }

        private void buttonGroup_Click(object sender, EventArgs e)
        {
            comboGroup.Items.Add(textGroup.Text);
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            comboGroupDoor.Items.Add(textDoorGroup.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {


            Door newDoor = new Door(idglobal2.num,comboGroupDoor.Text);
            idglobal2.num = idglobal2.num + 1;
            ListViewItem item = new ListViewItem(newDoor.GrouptoString());
            item.SubItems.Add(newDoor.IDtoString());
            listDoor.Items.Add(item);


        }

   


    }








}
