using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : Collidable
{
    //Logica de los recolectables
    protected bool interact;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            OnSpeak();
        }
    }

    protected virtual void OnSpeak()
    {
        interact = true;
    }
}
