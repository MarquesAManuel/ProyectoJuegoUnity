using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        //Limpiar info del jugador para empezar de 0
        //PlayerPrefs.DeleteAll(); 

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        DontDestroyOnLoad(gameObject);
    }

    //Recursos
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> expTable;

    //Referencias

    public Player player;
    public Weapon weapon;
    public TextManager textManager;

    //Logica
    public int bitcoin;
    public int exp;

    
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        textManager.Show(msg, fontSize, color, position, motion, duration);
    }

    //Upgrade weapon
    public bool TryUpgradeWeapon()
    {
        //Is the weapon at max lvl?
        if (weaponPrices.Count <= weapon.weaponLvl)
        {
            return false;
        }

        if (bitcoin >= weaponPrices[weapon.weaponLvl])
        {
            bitcoin -= weaponPrices[weapon.weaponLvl];
            weapon.upgradeWeapon();
            return true;
        }

        return false;
    }

    //Experience system
    public int GetCurrentLvl()
    {
        int r = 0;
        int add = 0;

        while (exp >= add)
        {
            add += expTable[r];
            r++;

            if (r == expTable.Count) //Check if max lvl
            {
                return r;
            }
        }

        return r;
    }

    public int GetXpToLvl(int lvl)
    {
        int r = 0;
        int xp = 0;

        while (r < lvl)
        {
            xp += expTable[r];
            r++;
        }

        return xp;
    }

    public void GrantXP(int xp)
    {
        int currLevel = GetCurrentLvl();
        exp += xp;
        if (currLevel < GetCurrentLvl())
        {
            OnLvlUp();
        }
    }

    public void OnLvlUp()
    {
        Debug.Log("Level UP!");
        player.OnLvlUp();
    }


    /*Save state
    *
    * INT prefSkin
    * INT bitcoin
    * INT exp
    * INT weaponLvl
    *
    */

    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += bitcoin.ToString() + "|";
        s += exp.ToString() + "|";
        s += weapon.weaponLvl.ToString(); 

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;
        
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        bitcoin = int.Parse(data[1]);
        exp = int.Parse(data[2]);
        if (GetCurrentLvl() != 1)
        {
          player.SetLvl(GetCurrentLvl());
        }
        weapon.setWeaponLvl(int.Parse(data[3]));

        Debug.Log("LoadState");
    }


}
