using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject shootButton;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject fadeManager;

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
        fadeManager.GetComponent<FadePanel_GameScene>().exitToMenu = true;
        fadeManager.GetComponent<FadePanel_GameScene>().shouldFadeIn = true;
        Button[] m_buttons = GetComponentsInChildren<Button>();

        foreach (Button b in m_buttons)
        {
            b.enabled = false;
        }
    }
}
