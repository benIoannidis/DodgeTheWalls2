using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
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

    private GameObject muteButton;

    private void Start()
    {
        GetComponent<AudioSource>().Play();
        DontDestroyOnLoad(this.gameObject);

        muteButton = GameObject.Find("MuteButton");
    }

    private void Update()
    {
        if (muteButton == null)
        {
            muteButton = GameObject.Find("MuteButton");
        }
        else
        {
            GetComponent<AudioSource>().mute = muteButton.GetComponent<MuteButton_Checker>().muted;
        }
    }
}
