using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class acuur : MonoBehaviour
{
    public Text Accur;
    
    void Start()
    {
        Accur.text = "��� ���� ���� " + GameObject.Find("timestorage").GetComponent<timestorage>().thirdtext;
    }

    
}
