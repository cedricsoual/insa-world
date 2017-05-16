using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class MoveCommand : ReplayCommand
    {
        public int IdUnit{ get; private set; }
        public Position NewPosition { get; private set; }
        public float NbPtMove { get; set; }
        public int CurrentPlayer { get; set; }

        public MoveCommand(int unitId, Position pos, float nbPtMove) : base()
        {
            this.IdUnit = unitId;
            this.NewPosition = new Position(pos.X, pos.Y);
            this.NbPtMove = nbPtMove;
            this.CurrentPlayer = GameManager.INSTANCE.CurrentGame.CurrentPlayer;
        }
    
        public override void action(bool replayMode)
        {
            Game game = GameManager.INSTANCE.CurrentGame;
            
            List<Unit> unitList;
            game.UnitsDictionary.TryGetValue(CurrentPlayer, out unitList);
            Unit unit = unitList.Find(u => u.IdUnit == IdUnit);
            if (replayMode)
            {
                game.CurrentPlayer = CurrentPlayer;
                game.SelectedUnit = unit;
            }
            // On déplace l'unité sur sa nouvelle case
            unit.moveUnit(NewPosition, NbPtMove);
        }
        public bool Equals(MoveCommand mc)
        {
            // If parameter is null return false:
            if ((object)mc == null)
            {
                return false;
            }

            if (IdUnit != mc.IdUnit) return false;
            if (NbPtMove != mc.NbPtMove) return false;
            if (NewPosition.Equals(mc.NewPosition)) return false;

            // Return true if the fields match:
            return true;
        }
       
    }
}
