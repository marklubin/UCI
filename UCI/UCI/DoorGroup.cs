using System;
using System.Collections.Generic;

namespace UCI
{
    public class DoorGroup
    {
        public string DoorGroupID;
        public String Description;
        public LinkedList<Door> doors = new LinkedList<Door>();
        public DoorGroup(string DoorGroupID,string Description)
        {
            this.Description = Description;
            this.DoorGroupID = DoorGroupID;
        }
    }
}