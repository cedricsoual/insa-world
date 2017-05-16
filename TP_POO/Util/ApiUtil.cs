using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace TP_POO
{
    public static class ApiUtil
    {
        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MapAlgo_fillMap(IntPtr algo, TileType[] tiles, int nbTiles);

        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void MapAlgo_placePlayers(IntPtr algo, int width, int[] posPlayers);

        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SuggestionAlgo_bestAction(IntPtr A, TileType[] possibleMove, UnitType race, int[] bestMove);

        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MapAlgo_new();

        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr MapAlgo_delete(IntPtr algo);

        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SuggestionAlgo_new();

        [DllImport("ProjetCpp.dll", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SuggestionAlgo_delete(IntPtr algo);

        // Fait appel à la dll pour remplir disposer les différents types de Tile sur la map
        public static void fillMap(TileType[] tiles, int nbTiles, int size)
        {
            IntPtr algo = MapAlgo_new();
            MapAlgo_fillMap(algo, tiles, size);
            MapAlgo_delete(algo);
        }
        // Fait appel à la dll pour disposer les unités à l'initialisation du jeu sur la map
        public static void placePlayers(int width, int[] posPlayers)
        {
            IntPtr algo = MapAlgo_new();
            ApiUtil.MapAlgo_placePlayers(algo, 10, posPlayers);
            MapAlgo_delete(algo);
        }

        public static List<Position> getBestActions(UnitType race, Position pUnit, Map map)
        {
            TileType[] possibleMoveTile = new TileType[5];
            // On créé les 4 positions accessibles par l'unité
            Position pTop = new Position(pUnit.X - 1, pUnit.Y);
            Position pRight = new Position(pUnit.X, pUnit.Y + 1);
            Position pBellow = new Position(pUnit.X + 1, pUnit.Y);
            Position pLeft = new Position(pUnit.X, pUnit.Y - 1);

            Position[] possibleMovePos = new Position[5] {pUnit, pTop, pRight, pBellow, pLeft};

            // Par défaut on met le type de Tile à -1 qui correspont à "OUT" au cas où l'unité serait en bordure de la Map
            for (int i = 0; i < 5; i++) possibleMoveTile[0] = (TileType)(-1);
            // On recupère le type de Tile pour chaque case accessible par l'unité
            if (map.getTileAt(pUnit) != null) possibleMoveTile[0] = (TileType)map.getTileAt(pUnit).getType();
            if (map.getTileAt(pTop) != null) possibleMoveTile[1] = (TileType)map.getTileAt(pTop).getType();
            if (map.getTileAt(pRight) != null) possibleMoveTile[2] = (TileType)map.getTileAt(pRight).getType();
            if (map.getTileAt(pBellow) != null) possibleMoveTile[3] = (TileType)map.getTileAt(pBellow).getType();
            if (map.getTileAt(pLeft) != null) possibleMoveTile[4] = (TileType)map.getTileAt(pLeft).getType();

            IntPtr algo = SuggestionAlgo_new();
            var bestMove = new int[5];
            SuggestionAlgo_bestAction(algo, possibleMoveTile, race, bestMove);
            SuggestionAlgo_delete(algo);

            List<Position> bestPos = new List<Position>();
            // On place dans une liste le ou les meilleur(s) choix possible(s) que l'algo a déterminé  
            for (int i = 0; i < 5; i++)
            {
                if (bestMove[i] == 1) bestPos.Add(possibleMovePos[i]);
            }
            return bestPos;
        }
        
    }
}
