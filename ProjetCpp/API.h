#include "MapAlgo.h"
#include "SuggestionAlgo.h"

#define EXPORTCDECL extern "C" __declspec(dllexport)

EXPORTCDECL void * MapAlgo_new(){
	return new MapAlgo();
}

EXPORTCDECL void MapAlgo_delete(MapAlgo* algo){
	delete algo;
}

EXPORTCDECL void MapAlgo_fillMap(void * A, TilesType map[], int size){
	std::vector<TilesType> mapVec = ((MapAlgo*)A)->fillMap(size);

	for (int i = 0; i < size; i++){
		map[i] = mapVec[i];
	}
}

EXPORTCDECL void MapAlgo_placePlayers(void * A, int width, int posPlayers[]){
	std::vector<int> unitsVec = ((MapAlgo*)A)->placePlayers(width);

	for (int i = 0; i < 2; i++){
		posPlayers[i] = unitsVec[i];
	}
}

EXPORTCDECL void * SuggestionAlgo_new(){
	return new SuggestionAlgo();
}

EXPORTCDECL void SuggestionAlgo_delete(SuggestionAlgo* algo){
	delete algo;
}

EXPORTCDECL void SuggestionAlgo_bestAction(void * A, TilesType possibleMove[], UnitType race, int bestMove[]){
	std::vector<int> bestMoveVec = ((SuggestionAlgo*)A)->bestAction(possibleMove, race);
	
	for (int i = 0; i < 5; i++){
		bestMove[i] = bestMoveVec[i];
	}

}