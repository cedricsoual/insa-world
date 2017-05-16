using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public class DemoGame : GameStrategy
    {
        
        public DemoGame(Game game) : base()
        {
            this.game = game;
        }

    
        public override void buildGame()
        {
            game.createPlayers(playersDetails);
            game.createMap(6);
            game.createUnits(4);
            game.MaxTurn = 5;
            game.UnitCount = 4;
            saveGameInit();
        }
    }
}
