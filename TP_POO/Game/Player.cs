using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class Player 
    {

        public String Name { get; set; } 
        public UnitType Race {get; set; }
        public int Points { get; set; }
        public int IdPlayer { get; set; }

       
        public Player(String name, UnitType race, int idPlayer)
        {
            Name = name;
            Race = race;
            Points = 0;
            IdPlayer = idPlayer;
        }

  
        public bool Equals(Player p)
        {
            // If parameter is null return false:
            if ((object)p == null)
            {
                return false;
            }
            if (Name != p.Name) return false;
            if (Race != p.Race) return false;
            if (Points != p.Points) return false;
            if (IdPlayer != p.IdPlayer) return false;

            // Return true if the fields match:
            return true;
        }
    }
}
