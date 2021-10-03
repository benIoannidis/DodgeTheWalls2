using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadePanel_GameScene : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;

    private Color currentPanelColour;

    public bool shouldFadeIn = false;
    public bool shouldFadeOut = true;

    public bool exitToMenu = false;

    private void Update()
    {
        if (shouldFadeOut)
        {
            currentPanelColour = panel.GetComponent<Image>().color;
            
            if (currentPanelColour.a < 0.7)
            {
                currentPanelColour.a -= 0.01f;
            }
            else
            {
                currentPanelColour.a -= 0.05f;
            }

            if (currentPanelColour.a <= 0)
            {
                currentPanelColour.a = 0;
                shouldFadeOut = false;
            }
            panel.GetComponent<Image>().color = currentPanelColour;
        }
    }
}
