using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCI
{


   public class Door
    {

       public String DoorID;
       public LinkedList<DoorGroup> groups = new LinkedList<DoorGroup>();
       public Boolean locked { get; set; }
       public Boolean opened { get; set; }

         public Door(String DoorID){
             this.DoorID = DoorID;
             this.locked = true;
             this.opened = false;
         }

    }
}
