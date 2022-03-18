using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectables
{
    public Sprite cofreBitcoins;
    public int bitcoinAmounts = 5;


    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = cofreBitcoins;
            Debug.Log("Dar " + bitcoinAmounts + " bitcoins!");

        }
        
        //base.OnCollect();
        //Debug.Log("Dar bitcoins");
    }
}
