using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class EndTurnCommand : ReplayCommand
    {
        
        public bool IsLastTurn{ get; private set; }
        public int NbTurn { get; set; }
    
        public EndTurnCommand(bool isLastTurn)
        {
            IsLastTurn = isLastTurn;
            NbTurn = GameManager.INSTANCE.CurrentGame.CurrentTurn;
        }

        public override void action(bool replayMode)
        {
            Game game = GameManager.INSTANCE.CurrentGame;
            // Met à jour les victory points de chaque player
            game.computeVictoryPoints();
            if (IsLastTurn)
            {
                // La partie est terminée
                game.IsGameCompleted = true;
                return;
            }
            // Passe au tour suivant
            game.CurrentTurn++;
            // Regénère les points de move à chaque début de tour 
            // Régénère de un point de vie si l'unité n'a pas fait d'actions durant le tour
            foreach (var kvp in game.UnitsDictionary)
            {
                foreach (Unit u in kvp.Value)
                {
                    // Regagne un point de vie s'il a rien fait durant le tour
                    if (u.Move == 3 && u.Health < u.MaxHealth) u.Health++;
                    // Regagne ses points de move
                    u.Move = 3;
                }
            }
        }

        public bool Equals(EndTurnCommand etc)
        {
            // If parameter is null return false:
            if ((object)etc == null)
            {
                return false;
            }

            if (IsLastTurn != etc.IsLastTurn) return false;
            if (NbTurn != etc.NbTurn) return false;
            
            // Return true if the fields match:
            return true;
        }
       
    }
}
