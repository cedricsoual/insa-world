using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_POO;

namespace IHM
{
    public class TileViewModel : ViewModelBase
    {
        private Tile tile;

        public TileViewModel(int row, int column, Tile tile)
        {
            this.tile = tile;
            this.raceUnit = "Ressources/centaurs.png";

            if (tile is Swamp)
            {
                Type = "Swamp";
                Color = "Blue";
            }
            if (tile is Plain)
            {
                Type = "Plain";
                Color = "DarkGreen";
            }
            if (tile is Desert)
            {
                Type = "Desert";
                Color = "Yellow";
            }

            if (tile is Volcano)
            {
                Type = "Volcano";
                Color = "Orange";    
            }

            Row = row;
            Column = column;
        }


        public string Color { get; private set; }
        public string Type { get; private set; }

        public int Row { get; private set; }
        public int Column { get; private set; }

        public int Iron { get { return 0; } }

        private bool isTileSuggested;
        public bool IsTileSuggested
        {
            get { return isTileSuggested; }
            set { isTileSuggested = value;
            RaisePropertyChanged("IsTileSuggested");
            }
        }

        String raceUnit;
        public String RaceUnit
        {
            get { return raceUnit; }
            set
            {
                raceUnit = value;
                RaisePropertyChanged("RaceUnit");
            }
        }

        bool hasUnit;
        public bool HasUnit
        {
            get { return hasUnit; }
            set
            {
                hasUnit = value;
                RaisePropertyChanged("HasUnit");
            }
        }

        bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        private bool isUnitSelected;
        public bool IsUnitSelected
        {
            get { return isUnitSelected; }
            set
            {
                isUnitSelected = value;
                RaisePropertyChanged("IsUnitSelected");
            }
        }

        internal void Refresh()
        {
            IsTileSuggested = false;
        }
    }
}
