using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catcontroller : MonoBehaviour
{
    GameObject player;

    void Start()
    {
        this.player = GameObject.Find("player");
    }

    void Update()
    {

        

        //// 충돌 판정
        //Vector2 p1 = transform.position;              
        //Vector2 p2 = this.player.transform.position;  // 플레이어의 중심 좌표
        //Vector2 dir = p1 - p2;
        //float d = dir.magnitude;
        //float r1 = 0.5f;  
        //float r2 = 1.0f;  // 플레이어의 반경

        //if (d < r1 + r2)
        //{
        //    // 충돌한 경우는 인형을지운다

        //    Destroy(gameObject);
        //}
    }
}