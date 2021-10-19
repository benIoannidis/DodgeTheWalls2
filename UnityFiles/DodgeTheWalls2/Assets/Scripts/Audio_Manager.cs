using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script to handle the persistent music audio, as well as keep track of the "mute" setting, which other objects will check when attempting to play audio
/// </summary>
public class Audio_Manager : MonoBehaviour
{
    //enforce singleton
    #region Singleton
    private static Audio_Manager m_instance = null;

    private void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;
        }
        else
        {
            Debug.LogWarning($"Multiple {this} detected. Removed duplicate.");
            Destroy(this.gameObject);
        }
    }
    #endregion

    //reference to mutebutton
    private GameObject muteButton;

    private void Start()
    {
        //start music playing
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this.gameObject);

        muteButton = GameObject.Find("MuteButton");
    }

    private void Update()
    {
        //attempt to find mute button object if null
        if (muteButton == null)
        {
            muteButton = GameObject.Find("MuteButton");
        }
        //when returning to the menu, ensure the mutebutton is set according to the mute setting on the music audio source
        else
        {
            GetComponent<AudioSource>().mute = muteButton.GetComponent<MuteButton_Checker>().muted;
        }
    }
}
