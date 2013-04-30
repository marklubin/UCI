using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace UCI
{
    public class CalendarUpdateController
    {
        public void fillCalendarSelect(ComboBox personGroups, ComboBox doorGroups){
            personGroups.Items.Clear();
            doorGroups.Items.Clear();
            foreach (PersonGroup pg in UCIDatabase.personGroupDict.Values)
            {
                personGroups.Items.Add(pg.PersonGroupID);
            }

            foreach (DoorGroup dg in UCIDatabase.doorGroupDict.Values)
            {
                doorGroups.Items.Add(dg.DoorGroupID);
            }
        }
        public TypicalWeek getTypicalWeek(string personGroupID, string doorGroupID)
        {
            TypicalWeek tw;
                  
            string typicalWeekKey = personGroupID + doorGroupID;

            if (UCIDatabase.typicalWeekDict.ContainsKey(typicalWeekKey))
                tw = UCIDatabase.typicalWeekDict[typicalWeekKey];
            else
            {
                throw new System.Collections.Generic.KeyNotFoundException("Can't find Door Group or Person Group.");
            }

            return tw;

        }
        public void getCalendarStatusForDay(string personGroupID, string doorGroupID, DayOfWeek day,CheckedListBox box)
        {
            Boolean[] hours = new Boolean[24];
            string twKey = personGroupID + doorGroupID;
            TypicalWeek tw = getTypicalWeek(personGroupID, doorGroupID);
            box.Items.Clear();

            for (int i = 0; i < 24; i++)
            {
                box.Items.Add(i.ToString() + ":00");
                box.SetItemChecked(i, tw.getAccessStatus(day,i));
            }
        }

        public void setAccessStatus(string personGroupID, string doorGroupID, DayOfWeek day, Boolean[] status)
        {
            TypicalWeek tw  = getTypicalWeek(personGroupID,doorGroupID);
            for (int i = 0; i < 24; i++)
            {
                if (status[i])
                    tw.grantAccess(day, i);
                else
                    tw.revokeAccess(day, i);
            }
        }
    }
}
