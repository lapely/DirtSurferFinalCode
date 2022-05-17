
//VERSION FINALE    

/*
 *Auteure: Lana Sylvestre
 * Permet de générer aléatoirement des courbes de Bézier en suivant une certaine grille pour
 * avoir un résultat potable
 */

using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RandomGeneratorFinal : MonoBehaviour
{
    //on utilise la classe BezierCurveCreator pour générer les courbes
    public BezierCurveCreatorFinal creator;
    
    //pour choisir quel type de courbe ("circulaire" ou "linéaire) est généré
    public Boolean useLinear;
    public Boolean useCircular;

    private void Start()
    {   
        initializePositionsLinear(useLinear);
        if (useLinear)
        {
            creator.generation();
        }
        
        initializePositionsCircular(useCircular);
        if (useCircular)
        {
            creator.generation();
        }
    }
    
    public void initializePositionsLinear(Boolean use)
    {
        if (use)
        {
            //Le premier point est placé complètement à gauche de la grille de référence
            creator.controlPoints[0].position = new Vector3(-20, 0, UnityEngine.Random.Range(0, 20));
        
            //Le deuxième point est placé au hasard dans les deux carrés les plus à gauche
            creator.controlPoints[1].position = new Vector3(UnityEngine.Random.Range(-20, -10), 0, UnityEngine.Random.Range(0, 20));
        
            //Le troisième point est placé au hasard dans les deux carrés à gauche du milieu
            creator.controlPoints[2].position = new Vector3(UnityEngine.Random.Range(-10, 0), 0, UnityEngine.Random.Range(0, 20));
        
            //Le quatrième point est placé au milieu de la grille de référence
            creator.controlPoints[3].position = new Vector3(0, 0, UnityEngine.Random.Range(0, 20));
        
            //Le cinquième point est placé au hasard dans les deux carrés à droite du milieu
            creator.controlPoints[4].position = new Vector3(UnityEngine.Random.Range(0, 10), 0, UnityEngine.Random.Range(0, 20));
        
            //Le sixième point est placé au hasard dans les deux carrés les plus à droite
            creator.controlPoints[5].position = new Vector3(UnityEngine.Random.Range(10, 20), 0, UnityEngine.Random.Range(0, 20));
        
            //Le septième point est placé complètement à droite de la grille de référence
            creator.controlPoints[6].position = new Vector3(20, 0, UnityEngine.Random.Range(0, 20));
        }
    }
    
    public void initializePositionsCircular(Boolean use)
    {
        if (use)
        {
            //Le premier point est placé au milieu des 4 carrés en haut à gauche
            creator.controlPoints[0].position = new Vector3(-10, 0, UnityEngine.Random.Range(0, 20));
            
            //Le deuxième point est placé au hasard dans les deux carrés à gauche du milieu en haut
            creator.controlPoints[1].position = new Vector3(UnityEngine.Random.Range(-10, 0), 0, UnityEngine.Random.Range(0, 20));
        
            //Le troisième point est placé au hasard dans les deux carrés à droite du milieu en haut
            creator.controlPoints[2].position = new Vector3(UnityEngine.Random.Range(0, 10), 0, UnityEngine.Random.Range(0, 20));
            
            //Le quatrième point est placé au milieu des 4 carrés en haut à droite
            creator.controlPoints[3].position = new Vector3(10, 0, UnityEngine.Random.Range(0, 20));
        
            //Le cinquième point est placé au hasard dans les deux carrés en bas du milieu des 4 carrés en haut à droite
            creator.controlPoints[4].position = new Vector3(UnityEngine.Random.Range(0, 20), 0, UnityEngine.Random.Range(0, 10));
        
            //Le sixième point est placé au hasard dans les deux carrés en haut du milieu des 4 carrés en bas à droite
            creator.controlPoints[5].position = new Vector3(UnityEngine.Random.Range(0, 20), 0, UnityEngine.Random.Range(-10, 0));
            
            //Le septième point est placé au milieu des 4 carrés en bas à droite
            creator.controlPoints[6].position = new Vector3(10, 0, UnityEngine.Random.Range(0, -20));
            
            //Le huitième point est placé au hasard dans les deux carrés à droite du milieu en bas
            creator.controlPoints[7].position = new Vector3(UnityEngine.Random.Range(0, 10), 0, UnityEngine.Random.Range(-20, 0));
            
            //Le neuvième point est placé au hasard dans les deux carrés à gauche du milieu en bas
            creator.controlPoints[8].position = new Vector3(UnityEngine.Random.Range(-10, 0), 0, UnityEngine.Random.Range(-20, 0));
            
            //Le dixième point est placé au milieu des 4 carrés en bas à gauche
            creator.controlPoints[9].position = new Vector3(-10, 0, UnityEngine.Random.Range(-20, 0));
            
            //Le onzième point est placé au hasard dans les deux carrés en haut du milieu des 4 carrés en bas à gauche
            creator.controlPoints[10].position = new Vector3(UnityEngine.Random.Range(-20, 0), 0, UnityEngine.Random.Range(-10, 0));
            
            //Le douzième point est placé au hasard dans les deux carrés en bas du milieu des 4 carrés en haut à gauche
            creator.controlPoints[11].position = new Vector3(UnityEngine.Random.Range(-20, 0), 0, UnityEngine.Random.Range(0, 10));
        }
    }
}
