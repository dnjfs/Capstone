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
    
}