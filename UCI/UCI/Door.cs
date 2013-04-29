using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCI
{


    public class idglobal2
    {
        public static int num = 0;
    }

    class Door
    {

        public int ID;
        public string Group;




         public Door(int id,string grp){

             ID = id;
             Group = grp;

         }


         public String IDtoString()
         {

             String buff;


             buff = ID.ToString();

             return buff;
         }


         public String GrouptoString()
         {

             String buff;

             buff = Group;


             return buff;
         }
    }
}
