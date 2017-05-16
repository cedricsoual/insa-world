using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    [Serializable]
    public class Cerberus : Unit
    {
        public Cerberus(Position pos, int id) : base(id)
        {
            Attack = 6;
            Defense = 4;
            MaxHealth = 10;
            Health = 10;
            Move = 3;
            Position = pos;
        }

        public override UnitType getRace()
        {
            return UnitType.CERBERUS;
        }

        public override int computeVictoryPoint(Tile tileType)
        {
            switch (tileType.getType())
            {
                case TileType.PLAIN:
                    return 0;
                case TileType.DESERT:
                    return 1;
                case TileType.SWAMP:
                    return 2;
                case TileType.VOLCANO:
                    return 3;
                default:
                    return 0;
            }
        }

     
    }
}
