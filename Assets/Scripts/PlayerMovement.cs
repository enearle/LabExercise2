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
    private Camera c;
    private bool missileRight = false;
    [SerializeField] public GameObject Missile;
    [SerializeField] public GameObject Torpedo;
    private Vector2 move;
    private float mass = 5f, force = 400, friction = 300, maxSpeed = 5;
    private Vector2 accel = Vector2.zero;
    private Vector2 velocity = Vector2.zero;
    private float fireTimer = 0;
    private bool cooldown;
    [SerializeField] private float cooldownTime = 0.3f;
    
    private void Awake()
    {
        o = gameObject;
        c = Camera.main;
    }
    
    // proper physics code    
    void MovePlayer()
    {
        accel = (move * force - velocity.normalized * friction) / mass;

        velocity += accel * Time.deltaTime;
        
        Vector3 movement = (Vector3)(velocity * Time.deltaTime + accel * Time.deltaTime * Time.deltaTime / 2);
        
        Vector3 cameraSpace = c.WorldToScreenPoint(o.transform.position + movement);
        
        if (cameraSpace.y < 0 || cameraSpace.y > c.pixelHeight)
        {
            movement.y = 0;
        }

        if (cameraSpace.x < 0 || cameraSpace.x > c.pixelWidth)
        {
            movement.x = 0;
        }
        
        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;
        
        if (move == Vector2.zero && velocity.magnitude <= 0.01f)
            velocity = Vector2.zero;
        
        o.transform.position += movement;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        //store input
        move = new (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        //normalize input
        move = move.normalized;
        
        //update speed
        MovePlayer();

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
