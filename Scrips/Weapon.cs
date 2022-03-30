using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    public int[] damagePoint = { 1, 4, 6, 9, 12, 15, 19 };
    public float[] pushForce = { 2.0f, 2.0f, 2.0f, 2.5f, 2.7f, 3.0f, 3.4f };

    //Upgrade
    public int weaponLvl = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    protected override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {
            if (coll.name == "Player")
            {
                return;
            }

            //Create a new damage object,then we'll send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLvl],
                origin = transform.position,
                pushForce = pushForce[weaponLvl]
            };

            coll.SendMessage("RecieveDamage", dmg);
            
            //Debug.Log(coll.name);

        }
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }

    public void upgradeWeapon()
    {
        //Change it's visual
        weaponLvl++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];


    }

    public void setWeaponLvl(int lvl)
    {
        weaponLvl = lvl;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLvl];
    }
}
