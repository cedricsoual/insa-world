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
using System.IO;
using Microsoft.Win32;
using TP_POO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace IHM
{
    /// <summary>
    /// Logique d'interaction pour MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement feSource = e.Source as FrameworkElement;
            switch (feSource.Name)
            {
                case "NewGame":
                    NavigationService.Navigate(new NewGamePage());
                    break;
                case "LoadGame":
                    LoadGameFile();
                    break;
                case "ReplayGame":
                    LoadReplayFile();
                    break;
                case "QuitGame":
                    ExitGame();
                    break;
            }
            e.Handled = true;
        }

        public void LoadGameFile()
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = "SaveGame Files |*.sav";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string res = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                string pute = saveFileDialog.FileName;
                GameManager.INSTANCE.loadGame(saveFileDialog.FileName);
                NavigationService.Navigate(new GamePage(false));
            }
            else
            {
                return;
            }

        }

        public void LoadReplayFile()
        {
            OpenFileDialog saveFileDialog = new OpenFileDialog();
            saveFileDialog.Filter = "ReplayGame Files |*.rep";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //string res = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                GameManager.INSTANCE.replayGame(saveFileDialog.FileName);
                NavigationService.Navigate(new GamePage(true));
            }
        }

        public void ExitGame()
        {
            Application.Current.Shutdown();
        }
    }
}
