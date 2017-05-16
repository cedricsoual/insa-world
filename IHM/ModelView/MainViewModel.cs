using System;
using System.Threading.Tasks;
using TP_POO;
using System.Windows.Input;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace IHM
{
    /// <summary>
    /// VueModele principale
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        Game engine;
        Map map;
        Page p;

        /// <summary>
        /// Constructeur de la VueModele pricipale
        /// </summary>
        public MainViewModel(Page p)
        {
            this.p = p;
            engine = GameManager.INSTANCE.CurrentGame;
            map = GameManager.INSTANCE.CurrentGame.Map;

            // création pour chaque tuile de sa VueModele associée 'TileViewModel'
            for (int l = 0; l < map.Width; l++)
            {
                for (int c = 0; c < map.Width; c++)
                {
                    tiles.Add(new TileViewModel(l, c, map.TilesTab[l,c]));
                }
            }
            
            foreach(KeyValuePair<int, List<Unit>> kvp in engine.UnitsDictionary)
            {
                foreach(Unit u in kvp.Value){
                    units.Add(new UnitViewModel(u));
                }
            }
            if (engine.SelectedUnit == null) engine.nextUnit();
            updateUnit();
            updateSelectedUnit();
        }

        /// <summary>
        /// Mise à jour de toutes les unités
        /// Principe : une unité est associée à une tuile
        /// </summary>
        private void updateUnit()
        {
            /*
            var unit = engine.SelectedUnit;
            // on enlève l'unité de son ancienne tuile
            var tile = tiles.FirstOrDefault(t => t.HasUnit);
            if (tile != null)
                tile.HasUnit = false;
            // on positionne l'unité sur sa nouvelle tuile
            tile = tiles.FirstOrDefault(t => t.Row == unit.Position.X && t.Column == unit.Position.Y);
            tile.HasUnit = true;
            */
            foreach (var t in tiles)
            {
                t.HasUnit = false;
            }
                
            TileViewModel tile;
            foreach(UnitViewModel u in units){
                // On affiche l'unité si elle est encore en vie 
                if (u.IsAlive)
                {
                    tile = tiles.FirstOrDefault(t => t.Row == u.Row && t.Column == u.Column);
                    tile.RaceUnit = u.UnitSource;
                    tile.HasUnit = true;
                }
            }
        }
        // Efface les suggestions de mouvement
        private void updateTiles()
        {
            foreach (var tile in tiles)
                tile.Refresh();
        }
        /// <summary>
        /// Acces à la largeur de la map
        /// </summary>
        public int Width
        {
            get { return map.Width; }
        }
        /// <summary>
        /// Acces à la liste des Tuiles
        /// </summary>
        List<TileViewModel> tiles = new List<TileViewModel>();
        public IEnumerable<TileViewModel> Tiles
        {
            get { return tiles; }
        }

        /// <summary>
        /// Acces à la liste des Units
        /// </summary>
        List<UnitViewModel> units = new List<UnitViewModel>();
        public IEnumerable<UnitViewModel> Units
        {
            get { return units; }
        }

        /// <summary>
        /// reaction à selectedTile
        /// </summary>
        private TileViewModel selectedTile;
        public TileViewModel SelectedTile
        {
            get { return selectedTile; }
            set
            {
                if (selectedTile != null)
                    selectedTile.IsSelected = false;
                selectedTile = value;
                if (selectedTile != null)
                {
                    MoveEnabled = engine.isPossibleMove(new Position(selectedTile.Row, selectedTile.Column));
                    AttackEnabled = engine.isPossibleAttack(new Position(selectedTile.Row, selectedTile.Column));
                    selectedTile.IsSelected = true;
                }
                RaisePropertyChanged("SelectedTile");
            }
        }

        public string ToursRestants
        {
            get { return "Tours : " + engine.CurrentTurn.ToString() + " / " + engine.MaxTurn.ToString(); }
        }

        public int PvP1
        {
            get
            {
                return engine.PlayersDictionary[1].Points;
            }
        }
        public int PvP2
        {
            get
            {
                return engine.PlayersDictionary[2].Points;
            }
        }

        public string CurrentPlayerName
        {
            get
            {
                return "Au tour de : " + engine.PlayersDictionary[engine.CurrentPlayer].Name;
            }
        }

        public string NbUnitsCurrentPlayer
        {
            get
            {
                List<Unit> listUnit = engine.UnitsDictionary[engine.CurrentPlayer];
                return "Unit " + (listUnit.IndexOf(engine.SelectedUnit)+1).ToString() + " / " + listUnit.Count ;
            }
        }
        public bool ReplayVisibility
        {
            get { return true; }
        }
      


        public string ButtonSuggestName
        {
            get { return "Aide"; }
        }
        private ICommand nextTurn;
        public ICommand NextTurn
        {
            get
            {
                if (nextTurn == null)
                    nextTurn = new RelayCommand(nextTurnAction);
                return nextTurn;
            }
        }

        private ICommand nextUnit;
        public ICommand NextUnit
        {
            get
            {
                if (nextUnit == null)
                    nextUnit = new RelayCommand(nextUnitAction);
                return nextUnit;
            }
        }

        private ICommand move;
        public ICommand Move
        {
            get
            {
                if (move == null)
                    move = new RelayCommand(moveUnitAction);
                return move;
            }
        }

        private ICommand attack;
        public ICommand Attack
        {
            get
            {
                if (attack == null)
                    attack = new RelayCommand(attackUnitAction);
                return attack;
            }
        }

        private ICommand suggest;
        public ICommand Suggest
        {
            get
            {
                if (suggest == null)
                    suggest = new RelayCommand(suggestAction);
                return suggest;
            }
        }

        public void attackUnitAction()
        {
            engine.attackUnit(new Position(SelectedTile.Row, SelectedTile.Column));
            updateUnit();
            updateSelectedUnit();
            updateTiles();
        }

        public void moveUnitAction()
        {
            if (engine.isPossibleMove(new Position(SelectedTile.Row, SelectedTile.Column)))
            {
                engine.moveUnit(new Position(SelectedTile.Row, SelectedTile.Column));
            }
            else if (engine.attackUnit(new Position(SelectedTile.Row, SelectedTile.Column)))
            {
                engine.attackUnit(new Position(SelectedTile.Row, SelectedTile.Column));
            }
            else { //return; 
            }
            updateUnit();
            updateSelectedUnit();
            updateTiles();
        }

        public void updateSelectedUnit()
        {
            var unit = engine.SelectedUnit;
            SelectedUnit = new UnitViewModel(unit);
            // on enlève l'unité de son ancienne tuile
            var tile = tiles.FirstOrDefault(t => t.IsUnitSelected);
            if (tile != null)
                tile.IsUnitSelected = false;
            // on positionne l'unité sur sa nouvelle tuile
            tile = tiles.FirstOrDefault(t => t.Row == unit.Position.X && t.Column == unit.Position.Y);
            tile.IsUnitSelected = true;
        }

        private void nextUnitAction()
        {
            int oldTurn = engine.CurrentTurn;
            int oldCurrentPlayer = engine.CurrentPlayer;
            engine.nextUnit();
            if (engine.IsGameCompleted)
            {
                p.NavigationService.Navigate(new EndGame());
            }
            updateSelectedUnit();
            updateTiles();
            // Si on passe au joueur suivant on met à jour le nom du joueur courant
            if (oldCurrentPlayer != engine.CurrentPlayer) RaisePropertyChanged("CurrentPlayerName");
            // Si on est passé au tour suivant en passant à l'unité suivante
            if (oldTurn != engine.CurrentTurn)
            {
                RaisePropertyChanged("ToursRestants");
                RaisePropertyChanged("PvP1");
                RaisePropertyChanged("PvP2");
            }
            RaisePropertyChanged("NbUnitsCurrentPlayer");
        }

        private void nextTurnAction()
        {
            engine.nextTurn();
            if (engine.IsGameCompleted)
            {
                p.NavigationService.Navigate(new EndGame());
            }
            updateUnit();  
            updateTiles();
            Message = "Prochain tour";
            RaisePropertyChanged("ToursRestants");
            RaisePropertyChanged("PvP1");
            RaisePropertyChanged("PvP2");
            RaisePropertyChanged("CurrentPlayerName");
            RaisePropertyChanged("NbUnitsCurrentPlayer");

        }

        private void suggestAction()
        {
            Task.Factory.StartNew(() =>
            {
                TileViewModel tile;
                foreach(Position p in engine.getBestMove()){
                    tile = tiles.FirstOrDefault(t => t.Row == p.X && t.Column == p.Y);
                    if(tile!= null) tile.IsTileSuggested = true;
                }
                //updateUnit();  
                //updateTiles();
            });
        }

        private UnitViewModel selectedUnit;
        public UnitViewModel SelectedUnit
        {
            get { return selectedUnit; }
            set { selectedUnit = value;
            RaisePropertyChanged("SelectedUnit");
            }
        }

        private bool moveEnabled;
        public bool MoveEnabled
        {
            get { return moveEnabled; }
            set
            {
                moveEnabled = value;
                RaisePropertyChanged("MoveEnabled");
            }
        }

        private bool attackEnabled;
        public bool AttackEnabled
        {
            get { return attackEnabled; }
            set
            {
                attackEnabled = value;
                RaisePropertyChanged("AttackEnabled");
            }
        }

        public string NameP1
        {
            get { return engine.PlayersDictionary[1].Name; }
        }
        public string NameP2
        {
            get { return engine.PlayersDictionary[2].Name; }
        }

        string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message == value)
                    return;
                message = value;
                RaisePropertyChanged("Message");
            }
        }
    }
}
