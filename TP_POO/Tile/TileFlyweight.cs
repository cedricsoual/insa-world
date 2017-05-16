using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TP_POO
{
    public class TileFlyweight
    {
        private IDictionary<TileType, Tile> tiles;

        public TileFlyweight()
        {
            tiles = new Dictionary<TileType, Tile>();
        }
        
        // Retourne une tuile existante, la créé sinon
        public Tile getVolcanoTile()
        {
            Tile t;
            if(!tiles.TryGetValue(TileType.VOLCANO, out t)){
                t = new Volcano();
                tiles.Add(TileType.VOLCANO, t);
            }
            return t;
        }

        public Tile getPlainTile()
        {
            Tile t;
            if (!tiles.TryGetValue(TileType.PLAIN, out t))
            {
                t = new Plain();
                tiles.Add(TileType.PLAIN, t);
            }
            return t;
        }

        public Tile getSwampTile()
        {
            Tile t;
            if (!tiles.TryGetValue(TileType.SWAMP, out t))
            {
                t = new Swamp();
                tiles.Add(TileType.SWAMP, t);
            }
            return t;
        }

        public Tile getDesertTile()
        {
            Tile t;
            if (!tiles.TryGetValue(TileType.DESERT, out t))
            {
                t = new Desert();
                tiles.Add(TileType.DESERT, t);
            }
            return t;
        }
    }
}
