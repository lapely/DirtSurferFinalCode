/*
 * Auteur: Emile Veillette
 * Je cr�er un constructeur qui permet de r�cup�rer 
 * la position d'un noeud dans le syst�me graphique (x,y,z).Je re
 * Note: Je calcule aussi le cout f ici.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Grid<Node> grid;
    public int x;
    public int y;
    public int z;

    public int gCost;
    public int hCost;
    public int fCost;

    public Node cameFromNode;


    public Node(Grid<Node> grid, int x, int y, int z)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
        this.z = z;
    }

    //J'�cris dans chaque noeud
    public override string ToString()
    {
        return "";
        //return x + "," + y +"," + z;
    }

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
