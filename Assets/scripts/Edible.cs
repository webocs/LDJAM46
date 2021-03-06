﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{
    public int nutrition;
    public bool ignoreVisibilityCheck;
    public AudioClip cycledSound;
    public AudioClip eatSound;
    public GameObject ripple;
 
    public int eat()
    {
        MapManager.removeEdible(this);
        GameObject.Find("SoundPlayer").GetComponent<AudioSource>().clip = eatSound;
        GameObject.Find("SoundPlayer").GetComponent<AudioSource>().Play();
        Destroy(gameObject);
        return nutrition;
    }

    public virtual void move() { }

    protected virtual void MakeNoise()
    {
        GameObject.Find("SoundPlayer").GetComponent<AudioSource>().clip = cycledSound;
        GameObject.Find("SoundPlayer").GetComponent<AudioSource>().Play();
        GameObject r = Instantiate(ripple, transform.position, Quaternion.identity);               
    }

    protected virtual void calculateVisibility()
    {
        if (!ignoreVisibilityCheck)
        {
            Tile targetTile = MapManager.tileAt(transform.position);
            if (targetTile && targetTile.isVisible)
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }

}
