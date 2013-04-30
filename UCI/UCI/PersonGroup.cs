using System;
using System.Collections.Generic;

namespace UCI
{
    public class PersonGroup
    {

        public string PersonGroupID;
        public string Description;

        public LinkedList<Person> persons = new LinkedList<Person>();


        public PersonGroup(string PersonGroupID)
        {
            this.PersonGroupID = PersonGroupID;
        }
    }
}