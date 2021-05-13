using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour


{
    Animator animator;
    public int a = 0;
    public int goal = 3;

    void start()
    {
        this.animator = GetComponent<Animator>();
        this.animator.speed = 0;
       

    }

    public void Update()
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
            
            coll.gameObject.transform.parent = this.transform;
            coll.gameObject.transform.position = this.transform.position + new Vector3(0, -1f, 0);

            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp();

        }
        else if (coll.gameObject.tag == "Finish")
        {
            if(this.transform.childCount >= 1)
            {

                a++;
                //goal = GameObject.Find("GameManager").GetComponent<InputDoll>().doll_number; //목표 개수 받아옴
                if (a >= goal)
                {
                    SceneManager.LoadScene("ClearScene");
                    return;
                }
                Destroy(this.transform.GetChild(0).gameObject);

                
                GameObject.Find("CatGenerator").GetComponent<CatGenerator>().Generate();
                Timer timer = GameObject.Find("Timer").GetComponent<Timer>();
                timer.timerOn = false;
                timer.AddAll();
                Debug.Log(timer.a);
                timer.TimerReset();



            }

            

           
        }

    }
}

//    void OnTriggerEnter2D(Collider2D other)
//    {
       
//        Debug.Log("골");
//        SceneManager.LoadScene("ClearScene");
//    }

//}