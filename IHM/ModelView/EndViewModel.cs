using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP_POO;

namespace IHM
{
    class EndViewModel : ViewModelBase
    {
        public EndViewModel()
        {
            afficheResultat();
        }

        private string textResult;
        public string TextResult {
            get { return textResult; }
            set
            {
                textResult = value;
                RaisePropertyChanged("TextResult");
            }
        }
        private string imgSource;
        public string ImgSource
        {
            get { return imgSource; }
            set
            {
                imgSource = value;
                RaisePropertyChanged("ImgSource");
            }
        }

        public void afficheResultat()
        {
            Game game = GameManager.INSTANCE.CurrentGame;
            Player winner;
            Player p1 = game.PlayersDictionary[1];
            Player p2 = game.PlayersDictionary[2];
            // Match nul
            if (p1.Points == p2.Points)
            {
                TextResult = "Match nul !";
                ImgSource = "Ressources/draw.png";
                return;
            }
            // Player 1 gagne
            else if (p1.Points > p2.Points)
            {
                winner = p1;
            }
            // Player 2 gagne
            else
            {
                winner = p2;
            }
            TextResult = winner.Name + " remporte la partie !";
            if (winner.Race == UnitType.CERBERUS)
            {
                ImgSource = "Ressources/cerberus.png";
            }
            if (winner.Race == UnitType.CENTAUR)
            {
                ImgSource = "Ressources/centaurs.png";
            }
            if (winner.Race == UnitType.CYCLOP)
            {
                ImgSource = "Ressources/cyclops.png";
            }

        }
    }
}
