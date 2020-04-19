using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Fader fader;
    public AudioClip restartSound;
    private void Awake()
    {
        fader = FindObjectOfType<Fader>();
    }
    public void gameOver()
    {
        fader.fadeIn();
        reload();
    }

    public void restart()
    {
        gameObject.GetComponent<AudioSource>().clip = restartSound;
        gameObject.GetComponent<AudioSource>().Play();
        fader.fadeIn();
        Invoke("reload", 1);
    }

    private void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

