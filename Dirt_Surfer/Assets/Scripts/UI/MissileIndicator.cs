/*
 * Auteur: Emile Veillette
 * Je m'occupe de l'indicateur sur le missile 
 * pour qu'on puisse savoir ou le missile est à tout moment.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileIndicator : MonoBehaviour
{
    public Camera currentCamera;
    public RawImage missileAlertImage;

    public Text distanceFromCar;
    private SpriteRenderer sprite;

    public Transform car;
    public Transform missile;

    public Vector3 offset;

    Vector3 positionMissile;


    private void Start()
    {
        sprite = missileAlertImage.GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (missileAlertImage != null)
        {
            float xMinMarker = missileAlertImage.GetPixelAdjustedRect().width / 2;
            float xMaxMarker = Screen.width - xMinMarker;

            float yMinMarker = missileAlertImage.GetPixelAdjustedRect().height / 2;
            float yMaxMarker = Screen.height - yMinMarker;



            //Je cherche le missile
            positionMissile = currentCamera.WorldToScreenPoint(missile.position + offset);

            if (Vector3.Dot((missile.position - transform.position), transform.forward) < 0)
            {
                // La target est derriere le joeur
                if (positionMissile.x < Screen.width / 2)
                    positionMissile.x = xMaxMarker;
                else
                    positionMissile.x = xMinMarker;

            }

            positionMissile.x = Mathf.Clamp(positionMissile.x, xMinMarker, xMaxMarker);
            positionMissile.y = Mathf.Clamp(positionMissile.y, yMinMarker, yMaxMarker);

            missileAlertImage.transform.position = positionMissile;
            distanceFromCar.text = ((int)Vector3.Distance(missile.position, car.position)).ToString() + "m";

        }
    }
}
