using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UCI
{
    public class DoorAccessController
    {
        public Boolean ValidateCard(string cardID, string doorID, Label display)
        {
            if (!UCIDatabase.cardReadersDict[doorID].activityMode())
            {
                display.Text = "Standby mode...";
                return false;
            }

            if (UCIDatabase.cardDict.ContainsKey(cardID))
            {
                Card c = UCIDatabase.cardDict[cardID];
                if (c.isEnabled)
                {
                    display.Text = "Enter password";
                    return true;
                }
                else
                {
                    display.Text = "Card currently disabled, see security staff.";
                    return false;
                }
            }
            else
            {
                display.Text = "Invalid Card, try again.";
                return false;
            }
        }
       public void FillReaderPersonSelect(ComboBox doors, ComboBox persons)
        {
            doors.Items.Clear();
            persons.Items.Clear();
            foreach (CardReader cr in UCIDatabase.cardReadersDict.Values)
            {
                String label = "";
                label += cr.CardReaderID;
                label += " (";
                foreach (DoorGroup dg in cr.door.groups)
                {
                    label += dg.DoorGroupID + " ";
                }

                label += ")";
                
                doors.Items.Add(label);
            }
            foreach (Card c in UCIDatabase.cardDict.Values)
            {
                Person p = c.person;
                String label = "";
                label += p.PersonID + " ";
                label +=  p.FirstName + " ";
                label +=  p.LastName + " ";
                label += " (";

                foreach(PersonGroup pg in p.groups)
                {
                    label += pg.PersonGroupID + " ";
                }

                label += ")";
                persons.Items.Add(label);
            }
        }
        CardReader findCardReader(string CardReaderID)
        {
            CardReader cr;
            if (UCIDatabase.cardReadersDict.ContainsKey(CardReaderID))
                cr = UCIDatabase.cardReadersDict[CardReaderID];
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException("No such Card Reader");
            }

            return cr;
        }
        IEnumerable<PersonGroup> findPersonGroupRequest(string CardID)
        {
            Card c;
            if (UCIDatabase.cardDict.ContainsKey(CardID))
                c = UCIDatabase.cardDict[CardID];
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException("No such Card.");
            }

            return c.person.groups;
        }

        Message passwordValidationRequest(string CardID, string password, CardReader cr)
        {
           Card c;
            if (UCIDatabase.cardDict.ContainsKey(CardID))
                c = UCIDatabase.cardDict[CardID];
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException("No such Card.");
            }

            return cr.verifyPassword(c, password);

        }

        TypicalWeek getCalendar(string PersonGroupID,string DoorGroupID)
        {
            string typicalWeekKey = PersonGroupID + DoorGroupID;
            TypicalWeek tw;

            if (UCIDatabase.typicalWeekDict.ContainsKey(typicalWeekKey))
                tw = UCIDatabase.typicalWeekDict[typicalWeekKey];
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException("Can't find Door Group or Person Group.");
            }

            return tw;

        }

        public void DoorUnlockRequest(string cardID, string cardReaderID, string password, DateTime datetime,
            Label ControlDisplay, Label DoorStatusDisplay, Label TimeKeeperDisplay)
        {

            CardReader cr = this.findCardReader(cardReaderID);
            IEnumerable<PersonGroup> people = this.findPersonGroupRequest(cardID);
            cr.lockDoor();
            cr.stopAlarmTimer();
            cr.stopTimeKeeper();

            DoorStatusDisplay.Text = "Locked";

            if (!UCIDatabase.cardDict[cardID].isEnabled)
            {
                ControlDisplay.Text = "Card disabled...";
                return;
            }

            //door in standby?
            if (cr.activityMode() == false)
            {
                ControlDisplay.Text = "Standby Mode...Door Inaccessible";
                return;
            }
            //verify password
            Message m = this.passwordValidationRequest(cardID, password, cr);
            if (m == Message.PASSWORD_INVALID)
            {
                ControlDisplay.Text = "Password Invalid " + cr.numRetriesLeft(cardID).ToString() + " retries left.";
                return;
            }
            else if (m == Message.MAX_ATTEMPTS_EXCEEDED)
            {
                ControlDisplay.Text = "Max attempts exceeded, please wait for security staff to apprehend you.";
                cr.setStandby();
                UCIDatabase.cardDict[cardID].isEnabled = false;
                return;
            }
            else
            {
                foreach (DoorGroup dg in cr.door.groups)
                {
                    foreach (PersonGroup pg in people)
                    {
                        TypicalWeek tw = this.getCalendar(pg.PersonGroupID, dg.DoorGroupID);
                        if (tw.accessible(datetime))
                        {
                            cr.unlockDoor();
                            cr.startTimeKeeper(new TimeKeeperController(cr,TimeKeeperDisplay,DoorStatusDisplay,ControlDisplay));
                            ControlDisplay.Text = "Access Granted";
                            DoorStatusDisplay.Text = "Unlocked";
                            return;
                        }

                    }

                }
            }
            ControlDisplay.Text = "Invalid Access Privileges";
        }
        

        void DoorLockRequest(string cardReaderID)
        {
            CardReader cr = this.findCardReader(cardReaderID);
            cr.lockDoor();
            cr.stopTimeKeeper();
        }

       public void DoorOpenRequest(string cardReadID,Label AlarmDisplay,Label DoorStatusDisplay)
        {
            CardReader cr = this.findCardReader(cardReadID);
            Message m = cr.tryOpenDoor();
            if (m == Message.DOOR_OPEN)
            {
                DoorStatusDisplay.Text = "Opened";
                TimeKeeperController tk = (TimeKeeperController) cr.TKController;
                tk.killTimer();
                cr.startAlarmTimer(new AlarmTimerController(cr,AlarmDisplay,DoorStatusDisplay));
            }
        }

        public void DoorCloseRequest(string cardReaderID, Label DoorStatusDisplay, Label ControlPanelDisplay)
        {
            CardReader cr = this.findCardReader(cardReaderID);
            if (cr.door.locked) return; //can't close a locked door
            AlarmTimerController at = (AlarmTimerController)cr.AController;
            if(at != null)
                at.killTimer();
            cr.lockDoor();
            cr.closeDoor();
            DoorStatusDisplay.Text = "Locked";
            if (cr.activityMode())
                ControlPanelDisplay.Text = "Please slide card.";
            else
                ControlPanelDisplay.Text = "Standby Mode...";
        }

    }
}