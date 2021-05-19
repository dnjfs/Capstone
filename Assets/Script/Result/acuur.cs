using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class acuur : MonoBehaviour
{
    public Text Accur;
    
    void Start()
    {
        Accur.text = "평균 오차 각도 " + GameObject.Find("timestorage").GetComponent<timestorage>().thirdtext;
    }

    
}
