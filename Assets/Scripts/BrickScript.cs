using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickScript : MonoBehaviour
{
    public int points;
    public int hitsToBreak;
    public Transform explosion;
    public Sprite lightlyCracked;
    public Sprite moderatelyCracked;
    public Sprite heavilyCracked;

    public void BreakBrick()
    {
        hitsToBreak--;
        if (hitsToBreak == 1)
        {
            GetComponent<SpriteRenderer>().sprite = heavilyCracked;
        }
        if (hitsToBreak >= 2)
        {
            GetComponent<SpriteRenderer>().sprite = moderatelyCracked;
        }
        if (hitsToBreak >= 3)
        {
            GetComponent<SpriteRenderer>().sprite = lightlyCracked;
        }
    }
}
