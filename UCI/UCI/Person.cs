using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCI
{
    public class idglobal
    {
        public static int num = 0;
    }

    public class Person
    {
        public int ID;
        public string FirstName;
        public string LastName;
        public string Group;
        public string Password;

        public Person(int id, string fn, string ln, string grp, string pw)
        {

            ID = id;
            FirstName = fn;
            LastName = ln;
            Group = grp;
            Password = pw;

        }

        public void ValidatePassword() { 
               
        }

        public String NametoString()
        {

            String buff;

            buff = FirstName + " " + LastName;


            return buff;
        }

        public String GrouptoString()
        {

            String buff;

            buff = Group;


            return buff;
        }



        public String IDtoString()
        {

            String buff;


            buff = ID.ToString();

            return buff;
        }



    }
}
