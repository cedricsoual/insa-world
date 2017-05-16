using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class Centaurs : Unit
    {
        public Centaurs(Position pos, int id) : base(id)
        {
            Attack = 8;
            Defense = 2;
            MaxHealth = 10;
            Health = 10;
            Move = 3;
            Position = pos;
        }

        public override UnitType getRace()
        {
            return UnitType.CENTAUR;
        }

        public override float computeMovePoint(Tile tileType)
        {
            if(tileType.getType() == TileType.PLAIN) return (float)0.5;
            return 1;
        }

        public override int computeVictoryPoint(Tile tile)
        {
            switch (tile.getType())
            {
                case TileType.PLAIN:
                    return 3;
                case TileType.DESERT:
                    return 2;
                case TileType.SWAMP:
                    return 1;
                case TileType.VOLCANO:
                    return 0;
                default:
                    return 0;
            }
        }
    }
}
