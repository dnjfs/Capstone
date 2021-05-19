using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultText : MonoBehaviour
{
    public Text TotalTime;



    void Start()
    {
        TotalTime.text = "ÃÑ °É¸° ½Ã°£" + GameObject.Find("timestorage").GetComponent<timestorage>().firsttext;


    }
    
}
