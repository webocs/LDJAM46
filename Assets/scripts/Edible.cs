using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edible : MonoBehaviour
{
    public int nutrition;

    public int eat()
    {
        MapManager.removeEdible(this);
        Destroy(gameObject);
        return nutrition;
    }
    protected virtual void MakeNoise()
    {
        MapManager.tileAt(transform.position).visibleFor(1.5f);
    }

    protected virtual void calculateVisibility()
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
