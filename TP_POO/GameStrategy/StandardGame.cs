using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public class StandardGame : GameStrategy
    {
        public StandardGame(Game game) : base()
        {
            this.game = game;
        }
    
        public override void buildGame()
        {
            game.createPlayers(playersDetails);
            game.createMap(14);
            game.createUnits(8);
            game.MaxTurn = 30;
            game.UnitCount = 8;
            saveGameInit();
        }
    }
}
