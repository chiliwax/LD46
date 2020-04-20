using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject nightPanel = null;
    public GameObject[] ToHideNight;
    private int DayTime = 0;

    [SerializeField]
    [Range(10,100)]
    private int DayDuration = 20;
    private int WichDay = 0;

    public void IncraseTime()
    {
        Debug.Log(WichDay);
        if (DayTime >= DayDuration)
        {
            WichDay += 1;
            DayTime = 0;
            if (nightPanel)
                nightPanel.SetActive(true);
            foreach (var item in ToHideNight) {
            //   item.SetActive(false);
            }
        }
        else
        {
            DayTime += 1;
        }
    }

    public void IncraseDay() {
        WichDay += 1;
        DayTime = 0;
    }

    public void ResetDay()
    {
        foreach (var item in ToHideNight)
            item.SetActive(true);
        if (nightPanel)
            nightPanel.SetActive(false);
    }

    public int getWichDay()
    {
        return WichDay;
    }
}
