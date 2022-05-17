/*
 * Auteur: Emile Veillette
 * Je m'occupe de toute les boutons du menu pause 
 * dans la scène racing. Je met aussi le jeu en pause
 * Note: Je n'ai pas eu le temps de bien restart une 
 * course et nous n'avons pas fait de système de checkpoint 
 * alors cette fonction ne marche pas
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject inGameUI;
    public GameObject playerCar;
    public PlayerInput carInput;
    private InputAction openMenu;

    private bool paused = false;
    private bool keyPressed = false;

    CountDown countdown;

    Vector3 startPosition;
    private void Start()
    {
        startPosition = playerCar.transform.position;
        countdown = GetComponent<CountDown>();
        Debug.Log(countdown);
    }

    void Update()
    {
        openMenu = carInput.actions["OpenPauseMenu"];

        if (openMenu.IsPressed() && keyPressed == false)
        {
            keyPressed = true;
            if (paused)
            {
                Resume();
                paused = false;
            }
            else
            {
                Pause();
                paused = true;
            }

        }
        else if (!openMenu.IsPressed())
        {
            keyPressed = false;
        }
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        inGameUI.SetActive(true);
        gameIsPaused = false;
    }

    void Pause()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        inGameUI.SetActive(false);
        gameIsPaused = true;
    }
    public void restartTheRace()
    {
        playerCar.transform.position = startPosition;
    }
    public void LoadLastCheckpoint()
    {

    }
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
