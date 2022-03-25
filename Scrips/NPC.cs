using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactuable
{
    public Sprite npc;
    
    protected override void OnSpeak()
    {
        if (!interact)
        {
            interact = true;
            GetComponent<SpriteRenderer>().sprite = npc;
            GameManager.instance.ShowText("It's dangerous to go outside.\n Llevate esta", 20, Color.yellow, transform.position, Vector3.up * 50, 2.0f);

        }

        //base.OnCollect();
        //Debug.Log("Dar bitcoins");
    }
}
