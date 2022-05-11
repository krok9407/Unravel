using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float oldVolume;
    
    void Start()
    {

        oldVolume = slider.value;
        if(!PlayerPrefs.HasKey("value")){
            slider.value = 1;
        }
        else{
            slider.value = PlayerPrefs.GetFloat("value");
        }
    }

    void Update()
    {
        if(oldVolume != slider.value){
            PlayerPrefs.SetFloat("value", slider.value);
            PlayerPrefs.Save();
            oldVolume = slider.value;
        }
    }
}
