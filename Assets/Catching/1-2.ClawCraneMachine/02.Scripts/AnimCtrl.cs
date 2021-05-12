using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    [SerializeField]
    private GameObject catchedObj;
    public GameObject catchAnim;
    private Vector3 pos = new Vector3(0, -0.42f, 0.2f);
    private Vector3 rot = new Vector3(-95, 0, 180);

    public void Start()
    {
        catchedObj = null;
    }
    [Obsolete]
    public void OnTriggerStay(Collider other)
    {
        //Debug.Log("stay");
        //ContactPoint contact = collision.contacts[0];
        //Debug.Log(other.name);
        catchedObj = other.gameObject;
        catchedObj.transform.parent = catchAnim.transform;
        catchedObj.layer = 12;
        catchedObj.transform.localPosition = pos;
        catchedObj.transform.localEulerAngles = rot;
        Destroy(catchedObj.GetComponent<Rigidbody>());
        catchAnim.GetComponent<Animator>().SetBool("isPlaying", true);
    }
    public void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit " + other.name);
        catchedObj = null;

    }
    public void DestroyObj()
    {
        //Debug.Log("destroy");
        //Destroy(catchedObj);
        for (int i = 0; i < catchAnim.GetComponentsInChildren<MeshCollider>().Length; i++)
        {
            Destroy(catchAnim.GetComponentsInChildren<MeshCollider>()[i].gameObject);
        }
    }
}

