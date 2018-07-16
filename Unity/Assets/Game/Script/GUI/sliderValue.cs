using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sliderValue : MonoBehaviour {

    public Text display;
    public Slider slider;
    
    void Update()
    {
        display.text = slider.value.ToString();
    }

}
