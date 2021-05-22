using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class flag : MonoBehaviour
{
    public float distance1;
    public float distance2;
    public bool isCalc;
    public float misangle;
    public float misAngleAvg;

    void Distance()
    {
        if (GameObject.FindWithTag("Doll") == null)
            return;
        Vector2 p1 = GameObject.FindWithTag("Doll").transform.position;
        Vector2 p2 = this.transform.position;
        Vector2 p3 = GameObject.FindWithTag("Player").transform.position;
        Vector2 dir = p2 - p1;
        Vector2 dir2 = p2 - p3;
        distance1 = dir.magnitude;
        distance2 = dir2.magnitude;
    }

    void Update()
    {
        Distance();
        if(distance1*0.9f < distance2)
        { 
            if(!isCalc)
                AngleCounting();
        }
    }

    private void AngleCounting()
    {
        isCalc = true;

        GameObject A = gameObject;
        GameObject B = GameObject.FindWithTag("Doll");
        GameObject C = GameObject.FindWithTag("Player");

        if (A == null || B == null || C == null) return;

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(A.transform.position, B.transform.position);

        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(A.transform.position, C.transform.position);

        Vector3 AB = B.transform.position - A.transform.position;
        Vector3 AC = C.transform.position - A.transform.position;

        //float cos = Vector3.Dot(AB, AC) / (AB.magnitude * AC.magnitude);
        //float cos_to_anlge = Mathf.Acos(cos) * Mathf.Rad2Deg;
        //Debug.LogFormat("angle between two Vectors -> cos {0}, angle {1}", cos, cos_to_anlge);

        float angle = Vector3.Angle(AC, AB);
        misangle += angle;
        misAngleAvg = misangle / 5;
        Debug.LogFormat("angle between two Vectors -> {0}", angle);
    }





}
