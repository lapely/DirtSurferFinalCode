/*
 * Auteur: Emile Veillette
 * C'est ici que je gère toute les options du menu principal
 * Note: Si je veux revenir et je veux utiliser
 * le slider(bar de progression), 
 * alors je dois passer par ici
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    Scene raceMap;
    public GameObject thingsToDisabled;
    public Slider progressBar;
    public GameObject slider;
    public Text progressText;
    public void LoadScene()
    {
        StartCoroutine(LoadAsync());
    }
    public void Quit()
    {
        Debug.Log("Quitting...");
        Application.Quit();
    }

    public void GenerateMap()
    {
        SceneManager.LoadScene("GenerateAMap");
    }

    IEnumerator LoadAsync()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Racing");

            slider.SetActive(true);
            thingsToDisabled.SetActive(false);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;
                progressText.text = progress * 100 + "%";

                yield return null;
            }
        }

        if (SceneManager.GetActiveScene().name == "Racing" || SceneManager.GetActiveScene().name == "GenerateAMap")
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Menu");

            slider.SetActive(true);
            thingsToDisabled.SetActive(false);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                progressBar.value = progress;
                progressText.text = progress * 100 + "%";

                yield return null;
            }
        }
    }
}
