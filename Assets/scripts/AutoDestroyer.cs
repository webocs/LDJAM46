using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroyer : MonoBehaviour
{
    public float time;
    void Start()
    {
        Invoke("autoDestroy", time);
    }

   void autoDestroy()
    {
        Destroy(gameObject);
    }
}
