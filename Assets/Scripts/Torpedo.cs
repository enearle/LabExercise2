using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torpedo : MonoBehaviour
{
    // Init
    private Camera c;
    private GameObject o;

    private float timer = 0;
    void Awake()
    {
        c = Camera.main;
        o = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
        o.transform.position = gameObject.transform.position + new Vector3(0, 10, 0) * Time.deltaTime;
            
        Vector3 cameraSpace = c.WorldToScreenPoint(o.transform.position);
            
        if (cameraSpace.y < 0 || cameraSpace.y > c.pixelHeight || cameraSpace.x < 0 || cameraSpace.x > c.pixelWidth)
        {
            Destroy(o);
        }
        /*
        if (timer >= 1)
        {
            o.transform.position = gameObject.transform.position + new Vector3(0, 10, 0) * Time.deltaTime;
            
            Vector3 cameraSpace = c.WorldToScreenPoint(o.transform.position);
            
            if (cameraSpace.y < 0 || cameraSpace.y > c.pixelHeight || cameraSpace.x < 0 || cameraSpace.x > c.pixelWidth)
            {
                Destroy(o);
            }
        }
        else
        {
            timer += Time.deltaTime;
        }*/
    }
}
