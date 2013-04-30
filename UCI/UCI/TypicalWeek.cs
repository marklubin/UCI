using System;
namespace UCI
{
    public class TypicalWeek
    {
        private Boolean[,] hasAccess = new Boolean[7,24];


        public Boolean getAccessStatus(DayOfWeek d, int h)
        {
            int day = (int)d;
            return hasAccess[day, h];
        }

        public void grantAccess(DayOfWeek day, int hour){
            int d = (int) day;
            if(hour < 0 || hour >= 24){
                throw new Exception("Invalid hour selection");
            }
            this.hasAccess[d,hour] = true;
        }

        public void revokeAccess(DayOfWeek day, int hour)
        {
            int d = (int)day;
            if (hour < 0 || hour >= 24)
            {
                throw new Exception("Invalid hour selection");
            }
            this.hasAccess[d,hour] = false;

        }

        public Boolean accessible(DateTime datetime)
        {
            int day = (int)datetime.DayOfWeek;
            return this.hasAccess[day,datetime.Hour];
        }
    }
}
