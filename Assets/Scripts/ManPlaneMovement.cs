using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using static Unity.Mathematics.math;


public class ManPlaneMovement : MonoBehaviour
{
    // Init
    private GameObject o;
    private Camera c;
    private bool missileRight = false;
    [SerializeField] public GameObject Missile;
    [SerializeField] public GameObject Torpedo;
    private Vector2 move;
    [SerializeField] private float speed = 100;
    private Vector2 moveDir = Vector2.zero;
    private float fireTimer = 0;
    private bool cooldown;
    [SerializeField] private float cooldownTime = 0.3f;
    private Animator a;
    private Rigidbody2D r;
    
    private void Awake()
    {
        o = gameObject;
        c = Camera.main;
        a = o.GetComponent<Animator>();
        r = o.GetComponent<Rigidbody2D>();
    }
    
    // unity physics   
    void FixedUpdate()
    {
        moveDir = new Vector2((Input.GetKey(KeyCode.A) ? -1 : 0) + (Input.GetKey(KeyCode.D) ? 1 : 0),
            (Input.GetKey(KeyCode.S) ? -1 : 0) + (Input.GetKey(KeyCode.W) ? 1 : 0));
        
        moveDir = moveDir.normalized;

        if (moveDir.magnitude > 0)
        {
            a.SetFloat("x", moveDir.x);
            a.SetFloat("y", moveDir.y);
            a.SetBool("isMoving", true);
            r.velocity = moveDir * speed;
        }
        else
        {
            a.SetBool("isMoving", false);
            r.velocity = Vector2.zero;
        }
        
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
        //fire missles
        if ((Input.GetKeyDown(KeyCode.Space) || 0.5f < Input.GetAxis("Fire1")) && ! cooldown)
        {
            Instantiate(Missile, o.transform.position + new Vector3(missileRight ? 0.5f : -0.5f, 0, 0), Quaternion.identity);
            missileRight = ! missileRight;
            cooldown = true;
        }
        
        //fire torpedos
        if ((Input.GetKeyDown(KeyCode.LeftControl) || 0.5f < Input.GetAxis("Fire2")) && ! cooldown)
        {
            Instantiate(Torpedo, o.transform.position + new Vector3(missileRight ? 0.5f : -0.5f, 0, 0), Quaternion.identity);
            missileRight = ! missileRight;
            cooldown = true;
        }

        if (cooldown)
        {
            fireTimer += Time.deltaTime;
            if (fireTimer >= cooldownTime)
            {
                cooldown = false;
                fireTimer = 0;
            }
        }

    }
} 
