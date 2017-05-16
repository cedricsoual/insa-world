using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public class SmallGame : GameStrategy
    {
       
        public SmallGame(Game game) : base()
        {
            this.game = game;
        }

        
        public override void buildGame()
        {
            game.createPlayers(playersDetails);
            game.createMap(10);
            game.createUnits(6);
            game.MaxTurn = 20;
            game.UnitCount = 6;
            saveGameInit();
        }
    }
}
