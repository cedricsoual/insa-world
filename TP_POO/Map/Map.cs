using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace TP_POO
{
    [Serializable]
    public class Map 
    {
        private int size;
        public int Width { get; set; }
        public TP_POO.Tile[,] TilesTab
        {
            get;
            set;
        }

        public Map(int width)
        {
            this.Width = width;
            size = width * width;
            TilesTab = new Tile[width,width];
        }

        // Retourne le type de Tile à une position donnée
        public Tile getTileAt(Position p)
        {
            // Retourne null si la position demandée est en dehors de la Map
            if (!(-1 < p.X && p.X < Width && -1 < p.Y && p.Y < Width)) return null;
            return TilesTab[p.X,p.Y];
        }

        public void createTiles()
        {
            TileFlyweight tileFw = new TileFlyweight();
            // Recupere un tab de int avec le type de chaque case
            var tiles = new TileType[size];

            IntPtr algo = ApiUtil.MapAlgo_new();
            // L'algo dipose les différents types de Tile
            ApiUtil.MapAlgo_fillMap(algo, tiles, size);

            // Remplissage de la map avec le résultat de l'algo
            for (int i = 0; i < size; i++)
            {
                int x = i / Width;
                int y = i % Width;
                switch(tiles[i]){
                    case TileType.PLAIN :
                        TilesTab[x,y] = tileFw.getPlainTile();
                        break;
                    case TileType.VOLCANO :
                        TilesTab[x, y] = tileFw.getVolcanoTile();
                        break;
                    case TileType.SWAMP :
                        TilesTab[x, y] = tileFw.getSwampTile();
                        break;
                    case TileType.DESERT :
                        TilesTab[x, y] = tileFw.getDesertTile();
                        break;
                    default :
                        break;
                }
            }
            ApiUtil.MapAlgo_delete(algo);
        }

        public bool Equals(Map m)
        {
            // If parameter is null return false:
            if ((object)m == null)
            {
                return false;
            }

            // Return true if the fields match:
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (!TilesTab[i, j].Equals(m.TilesTab[i, j])) return false;
                }
            }
            return true;
        }
    }
}
