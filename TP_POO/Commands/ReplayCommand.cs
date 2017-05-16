using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public abstract class ReplayCommand
    {
        public ReplayCommand()
        {
        }

        public void execute()
        {
            action(false);
            GameManager.INSTANCE.Replay.storeAction(this);
        }

        public void replay()
        {
            action(true);
        }

        public abstract void action(bool replayMode);

    }
}
