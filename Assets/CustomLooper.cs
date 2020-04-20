using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLooper : MonoBehaviour
{
    AudioSource audioSource;    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator co = loopAudio();
        StartCoroutine(co);
    }

    IEnumerator loopAudio()
    {
        while (true) {
            if(!audioSource.isPlaying)
            audioSource.Play();
            yield return new WaitForSeconds(0.3f);
        }

    }
}
