using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class AttackCommand : ReplayCommand
    {
        public int IdDefender { get; set; }
        public int IdAttacker { get; set; }
        public bool ResultFight { get; private set; }
        public int NbHpLost { get; private set; }
        public float NbPtMove { get; set; }
        public int CurrentPlayer { get; set; }

        public AttackCommand(int IdAttacker, int IdDefender, float nbPtMove)
        {
            this.IdAttacker = IdAttacker;
            this.IdDefender = IdDefender;
            this.NbPtMove = nbPtMove;
            this.CurrentPlayer = GameManager.INSTANCE.CurrentGame.CurrentPlayer;
        }
    
        // replayMode sert à définir si l'action doit être exéctuée en jeu ou lors d'un replay (pour ne pas refaire les calculs aléatoires)
        public override void action(bool replayMode)
        {
            Game gameContext = GameManager.INSTANCE.CurrentGame;
            Unit loser;
            Unit Attacker = null;
            Unit Defender = null;

            // On récupère les référence vers Attacker et Defender grâce à leur Id
            foreach(var kvp in gameContext.UnitsDictionary){
                if (CurrentPlayer == kvp.Key) Attacker = kvp.Value.Find(u => u.IdUnit == IdAttacker);
                else Defender = kvp.Value.Find(u => u.IdUnit == IdDefender);
            }

            if (replayMode)
            {
                gameContext.CurrentPlayer = CurrentPlayer;
                gameContext.SelectedUnit = Attacker;
            }

            Attacker.Move -= NbPtMove;
            // On isole les résultats susceptibles de changer d'une exécution à une autre
            // => résultats aléatoires
            // On souhaite que ces opérations ne soient réaliser que lors du déroulement du jeu
            // et non en mode replay 
            if (!replayMode)
            {
                // Retourne le résultat de l'attaque
                ResultFight = Attacker.attackUnit(Defender);
                // Définit le perdant du combat
                loser = ResultFight ? Defender : Attacker;
                NbHpLost = loser.computeLoseHealth();
            }
            else
            {
                loser = ResultFight ? Defender : Attacker;
            }
            
            Position posDefender = Defender.Position;
           
            // Récupère le nombre de points perdu et supprime l'unité si son nombre de points de vie est nul
            // L'attaquant remporte le combat
            if(ResultFight){
                loser.Health -= NbHpLost;

                // Si l'unité ayant perdu le combat n'a plus de points de vie, on la supprime du jeu
                if(loser.Health == 0) gameContext.deleteUnit(3-CurrentPlayer, loser);

                // Vérifie si la case attaquée est libre dans le cas où l'attaquant a gagné la partie, si oui on déplace l'unité
                List<Unit> unitsListDefender;
                gameContext.UnitsDictionary.TryGetValue(3-CurrentPlayer, out unitsListDefender);
                List<Unit> unitsListAtPos ;
                unitsListAtPos = unitsListDefender.FindAll(u => u.Position.Equals(posDefender));
                if (unitsListAtPos.Count == 0)
                   gameContext.moveUnit(posDefender);
            }
            // Le défenseur remporte le combat
            else{
                loser.Health -= NbHpLost;
                if (loser.Health == 0)
                {
                    // Si l'attaquant meurt on le supprime de la liste des unités
                    gameContext.deleteUnit(CurrentPlayer, loser);
                    // Si l'attaquant meurt on passe à l'unité suivante
                    gameContext.nextUnit();
                }
            }

        }

        public bool Equals(AttackCommand  ac)
        {
            // If parameter is null return false:
            if ((object)ac == null)
            {
                return false;
            }

            if (IdDefender != ac.IdDefender) return false;
            if (IdAttacker != ac.IdAttacker) return false;
            if (ResultFight != ac.ResultFight) return false;
            if (NbHpLost != ac.NbHpLost) return false;
            if (NbPtMove != ac.NbPtMove) return false;
            if (CurrentPlayer != ac.CurrentPlayer) return false;

            // Return true if the fields match:
            return true;
        }
       
    }
}
