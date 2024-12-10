using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueText : MonoBehaviour
{
    private Slider slider;
    private Text textComp;
   
    void Start()
    {
        UpdateText(slider.value);
        slider.onValueChanged.AddListener(UpdateText);
    }
    void Awake()
    {
        slider = GetComponentInParent<Slider>();
        textComp = GetComponentInParent<Text>();
    }
    void UpdateText(float val)
    {
        textComp.text = slider.value.ToString();
    }
}
