using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filler : MonoBehaviour
{
    public float maxCount;
    float count = 0;
    public Image image;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "_point")
        {
            count++;
            image.fillAmount = count / maxCount;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "_point")
        {
            count--;
            image.fillAmount = count / maxCount;
        }
    }
}
