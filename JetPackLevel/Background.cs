using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private Vector2 moveLeft = new Vector2(-10, 0);
    private Rigidbody2D rb;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        rb.MovePosition(rb.position + moveLeft * Time.deltaTime); 
    }
}
