#include "MapAlgo.h"

MapAlgo::MapAlgo()
{
}

MapAlgo::~MapAlgo()
{
}

// Dispose de mani�re al�atoire les diff�rents types de Tile possibles
// Le nombre de chaque type de Tile dispos�e sur la map est identique
std::vector<TilesType> MapAlgo::fillMap(int size){
	std::vector<TilesType> v;

	for (int i = 0; i < size / 4; ++i){
		v.push_back(Plain);
	}
	for (int i = 0; i < size / 4; ++i){
		v.push_back(Desert);
	}
	for (int i = 0; i < size / 4; ++i){
		v.push_back(Volcano);
	}
	for (int i = 0; i < size / 4; ++i){
		v.push_back(Swamp);
	}

	std::random_shuffle(v.begin(), v.end());

	return v;
}

// Place les unit�s de d�part du joueur 1 dans une zone correspondant au quart de la map, choisie al�atoirement
// Les unit�s du joueur 2 sont plac�es dans la quart oppos�
std::vector<int> MapAlgo::placePlayers(int width){
	std::vector<int> posVec;

	int sizeMap = width * width;
	int widthZone = width / 2;

	srand(time(NULL));

	// selection al�atoire du quart de map dans lequel seront les unit�s
	int zoneP1 = rand() % 4;
	int zoneP2 = (zoneP1+ 2) % 4;
	// selection al�atoire de la case � l'int�rieur de ce quart
	int posP1 = rand() % (sizeMap / 4);
	int posP2 = rand() % (sizeMap / 4);
	// conversion des positions dans le rep�re de la map enti�re
	posVec.push_back(convertPosition(posP1, zoneP1, width));
	posVec.push_back(convertPosition(posP2, zoneP2, width));

	return posVec;
}

// Convertit des positions dans le rep�re de la map enti�re
int MapAlgo::convertPosition(int pos, int zone, int width){
	int res;
	int widthZone = width / 2;

	res = width * (pos / widthZone) + (pos % widthZone);

	switch (zone) {
		case 1 :
			return res + (widthZone);
		case 2 : 
			return res + (widthZone*width) + widthZone;
		case 3 :
			return res + (widthZone*width);
		default:
			return res;
	}
}

