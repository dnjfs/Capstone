using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class Filename : MonoBehaviour
{
    public Text time1, time2;

    void Update()
    {
        //time1.text = DateTime.Now.ToString("HH:mm:ss");
        time2.text = DateTime.Now.ToString("tth:mm:ss");
    }
}
