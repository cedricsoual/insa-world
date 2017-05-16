using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public class UnitFactory
    {

        public UnitFactory()
        {
        }

        public Unit createUnit(Position pos, UnitType race, int id)
        {
            switch (race)
            {
                case UnitType.CERBERUS :
                    return new Cerberus(pos, id);
                case UnitType.CENTAUR :
                    return new Centaurs(pos, id);
                case UnitType.CYCLOP :
                    return new Cyclops(pos, id);
                default :
                    return null;
            }
        }

    }
}
