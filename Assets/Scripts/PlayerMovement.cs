using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using static Unity.Mathematics.math;


public class PlayerMovement : MonoBehaviour
{
    // Init
    private GameObject o;
    private Vector3 MoveSpeed = Vector3.zero;
    private float MaxSpeed = 0.04f;
    private float Accel = 0.05f;
    private float Drag = 0.1f;
    private Camera c;
    private bool missileRight = false;
    [SerializeField]
    public GameObject Missile;
    private void Awake()
    {
        
        o = gameObject;
        c = Camera.main;
    }
    
    // Update is called once per frame
    void Update()
    {
        //store input - using ternary to essentially cast a bool to a float (this only works for binary directional inputs)
        Vector3 move = new ((Input.GetKey("d") ? 1.0f : 0.0f) - (Input.GetKey("a") ? 1.0f : 0.0f), 
            (Input.GetKey("w") ? 1.0f : 0.0f) - (Input.GetKey("s") ? 1.0f : 0.0f),
            0);
        
        /*
         * normalizing the directional vector - there is only one type of non-axial movement so we can just multiply
         * by literal 1 over root 2 to save performance (this only works for binary directional inputs)
         */
        if (abs(move.x) + abs(move.y) == 2.0f)
        {
            move *= 0.707106781f;
        }
        
        //update speed
        if(move.x != 0 || move.y != 0)
        {
            MoveSpeed += move * (Accel * Time.deltaTime);

            //clamp max velocity
            if(MoveSpeed.magnitude > MaxSpeed) MoveSpeed = MoveSpeed.normalized * MaxSpeed;
        }
        else if (MoveSpeed != Vector3.zero) MoveSpeed -= MoveSpeed / Drag * Time.deltaTime ;
        
        
        //bounds checking
        Vector3 cameraSpace = c.WorldToScreenPoint(o.transform.position + MoveSpeed);
        
        if (cameraSpace.y < 0 || cameraSpace.y > c.pixelHeight)
        {
            MoveSpeed.y = 0;
        }

        if (cameraSpace.x < 0 || cameraSpace.x > c.pixelWidth)
        {
            MoveSpeed.x = 0;
        }
            
        
        //set position
        o.transform.position += MoveSpeed;


        //fire missles
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Missile, o.transform.position + new Vector3(missileRight ? 0.3f : -0.3f, 0, 0), Quaternion.identity);
            missileRight = ! missileRight;
        }

    }
}
