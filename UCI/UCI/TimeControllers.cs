using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;

namespace UCI
{
    public abstract class TimeController
    {
        protected CardReader cr;
        protected Label TimeDisplay;
        protected Label StatusDisplay;
        protected EventHandler tickHandler;
        protected static Timer t = new Timer();
        protected int ms;
        public TimeController(CardReader cr, Label TimeDisplay, Label StatusDisplay)
        {
            this.cr = cr;
            this.TimeDisplay = TimeDisplay;
            this.StatusDisplay = StatusDisplay;
            this.ms = 0;
            this.tickHandler = new EventHandler(this.onTickHandler);
            t.Tick += tickHandler;
            t.Interval = 1;
            t.Start();

        }

        public virtual void killTimer()
        {
            t.Stop();
            t.Tick -= tickHandler;
            TimeDisplay.Text = "--.--";
            cr.stopTimeKeeper();
        }

        public abstract void onTickHandler(object o, EventArgs e);


    }

    public class TimeKeeperController : TimeController
    {
    
        private Label PanelDisplay;
        public TimeKeeperController(CardReader cr, Label TimeDisplay, Label StatusDisplay, Label PanelDisplay)
            : base(cr, TimeDisplay, StatusDisplay)
        {
            this.PanelDisplay = PanelDisplay;
        }
        public override void onTickHandler(Object o, EventArgs e)
        {
            ms++;
            if (ms > cr.TIME_KEEPER_MSECS)
            {
                killTimer();
                cr.lockDoor();
                StatusDisplay.Text = "Locked";
                PanelDisplay.Text = "Please Slide Card.";


            }
            else
            {
                double msToGo = (cr.TIME_KEEPER_MSECS - ms) / 100.00;
                TimeDisplay.Text = String.Format("{0:00.00}", msToGo);
            }
        }
    }

    public class AlarmTimerController : TimeController
    {
        private SoundPlayer sp;
        private Boolean alarmSounded = false;
        public AlarmTimerController(CardReader cr, Label TimeDisplay, Label StatusDisplay)
            : base(cr, TimeDisplay, StatusDisplay)
        {
            sp = new SoundPlayer(UCI.Properties.Resources.tos_redalert);
        }

        public override void killTimer()
        {
            sp.Stop();
            alarmSounded = false;
            base.killTimer();
        }

        public override void onTickHandler(object o, EventArgs e)
        {
            ms++;
            if (alarmSounded)
            {
                if (ms % 20 == 0)
                    StatusDisplay.Text = "";
                else if (ms % 10 == 0)
                    StatusDisplay.Text = "Close Door!!";
            }
            else if (ms > cr.TIME_KEEPER_MSECS)
            {
                sp.PlayLooping();
                cr.setStandby();
                StatusDisplay.Text = "Close Door!!";
                TimeDisplay.Text = "--.--";
                alarmSounded = true;
            }
            else
            {
                double msToGo = (cr.TIME_KEEPER_MSECS - ms) / 100.00;
                TimeDisplay.Text = String.Format("{0:00.00}", msToGo);
            }

        }
    }

}

        