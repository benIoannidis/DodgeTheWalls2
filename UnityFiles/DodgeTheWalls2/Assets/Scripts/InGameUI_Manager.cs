using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script handles platform specific disabling of UI elements, as well as pausing the game on the button event trigger
/// </summary>
public class InGameUI_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject shootButton;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject pauseMenu;

    private void Start()
    {
        //remove shoot button for specified platforms
#if PLATFORM_STANDALONE_WIN
        shootButton.SetActive(false);
#elif PLATFORM_WEBGL
        shootButton.SetActive(false);
#endif
    }

    //called on event trigger when the pause button is pressed
    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        shootButton.SetActive(false);
        pauseButton.SetActive(false);

        pauseMenu.SetActive(true);
    }
}
