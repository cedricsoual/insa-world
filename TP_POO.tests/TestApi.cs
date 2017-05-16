using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TP_POO.tests
{
    [TestClass]
    public class TestApi
    {

        // Teste l'algorithme qui place les joueurs
        [TestMethod]
        public void Api_PlacePlayers()
        {
            var posPlayers = new int[2];
            IntPtr algo = ApiUtil.MapAlgo_new();
            ApiUtil.MapAlgo_placePlayers(algo, 10, posPlayers);

            Position positionDepartP1 = new Position(posPlayers[0] / 10, posPlayers[0] % 10);
            Position positionDepartP2 = new Position(posPlayers[1] / 10, posPlayers[1] % 10);
            
            // Vérifie que les players sont bien placés à l'intérieur de la map
            Assert.IsTrue(positionDepartP1.X>-1 && positionDepartP1.X<10 && positionDepartP1.Y>-1 && positionDepartP1.Y<10);
            Assert.IsTrue(positionDepartP2.X > -1 && positionDepartP2.X < 10 && positionDepartP2.Y > -1 && positionDepartP2.Y < 10);
        }

        // Teste l'algorithme de suggestion de mouvement
        [TestMethod]
        public void Api_bestMove()
        {

            TileType[] possibleMove = new TileType[5] { TileType.SWAMP, TileType.DESERT, TileType.PLAIN, TileType.VOLCANO, TileType.SWAMP };
            List<Position> bestMove;

            Map map = new Map(4);
            map.createTiles();
            map.TilesTab[1, 1] = new Swamp();
            map.TilesTab[0, 1] = new Desert();
            map.TilesTab[1, 2] = new Plain();
            map.TilesTab[2, 1] = new Volcano();
            map.TilesTab[1, 0] = new Swamp();

            bestMove = ApiUtil.getBestActions(UnitType.CERBERUS, new Position(1,1), map);
            // Vérifie que le meilleur mouvement est bien défini
            Assert.IsTrue(bestMove.Exists(p => p.Equals(new Position(2,1))));
            // Vérifie qu'on trouve bien le bon nombre de mouvement
            Assert.AreEqual(1, bestMove.Count);       
        }
    }
}
