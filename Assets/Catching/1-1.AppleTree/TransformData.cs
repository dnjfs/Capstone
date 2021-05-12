using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransformData : MonoBehaviour
{
    //public Button getDataBtn;

    public List<Vector3> applePos = new List<Vector3>();
    public List<Quaternion> appleRot = new List<Quaternion>();

    /*
    public void GetData()
    {
        GameObject[] apples = GameObject.FindGameObjectsWithTag("dropper");
        for (int i = 0; i < apples.Length; i++)
        {
            Debug.Log(apples[i].name + ": " + apples[i].transform.position + ", " + apples[i].transform.rotation);
            applePos.Add(apples[i].transform.position);
            appleRot.Add(apples[i].transform.rotation);
        }
    }
    */
}
