using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace TP_POO.tests
{
    [TestClass]
    public class TestReplay
    {
        private GameManager gm;
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

        // Teste si le MoveCommand éxecute bien le mouvement normalement lors d'un replay (on retombe bien sur le même résultat que pendant la partie)
        [TestMethod]
        public void Replay_MoveCommand()
        {
            MoveCommand c1 = new MoveCommand(3, new Position(0, 1), 1);
            c1.execute();
            MoveCommand c2 = new MoveCommand(3, new Position(0, 2), 1);
            c2.execute();

            // On récupère le game à l'état initial pour comparer par la suite
            gm.CurrentGame = gm.Replay.GameInit;
            gm.Replay.replayGame();
            List<Unit> unitsP1;
            gm.CurrentGame.UnitsDictionary.TryGetValue(1, out unitsP1);
            Unit unit = unitsP1.Find(u => u.IdUnit == 3); 
            Assert.IsTrue(unit.Position.Equals(new Position(0, 2)));       
        }

        // Teste si l'AttackCommand éxecute bien une attaque normalement lors d'un replay (on retombe bien sur le même résultat que pendant la partie)
        [TestMethod]
        public void Replay_AttackCommand()
        {
            Game gameBefore = gm.CurrentGame;
            AttackCommand a1 = new AttackCommand(1, 4, 1);
            a1.execute();
            AttackCommand a2 = new AttackCommand(2, 3, 1);
            a2.execute();

            // On récupère le game à l'état initial pour comparer par la suite
            gm.CurrentGame = gm.Replay.GameInit;
            gm.Replay.replayGame();

            Assert.IsTrue(CompareUtil.compareDictionaryUnits(gm.CurrentGame.UnitsDictionary, gameBefore.UnitsDictionary));
        }

        // Teste si l'EndTurnCommand met bien fin au tour lors d'un replay
        [TestMethod]
        public void Replay_EndTurnCommand()
        {
            Game gameBefore = gm.CurrentGame;
            EndTurnCommand e1 = new EndTurnCommand(false);
            e1.execute();
            EndTurnCommand e2 = new EndTurnCommand(false);
            e2.execute();

            gm.CurrentGame = gm.Replay.GameInit;
            gm.Replay.replayGame();
            Player pGameBefore, pGameAfter;
            gameBefore.PlayersDictionary.TryGetValue(1, out pGameBefore);
            gm.CurrentGame.PlayersDictionary.TryGetValue(1, out pGameAfter);

            Assert.AreEqual(gameBefore.CurrentTurn, gm.CurrentGame.CurrentTurn);
            Assert.AreEqual(pGameBefore.Points, pGameAfter.Points);
        }
    }
}
