using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static Dictionary<Vector2,Tile> tiles;
    public static List<Edible> edibles;
    private GameObject[] tilesInMap;

    void Awake()
    {
        tiles = new Dictionary<Vector2, Tile>();
        edibles = new List<Edible>();
        tilesInMap = GameObject.FindGameObjectsWithTag("tile");
        
        foreach(GameObject g in tilesInMap)
        {
            tiles[g.transform.position] = g.GetComponent<Tile>();
            if(!Constants.DISABLE_FOG_OF_WAR)
                tiles[g.transform.position].hide();        
        }

        Edible[] ediblesInMap = FindObjectsOfType<Edible>();
        foreach (Edible e in ediblesInMap)
        {
            edibles.Add(e);
        }
    }

    internal static List<Edible> getRemainingEdibles()
    {
        return edibles;
    }

    public static int getRemaining<T>()
    {
        int remaining = 0;
        if (edibles.Count > 0)
        {
            foreach (Edible e in edibles)
            {
                if (e is T) { remaining++; }
            }
        }
        return remaining;
    }

    public static Edible getEdibleAt(Vector2 position)
    {
        Edible retEdible = null;
        foreach(Edible e in edibles)
        {
            if ((Vector2)e.transform.position == position)
                retEdible = e;
        }
        return retEdible;
    }

    public static void removeEdible(Edible e)
    {
        edibles.Remove(e);
    }

    public static Tile tileAt(Vector2 position)
    {
        if (position != null)
        {
            if (tiles.ContainsKey(position))
                return tiles[position];
            else
                return null;
        }
        else
            return null;
           
    }

    internal static void tick()
    {
       foreach(Edible e in edibles)
        {
            e.move();
        }
    }

    internal static List<Vector2> getSurounding(Vector2 position, bool onlyWalkables)
    {
        List<Vector2> returnDirections = new List<Vector2>();
        Tile upTile, downTile, leftTile, rightTile = null;
        try
        {
            upTile = tiles[position + Vector2.up];
        }
        catch (KeyNotFoundException)
        {
            upTile = null;
        }
        try
        {
            downTile = tiles[position + Vector2.down];
        }
        catch (KeyNotFoundException)
        {
            downTile = null;
        }
        try
        {
            leftTile = tiles[position + Vector2.left];
        }
        catch (KeyNotFoundException)
        {
            leftTile = null;
        }
        try
        {
            rightTile = tiles[position + Vector2.right];
        }
        catch (KeyNotFoundException)
        {
            rightTile = null;
        }
        if (upTile && (!onlyWalkables || upTile.isWalkable)) returnDirections.Add(Vector2.up);
        if (downTile && (!onlyWalkables || downTile.isWalkable)) returnDirections.Add(Vector2.down);
        if (leftTile && (!onlyWalkables || leftTile.isWalkable)) returnDirections.Add(Vector2.left);
        if (rightTile && (!onlyWalkables || rightTile.isWalkable)) returnDirections.Add(Vector2.right);        
  
        return returnDirections;
    }

    public static void registerTile(Vector2 position, Tile tile)
    {
        tiles[position] = tile;
    }

}
