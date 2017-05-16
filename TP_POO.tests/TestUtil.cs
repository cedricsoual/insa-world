using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TP_POO.tests
{
    [TestClass]
    public class TestUtil
    {
        GameManager gm;
        [TestInitialize]
        public void init()
        {
            gm = GameManager.INSTANCE;
            GameManager.INSTANCE.newGame();
            GameStrategy gs = gm.CurrentGame.initGame(MapSize.DEMO);
            gs.addPlayer("User1", UnitType.CENTAUR);
            gs.addPlayer("User2", UnitType.CERBERUS);
            gs.buildGame();
        }

        [TestMethod]
        public void GameManager_CloneGame()
        {
            Game gameBefore = gm.CurrentGame;
            Game gameAfter = CloneUtil.Clone(gameBefore);
            // On modifie volontairement le game pour vérifier qu'il s'agit bien d'une copie
            gameBefore.nextUnit();

            Assert.IsFalse(gameBefore.Equals(gameAfter));
        }

        [TestMethod]
        public void GameManager_CompareDictionary()
        {
            Game gameBefore = gm.CurrentGame;
            Game gameInit = gm.Replay.GameInit;

            Assert.IsTrue(CompareUtil.compareDictionaryUnits(gameBefore.UnitsDictionary, gameInit.UnitsDictionary));
            Assert.IsTrue(CompareUtil.commpareDictionaryPlayer(gameBefore.PlayersDictionary, gameInit.PlayersDictionary));
        }

        [TestMethod]
        public void GameManager_ConvertPositionToInt()
        {
            Position p1 = new Position(4,5);
            Position p2 = new Position(9,8);
            Assert.AreEqual(45, ConvertUtil.positionToInt(p1, 10));
            Assert.AreEqual(134, ConvertUtil.positionToInt(p2, 14));
        }

        [TestMethod]
        public void GameManager_ConvertIntToPosition()
        {
            Position pConvert1 = ConvertUtil.intToPosition(74, 10);
            Position pConvert2 = ConvertUtil.intToPosition(59, 14);
            Assert.IsTrue(pConvert1.Equals(new Position(7, 4)));
            Assert.IsTrue(pConvert2.Equals(new Position(4, 3)));
        }
     
    }
}
