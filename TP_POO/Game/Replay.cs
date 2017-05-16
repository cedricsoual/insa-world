using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
     public class Replay
    {
        public List<ReplayCommand> CommandList {get; set;}
      
        public Game GameInit { get; set; }

        public Replay()
        {
            CommandList = new List<ReplayCommand>();
        }

        public void storeAction(ReplayCommand cmd)
        {
            CommandList.Add(cmd);
        }

        public void replayGame()
        {
            foreach (ReplayCommand rc in CommandList)
            {
                rc.replay();
            }
        }

        public bool Equals(Replay r)
        {
            // If parameter is null return false:
            if ((object)r == null)
            {
                return false;
            }

            if (!GameInit.Equals(r.GameInit)) return false;
            if (!CompareUtil.compareListCommand(CommandList, r.CommandList)) return false;

            // Return true if the fields match:
            return true;
        } 
       
    }
}
