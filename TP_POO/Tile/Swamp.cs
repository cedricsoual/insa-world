using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class Swamp : Tile
    {
    
        public Swamp()
        {
        }

     

        public TileType getType()
        {
            return TileType.SWAMP;
        }

        public bool Equals(Tile t)
        {
            // If parameter is null return false:
            if ((object)t == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (t is Swamp);
        }


    }
}
