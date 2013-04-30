using System;
using System.Collections.Generic;

namespace UCI
{
    public static class UCIDatabase
    {
        
        public static Dictionary<string, Card> cardDict = new Dictionary<string, Card>();
        public static Dictionary<string, PersonGroup> personGroupDict = new Dictionary<string, PersonGroup>();
        public static Dictionary<string, DoorGroup> doorGroupDict = new Dictionary<string, DoorGroup>();
        public static Dictionary<string, CardReader> cardReadersDict = new Dictionary<string, CardReader>();
        public static Dictionary<string, TypicalWeek> typicalWeekDict = new Dictionary<string, TypicalWeek>();

        

        // initalize system called upon application creation
        public static void InitalizeSystem()
        {
            //initalize some groups
            UCIDatabase.doorGroupDict["Lab"]  = new DoorGroup("Lab","Lab");
            UCIDatabase.doorGroupDict["Maintainance"] = new DoorGroup("Maintainance","Maintainance"); 
            UCIDatabase.doorGroupDict["Study"] = new DoorGroup("Study","Study");

            UCIDatabase.personGroupDict["Researchers"] = new PersonGroup("Researchers");
            UCIDatabase.personGroupDict["Janitors"] = new PersonGroup("Janitors");
            UCIDatabase.personGroupDict["Students"] = new PersonGroup("Students");

            ///initalize some people
      
            ///Bob Jones 0000 Janitor
            UCIDatabase.cardDict["0000"] = new Card("0000", "Bob", "Jones");
            UCIDatabase.cardDict["0000"].person.Password = "0000";
            UCIDatabase.personGroupDict["Janitors"].persons.AddFirst(UCIDatabase.cardDict["0000"].person);
            UCIDatabase.cardDict["0000"].person.groups.AddFirst(UCIDatabase.personGroupDict["Janitors"]);

            ///Rick Albertson 0001 Student
            UCIDatabase.cardDict["0001"] = new Card("0001", "Rick", "Albertson");
            UCIDatabase.cardDict["0001"].person.Password = "0001";
            UCIDatabase.personGroupDict["Students"].persons.AddFirst(UCIDatabase.cardDict["0001"].person);
            UCIDatabase.cardDict["0001"].person.groups.AddFirst(UCIDatabase.personGroupDict["Students"]);

            ///Steve Smith 0002 Researcher & Student
            UCIDatabase.cardDict["0002"] = new Card("0002", "Steve", "Smith");
            UCIDatabase.cardDict["0002"].person.Password = "0002";
            UCIDatabase.personGroupDict["Researchers"].persons.AddFirst(UCIDatabase.cardDict["0002"].person);
            UCIDatabase.cardDict["0002"].person.groups.AddFirst(UCIDatabase.personGroupDict["Researchers"]);
            UCIDatabase.personGroupDict["Students"].persons.AddFirst(UCIDatabase.cardDict["0002"].person);
            UCIDatabase.cardDict["0002"].person.groups.AddFirst(UCIDatabase.personGroupDict["Students"]);


            //Intialize some doors

            ///BroomCloset1 Maintainance
            UCIDatabase.cardReadersDict["BroomCloset1"] = new CardReader("BroomCloset1");
            UCIDatabase.cardReadersDict["BroomCloset1"].door.groups.AddFirst(UCIDatabase.doorGroupDict["Maintainance"]);
            UCIDatabase.doorGroupDict["Maintainance"].doors.AddFirst(UCIDatabase.cardReadersDict["BroomCloset1"].door);


            //ParticleAccelorator1 Lab
            UCIDatabase.cardReadersDict["ParticleAccelorator1"] = new CardReader("ParticleAccelorator1");
            UCIDatabase.cardReadersDict["ParticleAccelorator1"].door.groups.AddFirst(UCIDatabase.doorGroupDict["Lab"]);
            UCIDatabase.doorGroupDict["Lab"].doors.AddFirst(UCIDatabase.cardReadersDict["ParticleAccelorator1"].door);

            //IntroPhysicsLab1 Lab & Study
            UCIDatabase.cardReadersDict["IntroPhysicsLab1"] = new CardReader("IntroPhysicsLab1");
            UCIDatabase.cardReadersDict["IntroPhysicsLab1"].door.groups.AddFirst(UCIDatabase.doorGroupDict["Lab"]);
            UCIDatabase.doorGroupDict["Lab"].doors.AddFirst(UCIDatabase.cardReadersDict["IntroPhysicsLab1"].door);
            UCIDatabase.cardReadersDict["IntroPhysicsLab1"].door.groups.AddFirst(UCIDatabase.doorGroupDict["Study"]);
            UCIDatabase.doorGroupDict["Study"].doors.AddFirst(UCIDatabase.cardReadersDict["IntroPhysicsLab1"].door);



            //set up associated calendars
            foreach (DoorGroup dg in UCIDatabase.doorGroupDict.Values)
            {
                foreach (PersonGroup pg in UCIDatabase.personGroupDict.Values)
                {
                    UCIDatabase.typicalWeekDict[pg.PersonGroupID + dg.DoorGroupID] = new TypicalWeek();
                }
            }

            //default calendar enabled for testing
            UCIDatabase.typicalWeekDict["Researchers" + "Lab"].grantAccess(DayOfWeek.Sunday, 0);



        }
    }
}