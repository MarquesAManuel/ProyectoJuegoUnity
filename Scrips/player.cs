using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    public static Player instance;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Awake()
    {
        if (Player.instance == null)
        {
            Player.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    
    private void FixedUpdate()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLvlUp()
    {
        maxHitpoitns++;
        hitpoints = maxHitpoitns;
    }

    public void SetLvl(int level)
    {
        for (int i = 0; i < level; i++)
        {
            OnLvlUp();
        }
    }
}
