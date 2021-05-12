using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClawCMngr : MonoBehaviour
{
    [SerializeField]
    private float startTime;
    private float moveTime;

    public Text timeTxt;
    public ClawCtrl _clawCtrl;
    public GameObject btnPanel;
    public GameObject btnPanel1;
    public static bool isPickUp;
    public static bool isGameBtn;
    public GameObject endingUI;
    public AnimCtrl _animCtrl;

    // Start is called before the first frame update
    void Start()
    {
        GameSet();
    }
    public void GameSet()
    {
        startTime = 2.99f;
        moveTime = 5f;
        timeTxt.enabled = true;
        isPickUp = true;
        isGameBtn = false;
        btnPanel.SetActive(false);
        btnPanel1.SetActive(false);
        endingUI.SetActive(false);
        _animCtrl.DestroyObj();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        //starttime
        if (startTime > 0)
        {
            startTime -= Time.deltaTime;
            if (startTime <= 1f)
                timeTxt.text = "Game Start";
            else
                timeTxt.text = Mathf.Ceil(startTime - 1f).ToString();
            _clawCtrl.clawState = ClawCtrl.State.stop;
        }//moving
        else if (moveTime > 0)
        {
            moveTime -= Time.deltaTime;
            timeTxt.text = Mathf.Ceil(moveTime).ToString();
            _clawCtrl.clawState = ClawCtrl.State.move;
            if (moveTime <= 0)
            {
                timeTxt.enabled = false;
                ClawCtrl.once = true;
            }
        }//pickup
        else if (isPickUp)
        {
            _clawCtrl.clawState = ClawCtrl.State.down;

        }//game button on
        else
        {
            btnPanel.SetActive(true);
            btnPanel1.SetActive(true);
        }

        if (_animCtrl.catchAnim.GetComponent<Animator>().GetBool("isPlaying"))
        {
            endingUI.SetActive(true);
            //Debug.Log("isplaying = true");
        }
        
        


        if (Input.GetMouseButtonDown(0))
        {
            
        }
    }

    public void ReStart()
    {
        //SceneManager.LoadScene("ClawCraneMachine");
        _clawCtrl.GameSet();
        GameSet();
    }
    public void GameQuit()
    {
        Application.Quit();
    }
}
