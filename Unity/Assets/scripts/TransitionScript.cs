using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScript : MonoBehaviour
{
    Animator transitionAnim;

    AudioManager audioManager;

    public AudioClip[] sndTween;

    void Awake()
    {
        transitionAnim = GetComponent<Animator>();

        transitionAnim.SetTrigger("Start");

        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();

        audioManager.playSFX(sndTween, transform, 1f);
    }

    public void RestartLevelDelay()
    {
        StartCoroutine(transitionOutDelay());
    }
    
    IEnumerator transitionOutDelay()
    {
        yield return new WaitForSecondsRealtime(1);

        transitionAnim.SetTrigger("End");

        audioManager.playSFX(sndTween, transform, 1f);

        yield return new WaitForSecondsRealtime(.7f);

        reloadScene();
    }

    public void RestartLevel()
    {
        StartCoroutine(transitionOut());
    }

    IEnumerator transitionOut()
    {
        transitionAnim.SetTrigger("End");

        audioManager.playSFX(sndTween, transform, 1f);

        yield return new WaitForSecondsRealtime(.7f);

        reloadScene();
    }

    void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        StartCoroutine(transitionOutQuit());
    }

    IEnumerator transitionOutQuit()
    {
        transitionAnim.SetTrigger("End");
        
        audioManager.playSFX(sndTween, transform, 1f);

        yield return new WaitForSecondsRealtime(.7f);

        Application.Quit();

        Debug.Log("Game has been Closed");
    }
}
