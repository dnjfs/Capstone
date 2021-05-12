using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;


public class ClawCtrl : MonoBehaviour
{
    public Camera subCam;
    public GameObject claw;
    public enum State { stop, move, down }
    public State clawState;
    //private GameObject player;
    //private Rigidbody myRigidbody;
    private float screenX = 2.53f; //-2.73~2.73
    private float screenY = 1.1f; //-1.32 ~ 1.68
    private float clawHeight; //2.15~4.3, dif = 2.15f
    private Vector3 inputMousePoint;
    private Vector3 playerScreenPoint;
    private Vector3 clawPos;
    private Vector3 prevPos;
    private Vector3 difPos;
    private Vector3 joyStickQ;
    public GameObject joyStick;
    private Vector3 prevDownPos;
    public static bool once;
    public static bool isCheckColl;
    private float speed = 0.005f;
    private float currentDistance;
    private float pickUpDistance;
    private float grabRate;
    public Transform[] legs;
    public Animator catchAnim;
    public Collision[] legsColls;
    public GameObject lineObj;
    public GameObject moveSetXObj;
    public GameObject moveSetYObj;
    public Animator clawAnim;

    private void Start()
    {
        //player = this.gameObject;
        //myRigidbody = GetComponentInChildren<Rigidbody>();
        subCam = GameObject.FindGameObjectWithTag("SubCam").GetComponent<Camera>();
        //clawheight 샘플
        GameSet();
    }
    public void GameSet()
    {
        clawHeight = 5.76f;
        isCheckColl = false;
        currentDistance = 0;
        grabRate = 0;
        pickUpDistance = 0;
        //once = true;
        catchAnim.SetBool("isPlaying", false);
        clawAnim.SetBool("isPlaying", false);
        for (int i = 0; i < legs.Length; i++)
        {
            legs[i].localEulerAngles = new Vector3(5, legs[i].localEulerAngles.y, legs[i].localEulerAngles.z);
            
        }
    }
    public void Update()
    {
        switch (clawState)
        {
            case State.stop:
                //Debug.Log("state stop");
                break;
            case State.move:
                //Debug.Log("state move");
                ClawMove();
                break;
            case State.down:
                //Debug.Log("state down");
                PickUp();
                break;
            default:
                break;
        }
        /*
        if (!catchAnim.isActiveAndEnabled)
        {
            Debug.Log(catchAnim.GetComponent<GameObject>().GetComponentInChildren<GameObject>().name);
            DestroyImmediate(catchAnim.GetComponent<GameObject>().GetComponentInChildren<GameObject>());
        }*/
    }
    public void PickUp()
    {
        if (once)
        {
            once = !once;
            prevDownPos = claw.transform.position;
            //Debug.Log("once");
            //Debug.Log(prevDownPos);
        }//move down
        if (currentDistance <= 1)
        {
            //Debug.Log("currentDistance");
            currentDistance += speed;
            claw.transform.position = Vector3.Lerp(prevDownPos, prevDownPos + Vector3.down * 2.46f, currentDistance);
        }//grabing
        else if (grabRate <= 1)
        {
            clawAnim.SetBool("isPlaying", true);
            //Debug.Log("grabRate");
            grabRate += speed * 1.5f;
            
            for (int i = 0; i < legs.Length; i++)
            {
                legs[i].localEulerAngles = Vector3.Lerp(new Vector3(5, legs[i].localEulerAngles.y, legs[i].localEulerAngles.z), new Vector3(-13, legs[i].localEulerAngles.y, legs[i].localEulerAngles.z), grabRate);
            }
        }//move up
        else if (pickUpDistance <= 1)
        {
            pickUpDistance += speed * 1.5f;
            claw.transform.position = Vector3.Lerp(prevDownPos + Vector3.down * 2.46f, prevDownPos, pickUpDistance);
        }
        else //gamebtn on
        {
            ClawCMngr.isPickUp = false;//whenever it has finished picking up
            ClawCMngr.isGameBtn = true;
            
        }
        
    }
    public void ClawMove()
    {
        //-.5f~.5f
        inputMousePoint = subCam.GetComponent<Camera>().ScreenToViewportPoint(Input.mousePosition) - new Vector3(.5f, .5f, 0);
        if (inputMousePoint.x >= .5f)
            inputMousePoint = new Vector3(.5f, inputMousePoint.y, inputMousePoint.z);
        else if (inputMousePoint.x <= -.5f)
            inputMousePoint = new Vector3(-.5f, inputMousePoint.y, inputMousePoint.z);
        if (inputMousePoint.y >= .5f)
            inputMousePoint = new Vector3(inputMousePoint.x, .5f, inputMousePoint.z);
        else if (inputMousePoint.y <= -.5f)
            inputMousePoint = new Vector3(inputMousePoint.x, -.5f, inputMousePoint.z);

        //여백
        playerScreenPoint = new Vector3(inputMousePoint.x * 7.9f, clawHeight, inputMousePoint.y * 4.4f);
        //Debug.Log(playerScreenPoint);

        clawPos = playerScreenPoint;
        //보정
        if (clawPos.x <= -screenX)
            clawPos = new Vector3(-screenX, clawHeight, clawPos.z);
        else if (clawPos.x >= screenX)
            clawPos = new Vector3(screenX, clawHeight, clawPos.z);
        if (clawPos.z <= -1.13f)
            clawPos = new Vector3(clawPos.x, clawHeight, -1.13f);
        else if (clawPos.z >= 1.18f)
            clawPos = new Vector3(clawPos.x, clawHeight, 1.18f);

        //while it's moving, joystick react
        claw.transform.position = clawPos;
        difPos = clawPos - prevPos;
        if (difPos.sqrMagnitude > 0.001f)
        {
            joyStickQ = difPos / difPos.magnitude * difPos.sqrMagnitude * 300f;
            joyStickQ = new Vector3(-joyStickQ.z, -joyStickQ.x, -joyStickQ.y);
            
            if (joyStickQ.x >= 15)
                joyStickQ = new Vector3(15, joyStickQ.y, joyStickQ.z);
            else if (joyStickQ.x <= -15)
                joyStickQ = new Vector3(-15, joyStickQ.y, joyStickQ.z);
            if (joyStickQ.y >= 15)
                joyStickQ = new Vector3(joyStickQ.x, 15, joyStickQ.z);
            else if (joyStickQ.y <= -15)
                joyStickQ = new Vector3(joyStickQ.x, -15, joyStickQ.z);
            if (joyStickQ.z >= 15)
                joyStickQ = new Vector3(joyStickQ.x, joyStickQ.y, 15);
            else if (joyStickQ.z <= -15)
                joyStickQ = new Vector3(joyStickQ.x, joyStickQ.y, -15);
            
        }
        else
            joyStickQ = Vector3.zero;
        joyStick.transform.localRotation = Quaternion.Euler(joyStickQ);

        //Debug.Log(difPos.sqrMagnitude);
        prevPos = clawPos;

        //line, moveset move
        lineObj.transform.position = claw.transform.position + Vector3.up * 0.15f;
        moveSetXObj.transform.position = new Vector3(claw.transform.position.x, moveSetXObj.transform.position.y, moveSetXObj.transform.position.z);
        moveSetYObj.transform.position = new Vector3(moveSetYObj.transform.position.x, moveSetYObj.transform.position.y, claw.transform.position.z);
    }
}
