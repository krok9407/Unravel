using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    bool visText;
    public bool buttonPause;
    public Text text;
    public int level;
    public GameObject Panel;
    public GameObject Points;
    public GameObject Lines;
    public GameObject Pause_;

    void Start()
    {
        if (buttonPause)
            StartCoroutine(Mess());
    }

    IEnumerator Mess()
    {
        visText = true;
        ShowText();
        yield return new WaitForSeconds(2);
        visText = false;
        text.text = "";
        this.GetComponent<Image>().enabled = true;
    }

    void ShowText()
    {
        if (visText)
        {
            text.text = "Level " + level;
        }
    }

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            bool visGO = Points.activeSelf;

            Panel.SetActive(!isActive);
            Points.SetActive(!visGO);
            Lines.SetActive(!visGO);
            Pause_.SetActive(!visGO);
        }
    }
}
