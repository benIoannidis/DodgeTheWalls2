using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_ScoreManager : MonoBehaviour
{
    public int m_score;

    [SerializeField]
    private Text[] scoreText;

    [SerializeField]
    private GameObject playerExplosion;

    [SerializeField]
    private GameObject player;

    private bool playerDead = false;

    private GameObject playerParticleObject;

    [SerializeField]
    private GameObject pauseMenu, resumeButton, restartButton, highscoreText;

    private int currentHighscore;

    private void Start()
    {
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
        if (playerDead && Time.timeScale > 0.5)
        {
            Time.timeScale -= 0.025f;
        }
        else if (playerDead && Time.timeScale > 0.3)
        {
            Time.timeScale -= 0.01f;
        }
        if (playerParticleObject != null && playerParticleObject.GetComponent<ParticleSystem>().isStopped)
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            resumeButton.SetActive(false);
            restartButton.SetActive(true);
            playerParticleObject = null;
            playerDead = false;
        }
    }

    public void AddScore()
    {
        m_score++;
        foreach (Text t in scoreText)
        {
            t.text = m_score.ToString();
        }
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;
    }

    public void PlayerDied()
    {
        playerDead = true;
        Vector3 pos = player.transform.position;
        Destroy(player);
        //Time.timeScale = 0.1f;
        playerParticleObject = Instantiate(playerExplosion, pos, Quaternion.identity);

        if (m_score > currentHighscore)
        {
            currentHighscore = m_score;
            PlayerPrefs.SetInt("Highscore", currentHighscore);
            highscoreText.GetComponent<Text>().text = currentHighscore.ToString();
        }
    }
}
