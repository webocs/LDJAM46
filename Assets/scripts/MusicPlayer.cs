using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip song;
    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] musicPlayers = GameObject.FindGameObjectsWithTag("MusicPlayer");
        if (musicPlayers.Length > 1)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().clip = song;
        gameObject.GetComponent<AudioSource>().Play();
    }

}
