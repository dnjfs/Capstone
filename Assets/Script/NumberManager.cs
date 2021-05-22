using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberManager : MonoBehaviour
{
    public Text number;

    public int leftnumber = 5;


    void Update()
    {
        number.text = leftnumber.ToString();
    }

     public void Decrease()
    {
        leftnumber--;
    }
}