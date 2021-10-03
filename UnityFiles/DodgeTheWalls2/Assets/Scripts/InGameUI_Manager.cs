using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
#if PLATFORM_STANDALONE_WIN
        shootButton.SetActive(false);
#endif
    }


    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        shootButton.SetActive(false);
        pauseButton.SetActive(false);

        pauseMenu.SetActive(true);
    }
}
