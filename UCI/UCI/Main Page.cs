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
                CalendarUpdateController cuc = new CalendarUpdateController();
                cuc.fillCalendarSelect(PersonGroupCalendarComboBox, DoorGroupCalendarComboBox);
                TabNav.SelectedTab = tabCalendar;
                
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
               DoorAccessController dac = new DoorAccessController();
               dac.FillReaderPersonSelect(DoorSelectComboBox, PersonSelectComboBox);
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
            int x;
            if (!int.TryParse(ControlPanelDisplay.Text,out x) && passwordEntryMode)
                ControlPanelDisplay.Text = "";
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
            if(passwordEntryMode)
                ControlPanelDisplay.Text = "";
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            if (!passwordEntryMode) return;
            
            
            this.passwordEntryMode = false; //disable further input
            char[] delim = new char[1];
            delim[0] = ' ';
            if (PersonSelectComboBox.SelectedIndex < 0 || DoorSelectComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Select Person and Door!");
                
            }
            else
            {
                string cardID = PersonSelectComboBox.Items[PersonSelectComboBox.SelectedIndex].ToString().Split()[0];
                string doorID = DoorSelectComboBox.Items[DoorSelectComboBox.SelectedIndex].ToString().Split()[0];
                string password = ControlPanelDisplay.Text;
                DateTime dateTime = DoorAccessDateTimePicker.Value;

                DoorAccessController dac = new DoorAccessController();

                dac.DoorUnlockRequest(cardID, doorID, password, dateTime, ControlPanelDisplay, DoorStatusDisplay,TimeKeeperDisplay);

            }
        }

        private void DoorPersonSelectionButton_Click(object sender, EventArgs e)
        {
            DoorAccessController dac = new DoorAccessController();
            char[] delim = new char[1];
            delim[0] = ' ';
            if (PersonSelectComboBox.SelectedIndex < 0 || DoorSelectComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Select Person and Door!");
                this.passwordEntryMode = false;
            }
            else
            {
                string cardID = PersonSelectComboBox.Items[PersonSelectComboBox.SelectedIndex].ToString().Split()[0];
                this.passwordEntryMode = dac.ValidateCard(cardID, ControlPanelDisplay);
            }

        }

        private void DoorOpenButton_Click(object sender, EventArgs e)
        {
            if (DoorSelectComboBox.SelectedIndex < 0) return;

            DoorAccessController dac = new DoorAccessController();
            String id = DoorSelectComboBox.Items[DoorSelectComboBox.SelectedIndex].ToString().Split()[0];
            dac.DoorOpenRequest(id,AlarmTimerDisplay,ControlPanelDisplay);
            
        }

        private void DoorCloseButton_Click(object sender, EventArgs e)
        {
             if(DoorSelectComboBox.SelectedIndex < 0) return;

            DoorAccessController dac = new DoorAccessController();
            String id = DoorSelectComboBox.Items[DoorSelectComboBox.SelectedIndex].ToString().Split()[0];
            dac.DoorCloseRequest(id, DoorStatusDisplay,ControlPanelDisplay);

        }

        private void AccessPanelTab_Click(object sender, EventArgs e)
        {

        }

        private void Mainpage_Load(object sender, EventArgs e)
        {

        }

        private void PersonGroupCalendarComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void HourSelectCheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PersonGroupCalendarComboBox.SelectedIndex < 0 ||
                DoorGroupCalendarComboBox.SelectedIndex < 0 ||
                DayCalendarComboBox.SelectedIndex < 0)
            {
                MessageBox.Show("Enter all required information");
            }
            else
            {
                string pgID = PersonGroupCalendarComboBox.Items[PersonGroupCalendarComboBox.SelectedIndex].ToString();
                string dgID = DoorGroupCalendarComboBox.Items[DoorGroupCalendarComboBox.SelectedIndex].ToString();
                DayOfWeek day = (DayOfWeek)DayCalendarComboBox.SelectedIndex;
                CalendarUpdateController cuc = new CalendarUpdateController();

                cuc.getCalendarStatusForDay(pgID, dgID, day, HourSelectCheckListBox);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string pgID = PersonGroupCalendarComboBox.Items[PersonGroupCalendarComboBox.SelectedIndex].ToString();
            string dgID = DoorGroupCalendarComboBox.Items[DoorGroupCalendarComboBox.SelectedIndex].ToString();
            DayOfWeek day = (DayOfWeek)DayCalendarComboBox.SelectedIndex;

            Boolean[] status = new Boolean[24];

            for (int i = 0; i < 24; i++)
            {
                status[i] = HourSelectCheckListBox.CheckedIndices.Contains(i);

            }

            CalendarUpdateController cuc = new CalendarUpdateController();

            cuc.setAccessStatus(pgID, dgID, day, status);

            HourSelectCheckListBox.Items.Clear();
            MessageBox.Show("Calendar Updated!");

        }


   


    }


}
