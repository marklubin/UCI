using System;

namespace UCI{
public class Card
    {
        public Person person { get; set; }
        public string CardID { get; set; }
        public DateTime creationDate { get; set; }
        public Boolean isEnabled { get; set; }
        
        public Card(string CardID, string firstName, string lastName)
	    {
            this.CardID = CardID;
            this.person = new Person(CardID,firstName,lastName);
            this.isEnabled = true;
	    }
    }
}
