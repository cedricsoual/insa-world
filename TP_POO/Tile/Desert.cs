using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class Desert : Tile
    {
        public Desert()
        {
        }
      
        public TileType getType()
        {
            return TileType.DESERT;
        }

        public bool Equals(Tile t)
        {
            // If parameter is null return false:
            if ((object)t == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (t is Desert);
        }


    }
}
