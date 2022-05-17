
//VERSION FINALE

/* 
Auteure: Lana Sylvestre
Formation d'une courbe de Bézier cubique (avec 4 points de repère)
source: https://www.habrador.com/tutorials/interpolation/2-bezier-curve/
*/

using System;
using UnityEngine;
using System.Collections;
using Unity.Collections;

//Interpolation between 2 points with a Bezier Curve (cubic spline)
public class BezierCurveFinal : Transform
{
    //Easier to use ABCD for the positions of the points so they are the same as in the tutorial image
    Vector3 A, B, C, D;
    
    //Array of positions to follow
    public Vector3[] Positions;
    
    //Number of loops
    public int loops;
    
    public BezierCurveFinal()
    {
        //default constructor
        A = Vector3.zero;
        B = Vector3.zero;
        C = Vector3.zero;
        D = Vector3.zero;
    }
    
    public BezierCurveFinal(Transform s, Transform e, Transform cS, Transform cE)
    {
        A = s.position;
        B = cS.position;
        C = cE.position;
        D = e.position;
    }
    
    public Vector3[] MakeCurve()
    {
        //The start position of the line
        Vector3 lastPos = A;

        //The resolution of the line
        //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
        float resolution = 0.02f;

        //How many loops?
        loops = Mathf.FloorToInt(1f / resolution);

        Positions = new Vector3[loops];
        
        for (int i = 1; i <= loops; i++)
        {
            //Which t position are we at?
            float t = i * resolution;

            //Find the coordinates between the control points with a Catmull-Rom spline
            Vector3 newPos = DeCasteljausAlgorithm(t);

            Positions[i - 1] = newPos;
            
            //Save this pos so we can draw the next line segment
            lastPos = newPos;
        }

        return Positions;
    }
    
    //The De Casteljau's Algorithm
    Vector3 DeCasteljausAlgorithm(float t)
    {
        //To make it faster
        float oneMinusT = 1f - t;
        
        //Layer 1
        Vector3 Q = oneMinusT * A + t * B;
        Vector3 R = oneMinusT * B + t * C;
        Vector3 S = oneMinusT * C + t * D;

        //Layer 2
        Vector3 P = oneMinusT * Q + t * R;
        Vector3 T = oneMinusT * R + t * S;

        //Final interpolated position
        Vector3 U = oneMinusT * P + t * T;

        return U;
    }
}