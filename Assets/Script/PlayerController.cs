using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    public int a = 0;
    public int goal = 5;
    public float distance;

    public bool isStart;
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        MousePosition.z = 0f;
        this.transform.position = MousePosition;
    }

   



    /*
    Rigidbody2D rigid2D;
    private Vector3 m_Offset;
    private float m_ZCoord;

    
    void OnMouseDown()
    {
        m_ZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        m_Offset = gameObject.transform.position - GetMouseWorldPosition();
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + m_Offset;
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = m_ZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    */

    void OnCollisionEnter2D(Collision2D coll)
    {
        
        if (coll.gameObject.tag == "Doll")
        {
            anim.SetTrigger("Catch");

            coll.gameObject.transform.parent = this.transform;
            coll.gameObject.transform.position = this.transform.position + new Vector3(0, 0.5f, 0);

            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();

        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Finish")
        {
            if (!this.transform.GetChild(0).gameObject.activeSelf) //자식 객체가 비활성화 되어 있으면
            {
                this.transform.GetChild(0).gameObject.SetActive(true); //자식 객체 활성화
                GameObject.Find("CatGenerator").GetComponent<CatGenerator>().Generate();
            }
            else if (this.transform.childCount >= 2)
            {
                a++;
                //goal = GameObject.Find("GameManager").GetComponent<InputDoll>().doll_number; //목표 개수 받아옴
                if (a >= goal)
                {
                    SceneManager.LoadScene("ClearScene");
                    return;
                }
                Destroy(this.transform.GetChild(1).gameObject);


                GameObject.Find("CatGenerator").GetComponent<CatGenerator>().Generate();
                Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
                timer.timerOn = false;
                timer.AddAll();
                //Debug.Log(timer.a);
                timer.TimerReset();
            }
        }
    }


}