using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectables
{
    public Sprite cofreBitcoins;
    public int bitcoinAmount = 5;


    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = cofreBitcoins;
            GameManager.instance.ShowText("+" + bitcoinAmount + " bitcoins", 20, Color.yellow, transform.position, Vector3.up * 50, 1.0f);

        }
        
        //base.OnCollect();
        //Debug.Log("Dar bitcoins");
    }
}
