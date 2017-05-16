using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class Cyclops : Unit
    {
        public Cyclops(Position pos, int id) : base(id)
        {
            Attack = 4;
            Defense = 6;
            MaxHealth = 12;
            Health = 10;
            Move = 3;
            Position = pos;
        }

        public override UnitType getRace()
        {
            return UnitType.CYCLOP;
        }


        public override int computeVictoryPoint(Tile tile)
        {
            switch (tile.getType())
            {
                case TileType.PLAIN:
                    return 2;
                case TileType.DESERT:
                    return 3;
                case TileType.SWAMP:
                    return 0;
                case TileType.VOLCANO:
                    return 1;
                default:
                    return 0;
            }
        }
    }
}
