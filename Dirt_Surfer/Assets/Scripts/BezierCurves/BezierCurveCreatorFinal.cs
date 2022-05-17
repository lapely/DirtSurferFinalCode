
//VERSION FINALE

/*
 * Auteure: Lana Sylvestre
 * Génère des courbes de Bézier successives en
 * subdivisant les points en sous-groupes de 4 points de repères.
 * Note: Il n'y a pas de nombre de points maximal.
 * Une courbe apparaît lorsque tous les points dont elle a besoin sont déterminés.
 * Note: Si on enlève un point au milieu de la série de points, toutes les courbes après ce point
 * disparaissent.
 */

using UnityEngine;

public class BezierCurveCreatorFinal : MonoBehaviour
{
    //Points to be used
    public Transform[] controlPoints;

    //Line renderer to draw the curve
    public LineRenderer[] lineRenderer;
    
    private void Start()
    {   
        generation();
    }
    
    public void generation()
    {
        //getcomponent by name
        BezierCurveFinal curve;
        float width = 0.3f;

        //S'ASSURER QUE LE NOMBRE DE LIGNES DANS LINERENDERER ET LE NOMBRE DE POINTS DANS CONTROLPOINTS SONT COMPATIBLES
        for (int i = 0; i < lineRenderer.Length; i++)
        {
            curve = new BezierCurveFinal(controlPoints[3*i], 
                controlPoints[3*i + 3], 
                controlPoints[3*i + 1], 
                controlPoints[3*i + 2]);
            Vector3[] positions = curve.MakeCurve();
        
            lineRenderer[i].startColor = Color.black;
            lineRenderer[i].endColor = Color.black;
            lineRenderer[i].startWidth = width;
            lineRenderer[i].endWidth = width;
            lineRenderer[i].loop = false;
            lineRenderer[i].positionCount = positions.Length;
            lineRenderer[i].SetPositions(positions);
        }
    }
}
