using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace TP_POO.tests
{
    [TestClass]
    public class TestGameManager
    {
        private Game game;
        [TestInitialize]
        public void init()
        {
            GameManager.INSTANCE.newGame();
            game = GameManager.INSTANCE.CurrentGame;
            GameStrategy gs = game.initGame(MapSize.DEMO);
            gs.addPlayer("User1", UnitType.CENTAUR);
            gs.addPlayer("User2", UnitType.CERBERUS);
            gs.buildGame();
        }

        // S'assure que la sauvegarde du Game est bien identique une fois rechargée
        [TestMethod]
        public void GameManager_saveGame()
        {
            // On réalise quelques actions avant de sauvegarder le Game pour qu'il ne soit pas dans son état initial
            /* ******** début actions avant de sauvegarder ****** */
            Game gameBefore = GameManager.INSTANCE.CurrentGame;
            game.nextUnit();
            // On place le défenseur sur une case accessible
            List<Unit> unitsListPlayer2;
            gameBefore.UnitsDictionary.TryGetValue(2, out unitsListPlayer2);
            // On recherche l'unité d'id 1
            Unit defender = unitsListPlayer2.Find(u => u.IdUnit.Equals(1));
            defender.Position = new Position(1, 0);
            gameBefore.SelectedUnit.Position = new Position(0, 0);
            gameBefore.attackUnit(new Position(1, 0));
            gameBefore.nextUnit();
            gameBefore.moveUnit(new Position(1, 0));
            /* ******** fin actions avant de sauvegarder ****** */

            GameManager.INSTANCE.saveGame("savegame");
            GameManager.INSTANCE.CurrentGame = null;
            GameManager.INSTANCE.loadGame("savegame");

            Game gameAfter = GameManager.INSTANCE.CurrentGame;

            Assert.IsTrue(gameBefore.Equals(gameAfter));
        }

      
    }
}
