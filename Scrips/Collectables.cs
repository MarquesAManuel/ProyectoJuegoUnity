using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : Collidable
{
    //Logica de los recolectables
    protected bool collected;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            OnCollect();
        }
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }

}
