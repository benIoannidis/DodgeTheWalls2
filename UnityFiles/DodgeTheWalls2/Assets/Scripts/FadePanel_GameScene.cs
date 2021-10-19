using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Script to handle into fading when changing scenes 
/// </summary>
public class FadePanel_GameScene : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    public Color currentPanelColour;

    public bool shouldFadeIn = false;
    public bool shouldFadeOut = true;

    public bool exitToMenu = false;

    private void Update()
    {
        //check whether fading out or in, and increment the image alpha accordingly
        if (shouldFadeOut)
        {
            currentPanelColour = panel.GetComponent<Image>().color;
            
            if (currentPanelColour.a < 0.6)
            {
                currentPanelColour.a -= 0.01f;
            }
            else
            {
                currentPanelColour.a -= 0.05f;
            }

            panel.GetComponent<Image>().color = currentPanelColour;
            
            if (currentPanelColour.a <= 0)
            {
                currentPanelColour.a = 0;
                shouldFadeOut = false;
                panel.SetActive(false);
            }
        }
        else if (shouldFadeIn)
        {
            currentPanelColour = panel.GetComponent<Image>().color;

            if (currentPanelColour.a > 0.4)
            {
                currentPanelColour.a += 0.01f;
            }
            else
            {
                currentPanelColour.a += 0.05f;
            }

            panel.GetComponent<Image>().color = currentPanelColour;

            if (currentPanelColour.a >= 1)
            {
                if (exitToMenu)
                {
                    SceneManager.LoadScene(0);
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}
