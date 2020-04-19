using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    public int visionAoe;
    private int lastVisionAoe;
    private Vector2 lastPosition;
    private bool requiresUpdate;
    private bool firstRoll;
    public int MAX_VISION_AOE = 3;
    // Update is called once per frame

    private void Awake()
    {
        firstRoll = true;
    }
    void Update()
    {
        requiresUpdate = (visionAoe != lastVisionAoe)|| (lastPosition != (Vector2)transform.position) || firstRoll;

        if (requiresUpdate)
        {
            if(!firstRoll) clearPrevious();
            else firstRoll = false;

            lastPosition = transform.position;
            lastVisionAoe = visionAoe;

            int visionLength = visionAoe;
            for (int y = 0; y < visionAoe; y++)
            {
                for (int x = -visionLength+1; x < visionLength; x++)
                {
                    Vector2 target = lastPosition + new Vector2(x, y);
                    Vector2 mirrorTarget = lastPosition -new Vector2(-x, y);
                    Tile t = MapManager.tileAt(target);
                    Tile t2 = MapManager.tileAt(mirrorTarget);
                    if (t)
                    {
                        t.isVisible = true;
                        t.discover();
                    }
                    if (t2)
                    {
                        t2.isVisible = true;
                        t2.discover();
                    }
                }
                visionLength -= 1;
            }
        }
    }

    void clearPrevious()
    {
        int visionLength = lastVisionAoe;       
        for (int y = 0; y < lastVisionAoe; y++)
        {
            for (int x = -visionLength + 1; x < visionLength; x++)
            {
                Vector2 target = lastPosition + new Vector2(x, y);
                Vector2 mirrorTarget = lastPosition - new Vector2(-x, y);
                Tile t = MapManager.tileAt(target);
                Tile t2 = MapManager.tileAt(mirrorTarget);
                if (t) t.isVisible = false;
                if (t2) t2.isVisible = false;

               
            }
            visionLength -= 1;
        }
    }
}
