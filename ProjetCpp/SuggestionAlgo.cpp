#include "SuggestionAlgo.h"


SuggestionAlgo::SuggestionAlgo()
{
}


SuggestionAlgo::~SuggestionAlgo()
{
}


std::vector<int> SuggestionAlgo::bestAction(TilesType possibleMove[], UnitType race){
	std::vector<int> vecVp, v;
	// Initialise les vector à 0
	for (int i = 0; i < 5; i++)
	{
		vecVp.push_back(0);
		v.push_back(0);
	}
	// Récupère les victory points en fonction du type de case et la race de l'unité
	for (int i = 0; i < 5; i++){
		vecVp[i] = getVictoryPoints(possibleMove[i], race);
	}

	// Recherche le max de VP possible
	int maxVp = 0;
	for (int i = 0; i < 5; i++){
		if (vecVp[i] > maxVp) maxVp = vecVp[i];
	}
	// Met à 1 les positions rapportant le meilleur bonus possible
	for (int i = 0; i < 5; i++){
		if (vecVp[i] == maxVp) v[i] = 1;
	}

	return v;
}

// Retourne le nombre de victory points en fonction du type de case et la race de l'unité
int SuggestionAlgo::getVictoryPoints(TilesType tile, UnitType race){
	switch (tile)
	{
	case Out:
		return -1;
	case Plain:
		if (race == CENTAUR) return 3;
		if (race == CERBERUS) return 0;
		if (race == CYCLOP) return 2;
		break;
	case Desert:
		if (race == CENTAUR) return 2;
		if (race == CERBERUS) return 1;
		if (race == CYCLOP) return 3;
		break;
	case Volcano:
		if (race == CENTAUR) return 0;
		if (race == CERBERUS) return 3;
		if (race == CYCLOP) return 1;
		break;
	case Swamp:
		if (race == CENTAUR) return 1;
		if (race == CERBERUS) return 2;
		if (race == CYCLOP) return 0;
		break;
	default:
		return -1;
	}

	return 0;
}
