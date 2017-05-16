using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices;
using TP_POO;

namespace TP_POO.tests
{
    [TestClass]
    public class TestMap
    {
        TP_POO.Tile[,] tiles;
        int i;
        [TestInitialize]
        public void init()
        {
            Map map = new Map(4);
            map.createTiles();
            tiles = map.TilesTab;
            i = 0;
        }

        [TestMethod]
        public void Map_SizeMap_Correct()
        {
            Assert.AreEqual(16, tiles.Length,"Taille invalide");
        }

        [TestMethod]
        public void Map_TileContentMap()
        {
            i = 1;
            Assert.IsInstanceOfType(tiles[i,i],typeof(Tile));
        }

    
    }
}


