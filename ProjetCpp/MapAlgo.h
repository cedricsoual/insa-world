#pragma once

#include <stdio.h>
#include <cstdlib>
#include <vector>
#include <algorithm>
#include <time.h>

enum TilesType {
	Out = -1,
	Swamp = 0,
	Desert = 1,
	Plain = 2,
	Volcano = 3
};

class MapAlgo
{
public:
	MapAlgo();
	~MapAlgo();

	std::vector<TilesType> fillMap(int size);
	std::vector<int> placePlayers(int width);
	int convertPosition(int pos, int z, int width);
};

