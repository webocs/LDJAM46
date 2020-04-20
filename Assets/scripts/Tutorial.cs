using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{

    public Text[] tutorials;


    private void Start()
    {
        IEnumerator co = showTutorial();
        StartCoroutine(co);    
    }

    IEnumerator showTutorial()
    {
        int i = 0;
        while (i < tutorials.Length) {
            float tutoTimer = 7.0f;
            while (tutoTimer > 0)
            {
                tutoTimer -= Time.deltaTime;
                tutorials[i].gameObject.SetActive(true);
                yield return null;
            }
            tutorials[i].gameObject.SetActive(false);
            // small pause
            tutoTimer = 1.0f;
            while (tutoTimer > 0)
            {
                tutoTimer -= Time.deltaTime;
                yield return null;
            }
            i++;
        }
    }
}
