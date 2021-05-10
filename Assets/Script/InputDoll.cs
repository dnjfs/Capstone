using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDoll : MonoBehaviour
{
    [SerializeField] private InputField doll;
    

    public int doll_number;

    public void input()
    {
         doll_number = int.Parse(doll.text);
         Debug.Log(doll_number);

   
          DontDestroyOnLoad(gameObject);
        
    }

    
    

}
