
//VERSION FINALE

/*
 * Auteure: Lana Sylvestre
 * Classe qui permet de générer aléatoirement une courbe selon le bouton pesé (circulaire ou linéaire)
 */

using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonBehaviourFinal : MonoBehaviour
{
   //Deux instances de la classe RandomGenerator pour générer aléatoirement une courbe linéaire ou circulaire
   public RandomGeneratorFinal generatorCircular;
   public RandomGeneratorFinal generatorLinear;
   
   //Pour savoir si on doit générer une courbe linéaire ou circulaire
   public Boolean circulaire; //true = circulaire, false = linéaire
   /*
   public void OnButtonPress(){
      if (circulaire) //création circulaire
      {
         for (int i = 0; i < generatorLinear.creator.controlPoints.Length; i++)
         {
            generatorLinear.creator.controlPoints[i].position = Vector3.zero;
         }

         generatorLinear.creator.generation();
         
         generatorCircular.initializePositionsCircular(true);
         generatorCircular.creator.generation();
      }
      
      else //création linéaire
      {
         for (int i = 0; i < generatorCircular.creator.controlPoints.Length; i++)
         {
            generatorCircular.creator.controlPoints[i].position = Vector3.zero;
         }
         
         generatorCircular.creator.generation();
         
         generatorLinear.initializePositionsLinear(true);
         generatorLinear.creator.generation();
      }
   }
   */
    public void OnDrawCurveLine()
    {
        for (int i = 0; i < generatorCircular.creator.controlPoints.Length; i++)
        {
            generatorCircular.creator.controlPoints[i].position = Vector3.zero;
        }

        generatorCircular.creator.generation();

        generatorLinear.initializePositionsLinear(true);
        generatorLinear.creator.generation();
    }
    public void OnDrawCurveLoop()
    {
        for (int i = 0; i < generatorLinear.creator.controlPoints.Length; i++)
        {
            generatorLinear.creator.controlPoints[i].position = Vector3.zero;
        }

        generatorLinear.creator.generation();

        generatorCircular.initializePositionsCircular(true);
        generatorCircular.creator.generation();
    }
}