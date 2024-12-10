using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSliderCode : MonoBehaviour
{
    public float time = 1.0f;

    void Update()
    {
        Time.timeScale = time;
    }

    public void AdjustTime(float newTime)
    {
        time = newTime;
    }

    public void PauseTime()
    {
        time = 0;
    }

    public void ResumeTime()
    {
        time = 1;
    }

 
}
