using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// script to handle pause menu UI event triggers
/// </summary>
public class PauseMenu_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject shootButton;

    [SerializeField]
    private GameObject pauseButton;

    [SerializeField]
    private GameObject resumeButton, exitButton, restartButton;

    [SerializeField]
    private GameObject fadeManager;

    void Start()
    {
        restartButton.SetActive(false);
        pauseMenu.SetActive(false);
    }

    //called by resume button event trigger
    public void ResumeButtonPress()
    {
        pauseMenu.SetActive(false);
#if PLATFORM_ANDROID
        shootButton.SetActive(true);
#endif
        pauseButton.SetActive(true);

        Time.timeScale = 1;
    }

    //inform fade image that game is transitioning scenes, disable particles and obstacle spawning
    //called by exit button event trigger
    public void ExitButtonPress()
    {
        fadeManager.GetComponent<FadePanel_GameScene>().exitToMenu = true;
        fadeManager.GetComponent<FadePanel_GameScene>().shouldFadeIn = true;
        Button[] m_buttons = GetComponentsInChildren<Button>();

        foreach (Button b in m_buttons)
        {
            b.enabled = false;
        }
        GameObject.Find("ObjectSpawner").SetActive(false);
        Obstacle_MoveScript[] obstacles = FindObjectsOfType(typeof(Obstacle_MoveScript)) as Obstacle_MoveScript[];

        foreach (Obstacle_MoveScript o in obstacles)
        {
            o.enabled = false;
        }
        GameObject.Find("StarSpawner").GetComponent<ParticleSystem>().Stop();

        Time.timeScale = 1;
    }

    //inform fade image that game is transitioning scenes, disable particles and obstacle spawning
    //called by restart button event trigger
    public void RestartButtonPress()
    {
        fadeManager.GetComponent<FadePanel_GameScene>().shouldFadeIn = true;

        Button[] m_buttons = GetComponentsInChildren<Button>();

        foreach (Button b in m_buttons)
        {
            b.enabled = false;
        }
        GameObject.Find("ObjectSpawner").SetActive(false);
        Obstacle_MoveScript[] obstacles = FindObjectsOfType(typeof(Obstacle_MoveScript)) as Obstacle_MoveScript[];
    
        foreach (Obstacle_MoveScript o in obstacles)
        {
            o.enabled = false;
            o.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        GameObject.Find("StarSpawner").GetComponent<ParticleSystem>().Stop();

        Time.timeScale = 1;
    }
}
