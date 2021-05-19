using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // LoadScene을 사용하기 위해서 필요!!

public class ClearDirector : MonoBehaviour
{
     public void RePlay()
    {
       
       {
            SceneManager.LoadScene("GameScene");
        }
    }


    public void RetunrToStart()
    {
        SceneManager.LoadScene("StartScene");
    }

}