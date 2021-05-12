using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine.UI;

public class SubMonitor : MonoBehaviour
{
    public Camera[] myCam = new Camera[2];
    // Start is called before the first frame update
    //public Slider brightSlider;
    //public Light directionalLight;
    
    void Start()
    {

        myCam[0] = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        myCam[1] = GameObject.FindGameObjectWithTag("SubCam").GetComponent<Camera>();
        /*
        for (int i = 0; i < Display.displays.Length; i++)
        {
            myCam[i].targetDisplay = i;
            Display.displays[i].Activate();
        }*/
        
        myCam[0].targetDisplay = 1;
        //Display.displays[0].Activate();
        myCam[1].targetDisplay = 0;
        //Display.displays[1].Activate();
    }
    private void Update()
    {
        //Screen.brightness = brightSlider.value;
        //directionalLight.intensity = brightSlider.value + .5f;

    }

}
