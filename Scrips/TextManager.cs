using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()
    {
        foreach (FloatingText txt in floatingTexts)
        {
            txt.UpdateFloatingText();
        }
    }

    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingTxt = GetFloatingText();

        floatingTxt.txt.text = msg;
        floatingTxt.txt.fontSize = fontSize;
        floatingTxt.txt.color = color;
        floatingTxt.go.transform.position = Camera.main.WorldToScreenPoint(position); //Transfer the world space to screen space so we can use it for the UI
        floatingTxt.motion = motion;
        floatingTxt.duration = duration;

        floatingTxt.Show();
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active);

        //for (int i = 0; i < floatingText.Count; i++)
        //{
        //    if (!floatingText[i].active)
        //        return txt;
        //}

        if (txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);
            txt.go.transform.SetParent(textContainer.transform);
            txt.txt = txt.go.GetComponent<Text>();

            floatingTexts.Add(txt);
        }

        return txt;
    }
}
