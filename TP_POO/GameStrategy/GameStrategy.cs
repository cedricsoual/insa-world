using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public abstract class GameStrategy
    {
        protected Game game;
        protected IDictionary<string, UnitType> playersDetails;
        
        public abstract void buildGame();


        public GameStrategy(){
            
            playersDetails = new Dictionary<string, UnitType>();
        }

        public void addPlayer(String name, UnitType race)
        {
            playersDetails.Add(name, race);
        }

        public void saveGameInit()
        {
            GameManager.INSTANCE.Replay.GameInit = CloneUtil.Clone(game);
        }
    
    }
}
