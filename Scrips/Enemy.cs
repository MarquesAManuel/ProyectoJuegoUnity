using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience
    public int xpValue = 1;

    //Logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Enemy hitbox
    private ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Check if player is in range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if (chasing = Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
                chasing = true;
            
            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
        //UpdateMotor(Vector3.zero);

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)

                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //Limpiamos el array
            hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.exp += xpValue;
        GameManager.instance.ShowText("+ " + xpValue + " XP", 30, Color.magenta, transform.position, Vector3.up * 40, 1.5f);
    }
}
