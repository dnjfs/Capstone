using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timestorage : MonoBehaviour
{
    public string firsttext;
    public string secondtext;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //firsttext = GameObject.Find("Timer").GetComponent<Timer>().alltime.ToString();

    }

    
    void Update()
    {
        
    }
}
