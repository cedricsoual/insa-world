using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    public static class CompareUtil
    {
        // Compare les listes d'unités contenus dans deux dictionnary 
        public static bool compareDictionaryUnits(IDictionary<int, List<Unit>> idd1, IDictionary<int, List<Unit>> idd2)
        {
            if (idd1.Count != idd2.Count) return false;
            foreach (var pair in idd1)
            {
                List<Unit> unitList2;
                if (idd2.TryGetValue(pair.Key, out unitList2))
                {
                    List<Unit> unitList1 = pair.Value;
                    if (unitList1.Count != unitList2.Count) return false;
                    // Require lists be equal.
                    foreach (Unit u1 in unitList1)
                    {
                        Unit u2 = unitList2.Find(u => u.IdUnit == u1.IdUnit);
                        if (u2 == null) return false;
                        if (!u1.Equals(u2)) return false;
                    }
                }
                else
                    // La value doit être présente
                    return false;
            }
            return true;
        }

        // Compare les Players contenues dans deux dictionnary
        public static bool commpareDictionaryPlayer(IDictionary<int, Player> idd1, IDictionary<int, Player> idd2){
            if (idd1.Count != idd2.Count) return false;
            foreach (var pair in idd1)
            {
                Player value;
                if (idd2.TryGetValue(pair.Key, out value))
                {
                    // Require value be equal.
                    if (!value.Equals(pair.Value))
                        return false;
                }
                else
                    return false;
            }
            return true;
        }

        // Compare deux listes de ReplayCommand
        public static bool compareListCommand(List<ReplayCommand> l1, List<ReplayCommand> l2)
        {
            if (l1.Count != l2.Count) return false;
             // Require lists be equal.
            int i = 0;
             foreach (ReplayCommand c1 in l1)
             {
                 if (!l2[i].Equals(c1)) return false;
              }
              return true;
          }
    }
}
