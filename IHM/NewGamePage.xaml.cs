using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP_POO;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour StartNewGame.xaml
    /// </summary>
    public partial class NewGamePage : Page
    {
        private MapSize size_map = 0;
        private String name_p1 = null, name_p2 = null;
        private UnitType race_p1= (UnitType)9, race_p2=(UnitType)9;
        public NewGamePage()
        {
            InitializeComponent();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            //MapSize size_map = 0;
            // Pseudo par défault
            // String name_p1 = "Player1", name_p2 = "Player2";
            /*
            if (Demo.IsChecked == true) size_map = MapSize.DEMO;
            if (Small.IsChecked == true) size_map = MapSize.SMALL;
            if (Standard.IsChecked == true) size_map = MapSize.STANDARD;*/

            name_p1 = Pseudo_Player1.Text;
            name_p2 = Pseudo_Player2.Text;
            if (size_map == 0) { MessageBox.Show("Veuillez entrer un type de carte !", "Type de carte manquant"); return; }
            if (name_p1 == "" || name_p2 == "") { MessageBox.Show("Veuillez entrer un pseudo pour chaque joueur !", "Pseudo(s) manquant(s)"); return; }
            if (name_p1 == name_p2) { MessageBox.Show("Veuillez entrer un pseudo différent pour chaque joueur !", "Pseudo(s) identique(s)"); return; }
            if (race_p1 == (UnitType)9 || race_p2 == (UnitType)9) { MessageBox.Show("Veuillez entrer une race pour chaque joueur !", "Race(s) manquante(s)"); return; }

            Game g = GameManager.INSTANCE.newGame();
            GameStrategy gs = g.initGame(size_map);

            // Choisit aléatoirement le joueur commencant la partie
            int begin = new Random().Next(3);
            if (begin == 0) { gs.addPlayer(name_p1, race_p1); gs.addPlayer(name_p2, race_p2); }
            else { gs.addPlayer(name_p2, race_p2); gs.addPlayer(name_p1, race_p1); }

            gs.buildGame();
            NavigationService.Navigate(new GamePage(false));
        }

        private void Button_Click_Menu(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MenuPage());
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            switch (feSource.Name)
            {
                case "CentaurP1":
                    CentaurP2.IsEnabled = false;
                    race_p1 = UnitType.CENTAUR;
                    break;
                case "CerberusP1":
                    CerberusP2.IsEnabled = false;
                    race_p1 = UnitType.CERBERUS;
                    break;
                case "CyclopsP1":
                    CyclopsP2.IsEnabled = false;
                    race_p1 = UnitType.CYCLOP;
                    break;
                case "CentaurP2":
                    CentaurP1.IsEnabled = false;
                    race_p2 = UnitType.CENTAUR;
                    break;
                case "CerberusP2":
                    CerberusP1.IsEnabled = false;
                    race_p2 = UnitType.CERBERUS;
                    break;
                case "CyclopsP2":
                    CyclopsP1.IsEnabled = false;
                    race_p2 = UnitType.CYCLOP;
                    break;
                case "Demo" :
                    size_map = MapSize.DEMO;
                    break;
                case "Small":
                    size_map = MapSize.SMALL;
                    break;
                case "Standard":
                    size_map = MapSize.STANDARD;
                    break;
            }
            e.Handled = true;
        }

        private void CentaurP1_Unchecked(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            switch (feSource.Name)
            {
                case "CentaurP1":
                    CentaurP2.IsEnabled = true;
                    break;
                case "CerberusP1":
                    CerberusP2.IsEnabled = true;
                    break;
                case "CyclopsP1":
                    CyclopsP2.IsEnabled = true;
                    break;
                case "CentaurP2":
                    CentaurP1.IsEnabled = true;
                    break;
                case "CerberusP2":
                    CerberusP1.IsEnabled = true;
                    break;
                case "CyclopsP2":
                    CyclopsP1.IsEnabled = true;
                    break;
            }
            e.Handled = true;
        }


    }
}
