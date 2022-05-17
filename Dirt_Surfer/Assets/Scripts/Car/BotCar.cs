// Autheur Élysé Lapalme
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class BotCar : MonoBehaviour
{
    public BezierCurveCreator curve;
    private List<Vector3> nodes = new List<Vector3>();
    // Elyse
    private int currentNode = 0;
    private float coefficientTour;
    private float angle = 0f;
    public int invervalPoint = 20; // 1/n point pour les waypoint
    [FormerlySerializedAs("maxCoeficientTour")] public float maxCoefficientTour = 20f; // a quel point il ralenti dans les virages (plus haut = moins de ralentissement)
    public float minWaypointDistance = 10f; // distance en m à laquelle le bot change de waypoint


    private CarBody carBody;
    public CarEngine carEngine;
    public CarSteering carSteering;
    public CarBrakes carBrakes;

    private float forwardInput; // how much you want to accelerate (0 to 1)
    private float reverseInput; // how much you want to decelerate (0 to -1)
    private float throttleInput; // computed result of forward and reverse
    private float pedalBrakeInput;
    private float handBrakeInput;
    private float steerInput; // steering scale from -1 (left) to 1 (right)

    public float carSpeed; // speed parallel to the car body (m/s)


    private float steerAngle;
    private float wheelTorque;

    void Start()
    {
        carBody = GetComponent<CarBody>();
        handBrakeInput = 0f;

        // Elyse
        curve.generation(); // génération de la courbe de Bézier (voir BezierCurve et BezierCurveCreator)

         for (int i = 0; i < curve.getPosition().Count; i++) // curve.getPosition() est une liste de vecteur contenant toutes les positions de le courbe de Bézier 
        {
            if (i % invervalPoint == 0) // dinimution de la quantité de points
            {
                nodes.Add(curve.getPosition()[i]); // ajout des points (vector3) dans le tableau "nodes"
            }
        }
        print(nodes.Count);
    }

    void FixedUpdate()
    {
        // Elyse

        carSpeed = carBody.BodyVelocity() * Mathf.Sign(carBody.ForwardSpeed());

        CheckWaypointDistance(); // Changement de point si le temps est venu
        ConvertThrottle();
        steerAngle = carSteering.SetSteer(steerInput); // rotation des roues en fonction du résultat d'ApplySteer
        wheelTorque = carEngine.GetTorque(throttleInput); // accélère / décélère
        carBrakes.SetBrake(pedalBrakeInput, handBrakeInput);
        GetCurve(); // calcul du coefficient de virage
        ApplySteer(); // calcul de la nouvelle position des roues
        Drive(); // paramétrer la nouvelle commande de gaz
    }

    private void Drive() // paramétrer la nouvelle commande de gaz
    {
        if (coefficientTour < maxCoefficientTour && carSpeed < 33.3f) // si le coefficient de virage est < qu'une certaine valeur et que la vitesse < 33.3 m/s (120kmh)
        {
            reverseInput = 0;
            forwardInput += 0.5f; // le bot accélère

        }
        else if (carSpeed > 18f) // vitesse minimum de 65 km/h (déterminé expérimentalement)
        {
            forwardInput = 0;
            reverseInput -= 0.5f; // le bot ralentit
        }
        else
        {
            reverseInput = 0;
            forwardInput += 0.2f;
        }

    }

    private void ApplySteer() // calcul de la nouvelle position des roues
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode]); // Vecteur relatif entre l'auto et le prochain node
        float newSteerInput = (relativeVector.x / relativeVector.magnitude); // On normalise le vecteur relatif (pour le ramener entre -1 et 1)
        if (Math.Abs(steerAngle) > 10)
        {
            steerInput = newSteerInput * 4f;
        }
        else
        {
            steerInput = newSteerInput * 2f;
        }

        steerInput = Mathf.Clamp(steerInput, -1, 1); // On s'assure que s'il est plus grand que 1 le programme le ramène à 1

    }

    private void GetCurve() // calcul du coefficient de virage
    {
        angle = 0f;
        float a = 0f;
        float b = 0f;
        float c = 0f;
        for (int i = 1; i < 6; i++) // on regarde donc 5 points devant l'auto pour la courbure
        {
            if ((i + currentNode + 1) < nodes.Count) // on s'assure qu'on ne soit pas en dehors de la grandeur du tableau
            {
                a = transform.InverseTransformPoint(nodes[i + currentNode + 1]).magnitude; // distance entre l'auto et le 2eme (+ i) point devant lui
                b = transform.InverseTransformPoint(nodes[i + currentNode]).magnitude; // distance entre l'auto et le 1er (+ i) point devant lui
                c = Vector3.Distance(nodes[i + currentNode], nodes[i + currentNode + 1]); // distance entre le 1er (+ i) le 2eme (+ i) point devant l'auto

                if (c == 0) // on évite les erreurs de / 0
                {
                    a = 1;
                }
                else if (b == 0)
                {
                    b = 1;
                }
                angle += ((3.1416f) - (Mathf.Acos((-(Mathf.Pow(a, 2)) + Mathf.Pow(c, 2) + Mathf.Pow(b, 2)) / (2 * c * b)))) / ((float)i / 2); // loi des cosinus
            }
        }

        coefficientTour = (angle * carSpeed);
    }

    private void CheckWaypointDistance() // changement de point de visé si nécessaire
    {
        if (Vector3.Distance(transform.position, nodes[currentNode]) < minWaypointDistance) // on vérifie la distance entre la position du bot et du point de visé actuel
        {
            if (currentNode == nodes.Count - 1) // on s'assure que si on est à la fin de la boucle
            {
                currentNode = 0; // on recommence au point de départ
            }
            else // sinon, si le bot est à une distance < la distance minimum, il change de point
            {
                currentNode++;
            }
        }
    }


    private void ConvertThrottle()
    {
        throttleInput = forwardInput + reverseInput;

        if (carSpeed > 5)
        {
            throttleInput = Mathf.Clamp01(throttleInput);
            pedalBrakeInput = -reverseInput;
        }
        else if (carSpeed < -5)
        {
            throttleInput = Mathf.Clamp(throttleInput, -1, 0);
            pedalBrakeInput = forwardInput;
        }
        else
        {
            pedalBrakeInput = 0f;
        }
    }

}
