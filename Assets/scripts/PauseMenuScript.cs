using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pausePanel;

    public TransitionScript transitionScript;

    AudioManager audioManager;

    public AudioClip[] sndPause;
    public AudioClip[] sndClick;
    public AudioClip[] sndGonk;
    public AudioClip[] sndBlip;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if(Time.timeScale == 1)
            {
                Pause();   
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {   
        audioManager.playSFX(sndPause, transform, 1f);

        pausePanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void Resume()
    {   
        audioManager.playSFX(sndClick, transform, 1f);

        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    public void Retry()
    {   
        //Resume();

        audioManager.playSFX(sndBlip, transform, 1f);

        transitionScript.RestartLevel();
    }

    public void Quit()
    {   
        //Resume();

        audioManager.playSFX(sndGonk, transform, 1f);

        transitionScript.QuitGame();
    }

}