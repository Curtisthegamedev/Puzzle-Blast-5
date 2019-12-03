using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserParticleSystem : MonoBehaviour
{
    [SerializeField] ParticleSystem ps; 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Trigger" && collision.gameObject.tag != "LimitMovementCollider")
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Trigger" && collision.gameObject.tag != "LimitMovementCollider")
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
