using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script keeps track of the current score, as well as ending the game on player death, and updating and checking highscore through player prefs
/// </summary>
public class Game_ScoreManager : MonoBehaviour
{
    public int m_score;

    [SerializeField]
    private Text[] scoreText;

    //reference to player explosion particles prefab to instantiate on player death
    [SerializeField]
    private GameObject playerExplosion;

    //reference to player
    [SerializeField]
    private GameObject player;

    private bool playerDead = false;

    //reference to the instantiated player explosion particles prefab
    private GameObject playerParticleObject;

    //UI objects
    [SerializeField]
    private GameObject pauseMenu, resumeButton, restartButton, highscoreText;

    private int currentHighscore;

    [SerializeField]
    private GameObject inGameUI;

    private void Start()
    {
        //check for existence of highscore pref, and create if it doesn't exist
        if (PlayerPrefs.HasKey("Highscore"))
        {
            currentHighscore = PlayerPrefs.GetInt("Highscore");
        }
        else
        {
            currentHighscore = 0;
            PlayerPrefs.SetInt("Highscore", 0);
        }
        highscoreText.GetComponent<Text>().text = currentHighscore.ToString();
        m_score = 0;
        foreach (Text t in scoreText)
        {
            t.text = m_score.ToString();
        }
    }

    private void Update()
    {
        //these to conditional statements help in incrementely slowing down the game timescale
        if (playerDead && Time.timeScale > 0.5)
        {
            Time.timeScale -= 0.025f;
        }
        else if (playerDead && Time.timeScale > 0.3)
        {
            Time.timeScale -= 0.01f;
        }

        //once death particles have been instantiated, this checks to see when they are finished playing, and uses that as a queue to bring up the game over UI
        if (playerParticleObject != null && playerParticleObject.GetComponent<ParticleSystem>().isStopped)
        {
            Time.timeScale = 0f;
            inGameUI.SetActive(false);
            pauseMenu.SetActive(true);
            resumeButton.SetActive(false);
            restartButton.SetActive(true);
            playerParticleObject = null;
            playerDead = false;
        }
    }

    //increment score and update the score text
    public void AddScore()
    {
        m_score++;
        foreach (Text t in scoreText)
        {
            t.text = m_score.ToString();
        }
    }

    //called by player movement script on collision with an object that should kill the player (enemy, enemy bullet, or meteor)
    public void PlayerDied()
    {
        playerDead = true;
        Vector3 pos = player.transform.position;
        Destroy(player);
        
        //check audio manager for whether a sound should be played or not
        playerExplosion.GetComponent<AudioSource>().mute = GameObject.Find("AudioManager").GetComponent<AudioSource>().mute;

        //instantiate death explosion particles
        playerParticleObject = Instantiate(playerExplosion, pos, Quaternion.identity);

        //if a highscore, set the player pref and the visible text
        if (m_score > currentHighscore)
        {
            currentHighscore = m_score;
            PlayerPrefs.SetInt("Highscore", currentHighscore);
            highscoreText.GetComponent<Text>().text = currentHighscore.ToString();
        }
    }
}
