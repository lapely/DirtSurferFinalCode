/*
 * Auteur: Emile Veillette
 * Genère un sytème graphique dans unity et permet de retrouver
 * certaines informations du grid comme la position du grid dans 
 * unity ou la position d'un objet dans le grid.
 * Note: J'ai utilisé le code de CodeMonkey pour l'aspect visuel du graphique,
 * donc pour mieux comprendre CreateWorldClass(), regarder UtilsClass.
 */

using CodeMonkey.Utils;
using System;
using UnityEngine;

public class Grid<TGridObject>
{
    //dimension du grid (système graphique)
    private int xWidth;
    private int yHeight;
    private int zLength;
    private float cellSize;

    //La position d'origine du graphique
    private Vector3 originPosition;

    //Tout les arrays vide que nous allons remplir de TextMesh dans unity
    private TGridObject[,,] gridArray;

    //L'objet parent du grid pour que le projet Unity soit mieux organisé.
    private GameObject gridParent;

    //Constructeur du sytème graphique
    public Grid(int width, int height, int length, float cellSize, Vector3 originPosition, Func<Grid<TGridObject>, int, int, int, TGridObject> createGridObject)
    {
        //Initialisation de chaque variable
        this.xWidth = width;
        this.yHeight = height;
        this.zLength = length;
        this.cellSize = cellSize;
        this.originPosition = originPosition;
        gridArray = new TGridObject[xWidth, yHeight, zLength];
        //Permet de créer les lignes blanches représentant le système graphique
        TextMesh[,,] debugTextArray = new TextMesh[(int)width, (int)height, (int)length];

        //Création du système graphique
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                for (int z = 0; z < gridArray.GetLength(2); z++)
                {
                    //Création des objets possédant le text mesh
                    gridArray[x, y, z] = createGridObject(this, x, y, z);
                    //Les objets se font donner un textMesh qui représente une ligne dans le graphique
                    // debugTextArray[x, y, z] = UtilsClass.CreateWorldText(gridArray[x, y, z].ToString(), null, GetUnityPosition(x, y, z) + new Vector3(cellSize, cellSize, cellSize) * .5f, 10, Color.white, TextAnchor.MiddleCenter);
                    //J'associe les objets au parent
                    gridParent = GameObject.Find("GridParent");
                    debugTextArray[x, y, z].transform.parent = gridParent.transform;
                    //Debug.DrawLine(GetUnityPosition(x, y, z), GetUnityPosition(x + 1, y, z), Color.white, 100f);
                    //Debug.DrawLine(GetUnityPosition(x, y, z), GetUnityPosition(x, y + 1, z), Color.white, 100f);
                    //Debug.DrawLine(GetUnityPosition(x, y, z), GetUnityPosition(x, y, z + 1), Color.white, 100f);

                    //J'associe les objets au parent
                    gridParent = GameObject.Find("GridParent");
                }
            }
        }
        //Debug.DrawLine(GetUnityPosition(0, yHeight, zLength), GetUnityPosition(xWidth, yHeight, zLength), Color.white, 100f);
        //Debug.DrawLine(GetUnityPosition(xWidth, 0, zLength), GetUnityPosition(xWidth, yHeight, zLength), Color.white, 100f);
        //Debug.DrawLine(GetUnityPosition(xWidth, yHeight, 0), GetUnityPosition(xWidth, yHeight, zLength), Color.white, 100f);
    }

    //Permet de trouver la position dans Unity d'un vecteur faisant partie du système graphique.
    public Vector3 GetUnityPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize + originPosition;
    }

    public float GetWidth()
    {
        return xWidth;
    }
    public float GetHeight()
    {
        return yHeight;
    }
    public float GetLength()
    {
        return zLength;
    }
    public float getCellSize()
    {
        return cellSize;
    }

    //Permet de trouver la position dans le système graphique d'un vecteur faisant partie du système graphique.
    public void GetGridPosition(Vector3 worldPosition, out int x, out int y, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public TGridObject GetGridObject(int x, int y, int z)
    {
        return gridArray[x, y, z];
    }
}

