using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP_POO
{
    public static class ConvertUtil
    {
        // Convertit un entier en une position dans la map 
        public static Position intToPosition(int pos, int width)
        {
            int x = pos / width;
            int y = pos % width;
            return new Position(x, y);
        }
        // Convertit une position dans la map en un entier  
        public static int positionToInt(Position pos, int width)
        {
            return pos.X*width + pos.Y;
        }
    }
}
