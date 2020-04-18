using UnityEngine;
using UnityEditor;

public class Tile:MonoBehaviour
{
    public bool isWalkable;
    public bool isVisible;
    public bool wasDiscovered;
    public Sprite lastSeenSprite;
    public Sprite sprite;
    public Sprite fogEffect;

    private float visibilityTimer;
    private bool isTemporarilyVisible;
    
    private void Update()
    {
        if (Constants.DISABLE_FOG_OF_WAR) isVisible = true;
        if (Constants.DISABLE_FOG_OF_WAR) wasDiscovered = true;
        if (!isVisible)
        {
            hide();
        } else
        {
            show();
        }
        if (isTemporarilyVisible)
        {
            tempShow();
            if (visibilityTimer < 0)
                tempHide();
            else
                visibilityTimer -= Time.deltaTime;
        }           
        
    }

    public void discover()
    {
        wasDiscovered = true;
    }

    GameObject getChildWithName(string name)
    {
        Transform childTrans = transform.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void visibleFor(float seconds)
    {
        if (!isVisible)
        {
            isTemporarilyVisible = true;
            visibilityTimer = seconds;
        }
    }
    public void hide()
    {      
        if (!wasDiscovered)
            GetComponentInChildren<SpriteRenderer>().sprite = null;
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = lastSeenSprite;
            GameObject fogMask = getChildWithName("fogMask");
            if (fogMask) fogMask.SetActive(true);
        }        
    }
    public void show()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        lastSeenSprite = sprite;
        GameObject fogMask = getChildWithName("fogMask");
        if (fogMask) fogMask.SetActive(false);
    }
    
    private void tempShow()
    {
        GetComponentInChildren<SpriteRenderer>().sprite = sprite;
        GameObject fogMask = getChildWithName("fogMask");
        if (fogMask) fogMask.SetActive(false);
    }

    public void tempHide()
    {
        isTemporarilyVisible = false;
        if (!wasDiscovered)
            GetComponentInChildren<SpriteRenderer>().sprite = null;
        else
        {
            GetComponentInChildren<SpriteRenderer>().sprite = lastSeenSprite;
            GameObject fogMask = getChildWithName("fogMask");
            if (fogMask) fogMask.SetActive(true);
        }
    }
}