#include "MapAlgo.h"

MapAlgo::MapAlgo()
{
}

MapAlgo::~MapAlgo()
{
}

// Dispose de manière aléatoire les différents types de Tile possibles
// Le nombre de chaque type de Tile disposée sur la map est identique
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

// Place les unités de départ du joueur 1 dans une zone correspondant au quart de la map, choisie aléatoirement
// Les unités du joueur 2 sont placées dans la quart opposé
std::vector<int> MapAlgo::placePlayers(int width){
	std::vector<int> posVec;

	int sizeMap = width * width;
	int widthZone = width / 2;

	srand(time(NULL));

	// selection aléatoire du quart de map dans lequel seront les unités
	int zoneP1 = rand() % 4;
	int zoneP2 = (zoneP1+ 2) % 4;
	// selection aléatoire de la case à l'intérieur de ce quart
	int posP1 = rand() % (sizeMap / 4);
	int posP2 = rand() % (sizeMap / 4);
	// conversion des positions dans le repère de la map entière
	posVec.push_back(convertPosition(posP1, zoneP1, width));
	posVec.push_back(convertPosition(posP2, zoneP2, width));

	return posVec;
}

// Convertit des positions dans le repère de la map entière
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

