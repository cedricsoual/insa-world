using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_POO;

namespace IHM
{
    public class UnitViewModel : ViewModelBase
    {
        private Unit unit;
        

        public UnitViewModel(Unit unit)
        {
            this.unit = unit;

            if (unit is Cerberus)
            {
                Type = "Cerberus";
                Color = "Blue";
                UnitSource = "Ressources/cerberus.png";
            }
            if (unit is Centaurs)
            {
                Type = "Centaurs";
                Color = "DarkGreen";
                UnitSource = "Ressources/centaurs.png";
            }
            if (unit is Cyclops)
            {
                Type = "Cyclops";
                Color = "Yellow";
                UnitSource = "Ressources/cyclops.png";
            }

            Row = this.unit.Position.X;
            Column = this.unit.Position.Y;
        }

        public string PvRemaining
        {
            get { return "PV : " + unit.Health.ToString() + " / " + unit.MaxHealth; }
        }
        public string MovePoints
        {
            get { return "Move : " + unit.Move.ToString(); }
        }
        public string AttackPoints
        {
            get { return "Attack : " + unit.Attack.ToString(); }
        }
        public string DefensePoints
        {
            get { return "Defense : " + unit.Defense.ToString(); }
        }
        public bool IsAlive
        {
            get { return unit.Health>0; }
        }


        public string Color { get; private set; }
        public string Type { get; private set; }
        public string UnitSource { get; private set; }

        private int row;
        public int Row {
            get { return this.unit.Position.X; }
            set { 
                row = value; 
            }
        }
        private int column;
        public int Column
        {
            get { return this.unit.Position.Y; }
            set { column = value; }
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

    }
}
