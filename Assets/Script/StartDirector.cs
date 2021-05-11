using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartDirector : MonoBehaviour
{
    public void change()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
            SceneManager.LoadScene("GameScene"); //임시 코드
    }
}