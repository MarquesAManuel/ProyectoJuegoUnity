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
    //public Weapon weapon;
    public TextManager textManager;

    //Logica
    public int bitcoin;
    public int exp;

    
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        textManager.Show(msg, fontSize, color, position, motion, duration);
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
        s += "0"; 

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
        //Change weapon lvl

        Debug.Log("LoadState");
    }


}
