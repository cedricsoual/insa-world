using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;


namespace TP_POO.tests
{
    [TestClass]
    public class TestGame
    {
        private Game game;
        [TestInitialize]
        public void init()
        {
            game = GameManager.INSTANCE.newGame();
            GameStrategy gs = game.initGame(MapSize.DEMO);
            gs.addPlayer("User1", UnitType.CYCLOP);
            gs.addPlayer("User2", UnitType.CERBERUS);
            gs.buildGame();
        }

        // Teste l'assignation du nom d'un joueur
        [TestMethod]
        public void Game_PlayerName()
        {
           Player player1;
           game.PlayersDictionary.TryGetValue(1, out player1);
           Assert.AreEqual("User1", player1.Name);
        }

        // Teste l'assignation de la race d'un joueur
        [TestMethod]
        public void Game_PlayerRace()
        {
            Player player1;
            game.PlayersDictionary.TryGetValue(1, out player1);
            Assert.AreEqual(UnitType.CYCLOP, player1.Race);
        }

        // Teste que le bon nombre d'unités est créé en fonction du type de game
        [TestMethod]
        public void Game_NbUnits()
        {
            List<Unit> unitsListPlayer1;
            game.UnitsDictionary.TryGetValue(1, out unitsListPlayer1);
            Assert.AreEqual(4, unitsListPlayer1.Count);
        }

        // Teste que la map fait bien la taille adaptée au type de jeu
        [TestMethod]
        public void Game_MapSize()
        {
            Map map = game.Map;
            Assert.AreEqual((int)MapSize.DEMO, map.TilesTab.GetLength(0));
        }

        // Teste l'état de début du jeu
        [TestMethod]
        public void Game_StartGame()
        {
            game.nextUnit();
            Assert.AreEqual(1, game.SelectedUnit.IdUnit);
            Assert.AreEqual(1, game.CurrentPlayer);
        }

        // Teste le changement de joueur une fois qu'un joueur a joué toutes ses unités
        [TestMethod]
        public void Game_SwitchPlayer()
        {
          for (int i = 0; i < 5; i++) { game.nextUnit(); }
          Assert.AreEqual(2, game.CurrentPlayer);
        }

        //Teste le changement de tour après que les deux joueurs aient joué
        [TestMethod]
        public void Game_SwitchTurn()
        {
            for (int i = 0; i < 17; i++) { game.nextUnit(); }
            Assert.AreEqual(3, game.CurrentTurn);
        }

        // Teste la valeur renvoyée si on veut bouger une unité sur une case hors de portée de mouvement
        [TestMethod]
        public void Game_MoveUnitUnreachable()
        {
            game.nextUnit();
            game.SelectedUnit.Position = new Position(0, 0);
            Assert.IsFalse(game.moveUnit(new Position(4, 4)));
        }

        // Teste la valeur renvoyée si on veut bouger une unité sur une case disponible
        [TestMethod]
        public void Game_MoveUnitReachable()
        {
            game.nextUnit();
            game.SelectedUnit.Position = new Position(0, 0);
            Assert.IsTrue(game.moveUnit(new Position(0, 1)));
        }

        // Teste le mouvement d'une unité
        [TestMethod]
        public void Game_MoveUnit()
        {
            game.nextUnit();
            game.SelectedUnit.Position = new Position(0, 0);
            game.moveUnit(new Position(0, 1));
            game.moveUnit(new Position(0, 2));

            Assert.IsTrue(game.SelectedUnit.Position.Equals(new Position(0, 2)));
        }

        // Teste la réaction d'une unité si on lui demande de bouger plus de fois qu'elle n'a de points de mouvement
        [TestMethod]
        public void Game_TooManyMoves()
        {
            game.nextUnit();
            game.SelectedUnit.Position = new Position(0, 0);
            game.moveUnit(new Position(0, 1));
            game.moveUnit(new Position(0, 2));
            game.moveUnit(new Position(0, 3));
            game.moveUnit(new Position(0, 4));

            Assert.IsTrue(game.SelectedUnit.Position.Equals(new Position(0, 3)));
        }

        // Teste l'attaque d'une unité hors de portée
        [TestMethod]
        public void Game_AttackUnitUnreachable()
        {
            game.nextUnit();
            game.SelectedUnit.Position = new Position(0, 0);
            Assert.IsFalse(game.attackUnit(new Position(4, 4)));
        }

        // Teste l'attaque d'une unité à portée
        [TestMethod]
        public void Game_AttackUnitReachable()
        {
            game.nextUnit();
            game.SelectedUnit.Position = new Position(0, 0);
            // On positionne une unité adverse sur une case accessible
            List<Unit> unitsListPlayer2;
            game.UnitsDictionary.TryGetValue(2, out unitsListPlayer2);
            // On prend l'unit d'Id1 du player2
            Unit defender = unitsListPlayer2.Find(u => u.IdUnit.Equals(1));
            defender.Position = new Position(1, 0);
            Assert.IsTrue(game.attackUnit(new Position(1,0)));
        }

        // Teste le calcul des points de victoire à la fin d'un tour
        [TestMethod]
        public void Game_VictoryPoints()
        {
            game.Map.TilesTab[0, 0] = new Swamp();
            game.Map.TilesTab[0, 1] = new Volcano();
            game.Map.TilesTab[0, 2] = new Desert();
            game.Map.TilesTab[0, 3] = new Plain();
            List<Unit> unitsP1;
            game.UnitsDictionary.TryGetValue(1, out unitsP1);
            // On modifie volontairement la position des unités
            int i =0;
            foreach (Unit u in unitsP1)
            {
                u.Position = new Position(0, i);
                i ++;
            }
            game.computeVictoryPoints();
            Player p1;
            game.PlayersDictionary.TryGetValue(1, out p1);
            Assert.AreEqual(6, p1.Points);
        }

        // Teste le choix d'un défenseur si plusieurs unités ennemies occupent la case
        [TestMethod]
        public void Game_ChoseDefender()
        {
            List<Unit> unitsP2;
            game.UnitsDictionary.TryGetValue(2, out unitsP2);
            int i = 1;
            // On modifie la vie de plusieurs unités pour vérifier que le programme choisi bien celle avec le plus de points de vie
            foreach (Unit u in unitsP2)
            {
                u.Position = new Position(0, 1);
                u.Health -= i;
                i++;
            }
            Unit bestDefender = unitsP2.Find(u => u.IdUnit == 1);
            Unit defenderSelected = game.choseDefender(new Position(0,1));
            Assert.AreEqual(bestDefender, defenderSelected);
        }

        // Teste que les points de mouvement sont bien réinitialisés à la fin d'un tour
        [TestMethod]
        public void Game_ResetMovePoints()
        {
            List<Unit> unitsP2;
            game.UnitsDictionary.TryGetValue(2, out unitsP2);
            int i = 0;
            foreach (Unit u in unitsP2)
            {
                u.Move -= i;
                i++;
            }

            game.nextTurn();
            Assert.AreEqual(3, unitsP2.Find(u => u.IdUnit == 1).Move);
            Assert.AreEqual(3, unitsP2.Find(u => u.IdUnit == 2).Move);
            Assert.AreEqual(3, unitsP2.Find(u => u.IdUnit == 3).Move);
            Assert.AreEqual(3, unitsP2.Find(u => u.IdUnit == 4).Move);
        }
    }
}
