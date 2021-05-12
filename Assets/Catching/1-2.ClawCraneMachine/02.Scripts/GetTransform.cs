using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetTransform : MonoBehaviour
{
    public Transform[] dolls;
    public Vector3[] dollPos;
    public Quaternion[] dollQuat;

    public void GetTransBtn()
    {
        for (int i = 0; i < dolls.Length; i++)
        {
            dollPos[i] = dolls[i].localPosition;
            dollQuat[i] = dolls[i].localRotation;
        }
    }
    public void Start()
    {
        for (int i = 0; i < dolls.Length; i++)
        {
            dolls[i].localPosition = dollPos[i];
            dolls[i].localRotation = dollQuat[i];
        }
    }
    public void Update()
    {
        GetTransBtn();
    }
}
