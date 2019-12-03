using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        speed = 30;
        rb = this.gameObject.GetComponent<Rigidbody2D>(); 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "LaserBolt")
        {
            Destroy(this.gameObject);
        }
    }

    public void StartMoving()
    {
        rb.velocity = Vector2.left * speed; 
    }
}
