using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject blackoutPanel;

    [SerializeField]
    private GameObject playButton;

    [SerializeField]
    private GameObject exitButton;

    [SerializeField]
    private GameObject[] headlineTextObjects;

    private bool menuUnblacked = true;

    private bool shouldFadeToLeave = false;
    private bool fadedToLeave = false;


    private bool checkTextGrow = true;

    private void Start()
    {
        playButton.GetComponent<Button>().enabled = false;
        exitButton.GetComponent<Button>().enabled = false;

        foreach (GameObject g in headlineTextObjects)
        {
            g.GetComponent<RectTransform>().localScale = Vector3.zero;
        }
    }

    private void Update()
    {
        if (checkTextGrow)
        {
            if (headlineTextObjects[0].GetComponent<RectTransform>().localScale.x < 1)
            {
                Vector3 current = headlineTextObjects[0].GetComponent<RectTransform>().localScale;
                current += new Vector3(0.01f, 0.01f, 0.01f);

                foreach (GameObject g in headlineTextObjects)
                {
                    g.GetComponent<RectTransform>().localScale = current;
                }
            }
            else if (headlineTextObjects[0].GetComponent<RectTransform>().localScale.x >= 1)
            {
                headlineTextObjects[0].GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
                menuUnblacked = false;
                checkTextGrow = false;
            }
        }
        if (!menuUnblacked)
        {
            Color current = blackoutPanel.GetComponent<Image>().color;
            if (current.a < 0.7)
            {
                current.a -= 0.01f;
            }
            else
            {
                current.a -= 0.05f;
            }
            blackoutPanel.GetComponent<Image>().color = current;

            if (current.a <= 0)
            {
                current.a = 0;
                blackoutPanel.GetComponent<Image>().color = current;

                menuUnblacked = true;
                playButton.GetComponent<Button>().enabled = true;
                exitButton.GetComponent<Button>().enabled = true;
                blackoutPanel.SetActive(false);
            }
        }

        if (shouldFadeToLeave)
        {
            if (headlineTextObjects[0].GetComponent<RectTransform>().localScale.x > 0)
            {
                Vector3 currentSize = headlineTextObjects[0].GetComponent<RectTransform>().localScale;
                currentSize -= new Vector3(0.1f, 0.1f, 0.1f);

                foreach (GameObject g in headlineTextObjects)
                {
                    g.GetComponent<RectTransform>().localScale = currentSize;
                }
            }


            Color current = blackoutPanel.GetComponent<Image>().color;
            if (current.a < 0.2)
            {
                current.a += 0.01f;
            }
            else
            {
                current.a += 0.05f;
            }
            blackoutPanel.GetComponent<Image>().color = current;

            if (current.a >= 1)
            {
                StartCoroutine("LoadGame");
            }
        }
    }

    private IEnumerator LoadGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("MainScene");
    }

    public void OnPlayPress()
    {
        blackoutPanel.SetActive(true);
        shouldFadeToLeave = true;
    }

    public void OnExitPress()
    {

    }
}
