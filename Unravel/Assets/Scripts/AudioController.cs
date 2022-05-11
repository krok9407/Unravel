using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource audio_;

    void Start()
    {
        if (!PlayerPrefs.HasKey("value")){
            audio_.volume = 1;
        }
    }

    void Update()
    {
        audio_.volume = PlayerPrefs.GetFloat("value");
    }
}
