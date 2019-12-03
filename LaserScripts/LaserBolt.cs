using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBolt : MonoBehaviour
{
    private float speed = 20f;
    private Rigidbody2D rb;
    [SerializeField] ParticleSystem ps; 
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.right * speed; 
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject); 
        if (collision.gameObject.tag != "Trigger" && collision.gameObject.tag != "Player" 
            && collision.gameObject.tag != "LimitMovementCollider")
        {
            if(ps)
            { 
                Instantiate(ps, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("there are no particles"); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Trigger" && collision.gameObject.tag != "Player"
            && collision.gameObject.tag != "LimitMovementCollider" && collision.gameObject.tag !=
            "TopDroneTrigger" && collision.gameObject.tag != "BottomDroneTrigger" 
            && collision.gameObject.tag != "Gem" && collision.gameObject.tag != "BossFight")
        {
            if (ps)
            {
                Instantiate(ps, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("there are no particles");
            }
        }
    }
}
