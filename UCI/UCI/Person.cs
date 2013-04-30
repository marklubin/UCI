using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCI
{
    public class Person
    {
        public string PersonID;
        public string FirstName;
        public string LastName;
        public string Profile;
        public LinkedList<PersonGroup> groups = new LinkedList<PersonGroup>();
        public string Password;

        public Person(string PersonID, string fn, string ln)
        {

            this.PersonID = PersonID;
            FirstName = fn;
            LastName = ln;

        }




    }
}
