using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_POO;
using System.Windows.Input;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;


namespace IHM
{
    public class ReplayViewModel : ViewModelBase
    {
        Game engine;
        Map map;
        Replay replay;
        int iteReplay;
        Page p;

        /// <summary>
        /// Constructeur de la VueModele pricipale
        /// </summary>
        public ReplayViewModel(Page p)
        {
            engine = GameManager.INSTANCE.CurrentGame;
            map = GameManager.INSTANCE.CurrentGame.Map;
            replay = GameManager.INSTANCE.Replay;
            this.p = p;

            // création pour chaque tuile de sa VueModele associée 'TileViewModel'
            for (int l = 0; l < map.Width; l++)
            {
                for (int c = 0; c < map.Width; c++)
                {
                    tiles.Add(new TileViewModel(l, c, map.TilesTab[l, c]));
                }
            }

            foreach (KeyValuePair<int, List<Unit>> kvp in engine.UnitsDictionary)
            {
                foreach (Unit u in kvp.Value)
                {
                    units.Add(new UnitViewModel(u));
                }
            }
            if (engine.SelectedUnit == null) engine.nextUnit();
            iteReplay = 0;
            updateUnit();
            updateSelectedUnit();
           // replayGame();
        }

        public void replayGame()
        {
            // On a finit de jouer toutes les actions sauvegardées
            if (replay.CommandList.Count == iteReplay) {MessageBox.Show("Toutes les actions ont été jouées !", "Replay terminé"); return;}
            ReplayCommand rc = replay.CommandList[iteReplay];
               // if(rc)

                rc.replay();
                updateUnit();
                updateSelectedUnit();
                RaisePropertyChanged("ToursRestants");
                RaisePropertyChanged("PvP1");
                RaisePropertyChanged("PvP2");
                RaisePropertyChanged("CurrentPlayerName");
                RaisePropertyChanged("NbUnitsCurrentPlayer");
                iteReplay++;
                if (engine.IsGameCompleted) p.NavigationService.Navigate(new EndGame());
        }

        /// <summary>
        /// Mise à jour de l'unité
        /// Principe : une unité est associée à une tuile
        /// </summary>
        private void updateUnit()
        {
         
            foreach (var t in tiles)
            {
                t.HasUnit = false;
            }

            TileViewModel tile;
            foreach (UnitViewModel u in units)
            {
                // On affiche l'unité si elle est encore en vie 
                if (u.IsAlive)
                {
                    tile = tiles.FirstOrDefault(t => t.Row == u.Row && t.Column == u.Column);
                    tile.RaceUnit = u.UnitSource;
                    tile.HasUnit = true;
                }
            }
        }

        private void updateTiles()
        {
            foreach (var tile in tiles)
                tile.Refresh();
        }
     
        public int Width
        {
            get { return map.Width; }
        }
      
        List<TileViewModel> tiles = new List<TileViewModel>();
        public IEnumerable<TileViewModel> Tiles
        {
            get { return tiles; }
        }

      
        List<UnitViewModel> units = new List<UnitViewModel>();
        public IEnumerable<UnitViewModel> Units
        {
            get { return units; }
        }

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
                return "Unit " + (listUnit.IndexOf(engine.SelectedUnit) + 1).ToString() + " / " + listUnit.Count;
            }
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
                    suggest = new RelayCommand(replayGame);
                return suggest;
            }
        }

        public void attackUnitAction()
        {
       
        }

        public void moveUnitAction()
        {
           
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
            
        }

        private void nextTurnAction()
        {

        }

        private void suggestAction()
        {
          
        }

        private UnitViewModel selectedUnit;
        public UnitViewModel SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                selectedUnit = value;
                RaisePropertyChanged("SelectedUnit");
            }
        }

        private bool moveEnabled;
        public bool MoveEnabled
        {
            get { return false; }
            set
            {
                moveEnabled = value;
                RaisePropertyChanged("MoveEnabled");
            }
        }

        private bool attackEnabled;
        public bool AttackEnabled
        {
            get { return false; }
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
        public bool ReplayVisibility
        {
            get { return false; }
        }
       
        public string ButtonSuggestName
        {
            get { return "Action suivante"; }
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
