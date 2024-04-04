using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private GameObject clip;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Missile m = other.gameObject.GetComponent<Missile>();
        Debug.Log("Missile detected");
        if (m != null)
        {
            Destroy(m.gameObject);
            Instantiate(clip);
            Destroy(gameObject);
        }
    }
}
