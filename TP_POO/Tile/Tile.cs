using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public interface Tile
    {
         TileType getType();
         bool Equals(Tile t);
    }
}
