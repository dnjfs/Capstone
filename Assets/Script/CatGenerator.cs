using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGenerator : MonoBehaviour
{
    public GameObject CatPrefab;
    

    void Update()
    {
       
       
    }

   public void Generate()
    {
        Invoke("example", 2f);

       
        
        
    }

    void example()
    {
        GameObject go = Instantiate(CatPrefab) as GameObject;
        float px = 0, py = -2;
        while ((-2 < px && px < 2) && (py < 0))
        {
            px = Random.Range(-7f, 7f);
            py = Random.Range(-3f, 5f);
        }

        go.transform.position = new Vector3(px, py, 0);


        GameObject.Find("Timer").GetComponent<Timer>().timerOn = true;
        
    }

}