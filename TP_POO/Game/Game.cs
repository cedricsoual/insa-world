using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace TP_POO
{
    [Serializable]
    public class Game
    {

        public IDictionary<int, Player> PlayersDictionary{ get; set; }
        public IDictionary<int, List<Unit>> UnitsDictionary { get; set; }
        public Unit SelectedUnit { get; set; }
        public Map Map { get; set; }
        public int CurrentTurn {get; set;}
        public int CurrentPlayer{ get; set; }
        public int UnitCounterTurn{ get; set;}
        public int MaxTurn { get; set; }
        public int UnitCount { get; set; }
        public bool IsGameCompleted { get; set; }


        public Game()
        {
            PlayersDictionary = new Dictionary<int, Player>();
            UnitsDictionary= new Dictionary<int, List<Unit>>();
            UnitCounterTurn = 0;
            CurrentPlayer = 1;
            CurrentTurn = 1;
            IsGameCompleted = false;
        }

        public bool isReachable(Position targetedTile)
        {
            Position positionUnit = SelectedUnit.Position;
            float ptMoveAvailable = SelectedUnit.Move;
            float ptMoveRequired = SelectedUnit.computeMovePoint(Map.getTileAt(targetedTile));
            // Si le nombre de MovePoint est suffisant et que le déplacement est bien sur une case accessible
            if (ptMoveAvailable >= ptMoveRequired &&
                (((Math.Abs(positionUnit.X - targetedTile.X) == 1 && Math.Abs(positionUnit.Y - targetedTile.Y) == 0)) ^
                ((Math.Abs(positionUnit.X - targetedTile.X) == 0 && Math.Abs(positionUnit.Y - targetedTile.Y) == 1))))
            { return true; }
            return false;
        }

        public bool isPossibleMove(Position targetedTile)
        {
            if (isReachable(targetedTile) && !isPossibleAttack(targetedTile)) return true;
            return false;
        }

        public bool isPossibleAttack(Position targetedTile)
        {
            if (isReachable(targetedTile))
            {
              

                if((UnitsDictionary[3 - CurrentPlayer].Find(u => u.Position.X == targetedTile.X && u.Position.Y == targetedTile.Y)) != null)
                    return true;
            }
            return false;
        }

        public List<Position> getBestMove()
        {
            return ApiUtil.getBestActions(SelectedUnit.getRace(), SelectedUnit.Position, Map);

        }

        public bool moveUnit(Position targetedTile)
        {
            bool isAccessible = false;
            Position positionUnit = SelectedUnit.Position;
            float ptMoveAvailable = SelectedUnit.Move;
            float ptMoveRequired = SelectedUnit.computeMovePoint(Map.getTileAt(targetedTile));
            // Si le nombre de MovePoint est suffisant et que le déplacement est bien sur une case accessible
            if (isPossibleMove(targetedTile))
            {
                isAccessible = true;
                MoveCommand mc = new MoveCommand(SelectedUnit.IdUnit, targetedTile, ptMoveRequired);
                mc.execute();
            }
            return isAccessible;
        }

        // Return true si l'unité est accessible
        public bool attackUnit(Position targetedTile)
        {
            bool isAccessible = false;
            Position positionUnit = SelectedUnit.Position;
            float ptMoveAvailable = SelectedUnit.Move;
            float ptMoveRequired = SelectedUnit.computeMovePoint(Map.getTileAt(targetedTile));
            // Si le nombre de MovePoint est suffisant et que l'attaque est effectuée sur une unité à portée
            if (isPossibleAttack(targetedTile))
            {
                isAccessible = true;
                AttackCommand mc = new AttackCommand(SelectedUnit.IdUnit, choseDefender(targetedTile).IdUnit, ptMoveRequired);
                mc.execute();
            }
            return isAccessible;
        }

        public void nextTurn()
        {
            EndTurnCommand etd; 
            if (CurrentTurn == MaxTurn)
            {
                // On met fin à la partie
                etd = new EndTurnCommand(true);
                etd.execute();
                return;
            }
            etd = new EndTurnCommand(false);
            etd.execute();
            CurrentPlayer = 1;
            UnitCounterTurn = 0;
            nextUnit();
        }

        public void nextUnit()
        {
            List<Unit> unitsList;
            UnitsDictionary.TryGetValue(CurrentPlayer, out unitsList);
            // On a parcouru toutes les unités
            if (UnitCounterTurn == UnitCount) {
                // Si le joueur actuel est le 1, on passe au joueur 2 
                if (CurrentPlayer == 1) { 
                    CurrentPlayer = 2; 
                    UnitCounterTurn = 0; 
                    nextUnit(); }
                // Si le joueur actuel est le 2, on passe au tour suivant
                else nextTurn();
                return;
            }
            UnitCounterTurn++;
            Unit unit = unitsList.Find(u => u.IdUnit.Equals(UnitCounterTurn)); 
            if(unit == null) {
                nextUnit();
                return;
            }
            SelectedUnit = unit;
        }

        // Retire une unité du jeu
        public void deleteUnit(int idPlayer, Unit unit)
        {
            List<Unit> unitsList;
            UnitsDictionary.TryGetValue(idPlayer, out unitsList);
            unitsList.Remove(unit);
     
            if (unitsList.Count == 0) {
                // On met fin à la partie
                EndTurnCommand etd = new EndTurnCommand(true);
                etd.execute();
            } 
        }

        // Initialise le builder qui va construire la Game
        public GameStrategy initGame(MapSize gameType)
        {
            GameStrategy gameStg;
            switch (gameType)
            {
                case MapSize.DEMO: 
                    gameStg = new DemoGame(this);
                    break;
                case MapSize.SMALL:
                    gameStg = new SmallGame(this);
                    break;
                case MapSize.STANDARD :
                    gameStg = new StandardGame(this);
                    break;
                default :
                    gameStg = null;
                    break;
            }
            return gameStg;
        }

        // Génère l'ensemble des unités
        public void createUnits(int nbUnits)
        {
            UnitFactory unitFct = new UnitFactory();

            var posPlayers = new int[2];
            IntPtr algo = ApiUtil.MapAlgo_new();
            ApiUtil.MapAlgo_placePlayers(algo, Map.Width, posPlayers);
            foreach (KeyValuePair<int, Player> kvp in PlayersDictionary)
            {
                int idPlayer = kvp.Key;
                Player player = kvp.Value;
                UnitType race = player.Race;
                List<Unit> unitsList = new List<Unit>();
                Position positionDepart = new Position(posPlayers[idPlayer-1] / Map.Width, posPlayers[idPlayer-1] % Map.Width);

                for (int i = 1; i <= nbUnits; i++)
                {
                    unitsList.Add(unitFct.createUnit(positionDepart, player.Race, i));
                }
                UnitsDictionary.Add(idPlayer, unitsList);
            }
        }

        // Créé les Players
        public void createPlayers(IDictionary<string, UnitType> playersDetails)
        {
            int idPlayer = 1;
            foreach (KeyValuePair<string, UnitType> kvp in playersDetails)
            {
                PlayersDictionary.Add(idPlayer, new Player(kvp.Key, kvp.Value, idPlayer));
                idPlayer++;
            }
        }

        // Génère la Map 
        public void createMap(int width)
        {
            Map = new Map(width);
            Map.createTiles();
        }

        // Retourne le meilleur défenseur pour une position donnée 
        public Unit choseDefender(Position position)
        {
            List<Unit> unitsListDef;
            Unit defender = null;
            float bestRatio = 0;
            float ratio = 0;
            int playerDef = (1+2) - CurrentPlayer;
            UnitsDictionary.TryGetValue(playerDef, out unitsListDef);
            // Retourne une liste des unités présentes à la position passée en paramètre
            List<Unit> unitsListAtPos = unitsListDef.FindAll(u => u.Position.Equals(position));
            // Recherche l'unité disposant du meilleur ratio ptsvierestants/ptsviemax
            foreach (Unit u in unitsListAtPos)
            {
                    ratio = (float) u.Health / (float) u.MaxHealth;
                    if (ratio == 1) return u;
                    if (ratio > bestRatio)
                    {
                        bestRatio = ratio;
                        defender = u;
                    }
            }
            return defender;
        }

        // Calcule le nombre de VictoryPoints génerés par l'ensemble des unités
        public void computeVictoryPoints()
        {
            foreach (KeyValuePair<int, Player> kvp in PlayersDictionary)
            {
                int IdPlayer = kvp.Key;
                Player p = kvp.Value;
                List<Unit> unitsList;
                List<Position> alreadyCounted = new List<Position>();
                p.Points = 0;
                UnitsDictionary.TryGetValue(IdPlayer, out unitsList);
                foreach (Unit u in unitsList)
                {
                    if (alreadyCounted.Find(pos => pos.X == u.Position.X && pos.Y == u.Position.Y) == null)
                    {
                        alreadyCounted.Add(u.Position);
                        p.Points += u.computeVictoryPoint(Map.getTileAt(u.Position));
                    }
                }
            }
        }

        public bool Equals(Game g)
        {
            // If parameter is null return false:
            if ((object)g == null)
            {
                return false;
            }
            // Return true if the fields match:
            //if (!SelectedUnit.Equals(g.SelectedUnit)) return false;
            if (!Map.Equals(g.Map)) return false;
            if (CurrentTurn != g.CurrentTurn) return false;
            if (UnitCounterTurn != g.UnitCounterTurn) return false;
            if (MaxTurn != g.MaxTurn) return false;
            if (UnitCount != g.UnitCount) return false;
            CompareUtil.compareDictionaryUnits(UnitsDictionary, g.UnitsDictionary);
            CompareUtil.commpareDictionaryPlayer(PlayersDictionary, g.PlayersDictionary);
            
            return true;
        }
    }
}
