using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringCtrl : MonoBehaviour
{
    public GameObject crane;

    public void Update()
    {
        //Debug.Log(crane.transform.localPosition.y);
        this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y, 0.5f * (6f - crane.transform.localPosition.y));
    }
}
