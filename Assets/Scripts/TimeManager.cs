using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public GameObject nightPanel = null;
    public GameObject[] ToHideNight;
    private int DayTime = 0;
    private int DayDuration = 10;
    private int WichDay = 0;

    public void IncraseTime()
    {
        if (DayTime >= DayDuration)
        {
            if (nightPanel)
                nightPanel.SetActive(true);
            foreach (var item in ToHideNight)
                item.SetActive(false);
        }
        else
        {
            DayTime += 1;
        }
    }

    public void ResetDay()
    {
        DayTime = 0;
        foreach (var item in ToHideNight)
            item.SetActive(true);
        if (nightPanel)
            nightPanel.SetActive(false);
    }

    public bool IsDay()
    {
        return DayTime < DayDuration;
    }

    public int getWichDay()
    {
        return WichDay;
    }
}
