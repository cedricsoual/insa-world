# Projet INSAWorld

Ce projet est réalisé dans le cadre du module POO / MOO de 4ème année au département informatique de l'INSA Rennes.

La solution Visual Studio se décompose en 3 projets.

## C++ Library

Cette bibliothèque propose trois algorithmes :  
- Un algorithme permettant de créer la carte, en respectant les contraintes imposées par le cahier des charges;  
- Un algorithme permettant de placer les unités de chaque joueur de manière à ce qu'elles soient dans des quarts opposés de la carte;  
- Un algorithme de suggestion de mouvement d'une unité au cours du jeu, celui ci compare parmi les cases accessibles celles rapportant le maximum de points de victoire.

## C# Model Implementation

Ce projet contient l'ensemble des classes permettant l'execution du jeu normalement et aussi de rejouer une partie sauvegardée.

## C# Software Testing

Ce projet contient l'ensemble des tests unitaires permettant de tester la bibliothèque C++ ainsi que le modèle en C#.

