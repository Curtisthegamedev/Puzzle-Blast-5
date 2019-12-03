using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovingLaser : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D rb;
    [SerializeField] GameObject particles; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "LimitMovementCollider" && 
            collision.gameObject.tag != "Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Trigger" && 
            collision.gameObject.tag != "LimitMovementCollider" && 
            collision.gameObject.tag != "Enemy" && 
            !collision.gameObject.CompareTag("BossFight") &&
            !collision.gameObject.CompareTag("BottomDroneTrigger"))
        {
            Debug.Log(collision.gameObject);
            Instantiate(particles, transform.position, transform.rotation); 
            Destroy(this.gameObject);
        }
    }
}
