using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollition : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject); 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(this.gameObject); 
    }
}
