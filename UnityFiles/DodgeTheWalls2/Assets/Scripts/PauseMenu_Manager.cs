using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject shootButton;

    [SerializeField]
    private GameObject pauseButton;

    void Start()
    {
        pauseMenu.SetActive(false);
    }

    public void ResumeButtonPress()
    {
        pauseMenu.SetActive(false);
#if PLATFORM_ANDROID
        shootButton.SetActive(true);
#endif
        pauseButton.SetActive(true);

        Time.timeScale = 1;
    }

    public void ExitButtonPress()
    {

    }
}
