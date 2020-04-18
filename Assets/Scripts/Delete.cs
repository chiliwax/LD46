﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour
{
    [SerializeField] GameObject[] ToShow;
    [SerializeField] GameObject[] ToHide;
    // Start is called before the first frame update
    [SerializeField] ItemSlot Result;

    public void Onclick() {
        foreach (var item in ToShow)
            item.SetActive(true);
        foreach (var item in ToHide)
            item.SetActive(false);
        Result.Item = null;
        return;
        
    }
}