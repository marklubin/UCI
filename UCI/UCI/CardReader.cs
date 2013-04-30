using System;
using System.Collections.Generic;
using System.Timers;

namespace UCI
{
    public class CardReader
    {
        private string NetworkAddress;
        public string CardReaderID { get; set; }
        public LinkedList<string> eventLog { get; set; }
        private Boolean isEnabled;
        public int TIME_KEEPER_MSECS = 500;
        public int ALARM_MSECS = 5;
        private int MAX_RETRIES = 3;
        public object TKController {get; set;}
        public object AController {get; set;}
        private Dictionary<string, int> attemptsPerUserDict = new Dictionary<string,int>();
        public Door door { get; set; }


        public CardReader(string CardReaderID)
        {
            this.CardReaderID = CardReaderID;
            this.door = new Door(CardReaderID);
            this.isEnabled = true;
        }

        public int numRetriesLeft(string CardID)
        {
            
            if(attemptsPerUserDict.ContainsKey(CardID))
            {
                return MAX_RETRIES - attemptsPerUserDict[CardID];
            }
            return MAX_RETRIES;
        }

        public Message verifyPassword(Card card, string password)
        {
            if (false == attemptsPerUserDict.ContainsKey(card.CardID))
            {
                attemptsPerUserDict[card.CardID] = 0;
            }
            if (attemptsPerUserDict[card.CardID] >= 3)
                return Message.MAX_ATTEMPTS_EXCEEDED;

           
            if (card.person.Password == password)
            {
                attemptsPerUserDict[card.CardID] = 0;
                return Message.PASSWORD_VALID;
            }

            attemptsPerUserDict[card.CardID]++;
            return Message.PASSWORD_INVALID;

        }
        public Boolean activityMode()
        {
            return this.isEnabled;
        }

        public void setStandby()
        {
            this.isEnabled = false;
        }

        public void setActive()
        {
            this.isEnabled = true;
        }

        public void lockDoor()
        {
            this.door.locked = true;
        }

        public void unlockDoor()
        {
            this.door.locked = false;
        }

        public Message tryOpenDoor()
        {
            if (this.door.locked == false)
            {
                this.door.opened = true;
                return Message.DOOR_OPEN;
            }
            return Message.DOOR_LOCKED;

        }

        public void closeDoor()
        {
            this.door.opened = false;

        }

        public void startAlarmTimer(object AController)
        {
            this.AController = AController;
        }

        public void stopAlarmTimer()
        {
            this.AController = null;
        }

        public void startTimeKeeper(object TKController)
        {
            this.TKController = TKController;
        }

        public void stopTimeKeeper()
        {
            this.TKController = null;
        }
    }
}