using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject Destroy_Effect;
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.contacts[0].normal.y < 0.1f)
        {
       
            Instantiate(Destroy_Effect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }    
    }
}
