using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AudioClip restartSound;
    public GameObject victoryPrefab;
    private float nextLevelActionTimer= 2.0f;

    bool inVictoryScreen;
    Fader fader;

    private void Awake()
    {
        fader = FindObjectOfType<Fader>();
        inVictoryScreen = false;
    }
    public void gameOver()
    {
        if (!inVictoryScreen)
        {
            fader.fadeIn();
            reload();
        }
    }

    public void checkVictory()
    {      
        List<Edible> edibles = MapManager.getRemainingEdibles();
        if(edibles.Count==0)
        {
            showVictoryMessage();            
        }      
    }
    void showVictoryMessage()
    {
        fader.fadeIn();
        inVictoryScreen = true;
        GameObject vPf = Instantiate(victoryPrefab);               
    }
    public void goToNextLevel() {      
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
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

    private void Update()
    {
        checkVictory();
        if(inVictoryScreen)
        {
            if(nextLevelActionTimer<0 && Input.anyKey)
                goToNextLevel();
            nextLevelActionTimer -= Time.deltaTime;
        }
    }

    internal void tick()
    {
        MapManager.tick();
    }
}

