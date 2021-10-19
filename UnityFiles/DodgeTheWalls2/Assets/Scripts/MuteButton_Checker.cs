using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script to handle changing the mute button text to inform the user of the audio's current state
/// </summary>
public class MuteButton_Checker : MonoBehaviour
{
    public bool muted;

    private void Start()
    {
        muted = GameObject.Find("AudioManager").GetComponent<AudioSource>().mute;

        if (muted)
        {
            GetComponentInChildren<Text>().text = "UNMUTE";
        }
        else
        {
            GetComponentInChildren<Text>().text = "MUTE";
        }
    }
    public void ButtonPressed()
    {
        muted = !muted;

        if (muted)
        {
            GetComponentInChildren<Text>().text = "UNMUTE";
        }
        else
        {
            GetComponentInChildren<Text>().text = "MUTE";
        }
    }
}
