using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;

namespace TP_POO
{
    public class GameManager
    {

        public static readonly GameManager INSTANCE = new GameManager();
        public Game CurrentGame {get; set;}
        public Replay Replay { get; set; }
        
        private GameManager(){}

        public Game newGame(){
            CurrentGame = new Game();
            Replay = new Replay();
            return CurrentGame;
        }

        // Enregistre le game et le replay dans un fichier sous la forme d'objets serialized
        public void saveGame(String p)
        {
            string path = p;
            if (Path.HasExtension(p)) path = path.Substring(0, path.Length - 4);
            IFormatter formatter = new BinaryFormatter();
            Stream streamGame = new FileStream(path+".sav", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(streamGame, CurrentGame);
            streamGame.Close();

            Stream streamReplay = new FileStream(path+".rep", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(streamReplay, Replay);
            streamReplay.Close();
        }

        // Charge le game à partir d'un fichier qui contient un serialized
        public void loadGame(string p)
        {
            //string pathExplorer = Path.GetFileNameWithoutExtension(p);
            string path = p.Substring(0, p.Length - 4);
            IFormatter formatter = new BinaryFormatter();
            Stream streamGame = new FileStream(path+".sav", FileMode.Open, FileAccess.Read, FileShare.Read);
            CurrentGame = (Game)formatter.Deserialize(streamGame);
            streamGame.Close();
            
            Stream streamReplay = new FileStream(path+".rep", FileMode.Open, FileAccess.Read, FileShare.Read);
            Replay = (Replay)formatter.Deserialize(streamReplay);
            streamReplay.Close();           
        }

        // Charge le replay à partir d'un fichier
        public void replayGame(String p)
        {
            string path = p.Substring(0, p.Length - 4);
            IFormatter formatter = new BinaryFormatter();
          
            Stream streamReplay = new FileStream(path + ".rep", FileMode.Open, FileAccess.Read, FileShare.Read);
            Replay = (Replay)formatter.Deserialize(streamReplay);
            streamReplay.Close();
            // On restaure la Game à son état initial
            CurrentGame = Replay.GameInit;
        }
    }
}
