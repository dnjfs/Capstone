using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class averagetime: MonoBehaviour
{
    
    public Text AverageTime;


    void Start()
    {
       
        AverageTime.text = "��սð� " + GameObject.Find("timestorage").GetComponent<timestorage>().secondtext;
    }


    void Update()
    {

    }
}
