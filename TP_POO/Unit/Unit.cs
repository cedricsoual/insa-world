using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public abstract class Unit 
    {
        public int Health { get; set; }
        public float Move { get; set; }
        public int Defense { get; protected set; }
        public int Attack { get; protected set; }
        public Position Position { get; set; }
        public int MaxHealth { get; protected set; }
        public int IdUnit { get; private set; }
        public Unit(int id)
        {
            IdUnit = id;
        }

        public abstract int computeVictoryPoint(Tile tile);
        public virtual float computeMovePoint(Tile tileType)
        {
            return 1;
        }

        public abstract UnitType getRace();

        // Retourne true si l'attaquant remporte le combat, false si le défenseur le remporte
        public bool attackUnit(Unit def)
        {
            Random rdm = new Random();
            // Génère un nombre aléatoire entre 1 et 10
            int pt = rdm.Next(1, 10 + 1);

            // L'attaquant a 70% de chances de gagner un combat
            if (pt < 8)
                return true;
            
            return false;
        }

        public void moveUnit(Position pos, float cost)
        {
            Position = pos;
            this.Move -= cost;
        }

        // Retourne le nombre de points de vie perdu lors du combat 
        public int computeLoseHealth()
        {
            Random rdm = new Random();
            // Génère un nombre aléatoire entre 1 et le nombre de points de vie restants (Health)
            int pt = rdm.Next(1, Health + 1);
            return pt;
        }

        public bool Equals(Unit u)
        {
            // If parameter is null return false:
            if ((object)u == null)
            {
                return false;
            }

            // Return true if the fields match:
            if (Health != u.Health) return false;
            if (Move != u.Move) return false;
            if (Defense != u.Defense) return false;
            if (Attack != u.Attack) return false;
            if (!Position.Equals(u.Position)) return false;
            if (MaxHealth != u.MaxHealth) return false;
            if (IdUnit != u.IdUnit) return false;

            return true;
        }

    }
}
