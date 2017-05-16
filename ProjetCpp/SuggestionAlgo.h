#pragma once
#include <stdio.h>
#include <cstdlib>
#include <vector>
#include <algorithm>
#include <vector>
#include "MapAlgo.h"

enum UnitType
{
	CERBERUS = 0,
	CENTAUR = 1,
	CYCLOP = 2
};

class SuggestionAlgo
{
public:
	SuggestionAlgo();
	~SuggestionAlgo();

	std::vector<int> SuggestionAlgo::bestAction(TilesType possibleMove[], UnitType race);
	int SuggestionAlgo::getVictoryPoints(TilesType tile, UnitType race);

};

