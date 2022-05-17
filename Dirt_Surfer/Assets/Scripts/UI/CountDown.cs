/*
 * Auteur: Emile Veillette
 * C'est ici que je fais le compte à rebours pour le début 
 * de la course dans la scène Racing.
 * Note : Le code est sur l'objet UI.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CountDown : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject startCanvas;
    public GameObject count1;
    public GameObject count2;
    public GameObject count3;
    public GameObject countGO;
    public void Start()
    {
        theCountDown();
    }

    public void theCountDown()
    {
        StartCoroutine(startofRace());
    }


    IEnumerator startofRace()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 0f;
        startCanvas.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        count1.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        count2.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        count3.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        countGO.SetActive(true);
        Time.timeScale = 1f;
        yield return new WaitForSecondsRealtime(1.5f);
        startCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }
}
