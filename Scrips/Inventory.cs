using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //Text fields
    public Text levelText, hitpointText, bitcoinText, upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weapongSprite;
    public RectTransform xpBar;

    //Character selection
    public void onArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            //If we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
            {
                currentCharacterSelection = 0;
            }

            onSelectionChange();

        }
        else
        {
            currentCharacterSelection--;

            //If we went too far away
            if (currentCharacterSelection < 0)
            {
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
            }

            onSelectionChange();
        }
    }

    private void onSelectionChange()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void onUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            updateMenu();

    }

    //Update character information
    public void updateMenu()
    {
        //Weapon
        weapongSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.weapon.weaponLvl];
        if (GameManager.instance.weapon.weaponLvl == GameManager.instance.weaponPrices.Count)
        {
            upgradeCostText.text = "MAX LVL";
        }
        else
        {
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.weapon.weaponLvl].ToString();
        }


        //Meta
        levelText.text = GameManager.instance.GetCurrentLvl().ToString();
        hitpointText.text = GameManager.instance.player.hitpoints.ToString();
        bitcoinText.text = GameManager.instance.bitcoin.ToString();

        //XP bar
        int currentLvlXp = GameManager.instance.GetCurrentLvl();
        if (currentLvlXp == GameManager.instance.expTable.Count)
        {
            xpText.text = GameManager.instance.exp.ToString() + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLvlXp = GameManager.instance.GetXpToLvl(currentLvlXp - 1);
            currentLvlXp = GameManager.instance.GetXpToLvl(currentLvlXp);

            int diff = currentLvlXp - prevLvlXp;
            int currentXpIntoLvl = GameManager.instance.exp - prevLvlXp;

            float completionRatio = (float)currentXpIntoLvl / (float)diff;
            xpBar.localScale = new Vector3(completionRatio, 1, 1);
            xpText.text = currentXpIntoLvl.ToString() + " / " + diff;
        }

    }
}
