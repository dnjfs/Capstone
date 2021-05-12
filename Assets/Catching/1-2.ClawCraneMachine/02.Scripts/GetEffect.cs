using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GetEffect : MonoBehaviour
{
    public enum State { up, down};
    private State EffectState;
    private Vector3 originScale;
    private Vector3 impactScale;
    private float time;
    private float speed = 0.02f;
    public void Start()
    {
        originScale = this.transform.localScale;
        impactScale = originScale + (Vector3.one * 4f);
        EffectState = State.up;
    }
    public void Update()
    {
        switch (EffectState)
        {
            case State.up:
                UpScale();
                break;
            case State.down:
                DownScale();
                break;
            default:
                break;
        }
    }
    public void UpScale()
    { 
        if (time < 1)
        {
            time += speed;
            this.transform.localScale = Vector3.Lerp(originScale, impactScale, time);
        }
        else if (time > 1)
        {
            EffectState = State.down;
            time = 0;
        }
    }
    public void DownScale()
    {
        if (time < 1)
        {
            time += speed;
            this.transform.localScale = Vector3.Lerp(impactScale, originScale, time);
        }
        else if (time > 1)
        {
            EffectState = State.up;
            time = 0;
        }
    }
}
