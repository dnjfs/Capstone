using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartDirector : MonoBehaviour
{

    public void change()
    {
        SceneManager.LoadScene("GameScene");//임시 코드
    }

    public void RightButton()
    {
        SceneManager.LoadScene("GameScene");

    }

    public void LeftButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ResultButton()
    {
        SceneManager.LoadScene("ClearScene");
    }
}