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
using System.IO;
using Microsoft.Win32;


namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        Game game = GameManager.INSTANCE.CurrentGame;
        public GamePage(bool replaymode)
        {
            InitializeComponent();
            if (replaymode) { this.DataContext = new ReplayViewModel(this); }
            else { this.DataContext = new MainViewModel(this); }
             
           // pseudo_p1.Text = game.PlayersDictionary[1].Name;
           // pseudo_p2.Text = game.PlayersDictionary[2].Name;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            switch (feSource.Name)
            {
                case "Menu":
                    NavigationService.Navigate(new MenuPage());
                    break;
                case "Save":
                   // MessageBox.Show("bite");
                    SaveFile();
                    break;
                case "Move":
                    break;
                case "Attack":
                    break;
                case "Suggest":
                    break;
                case "NextUnit":
                    break;
                case "NextTurn":
                    break;
            }
            e.Handled = true;
        }

        public void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string res = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                GameManager.INSTANCE.saveGame(saveFileDialog.FileName);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
