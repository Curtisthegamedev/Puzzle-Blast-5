using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{ 
    protected Vector2 thisObjectsPos;
    public SpriteRenderer thisSprite;
    string SpriteName;
    private void Start()
    {
        thisObjectsPos = transform.position;
    }

    void ReturnToPos()
    {
        transform.position = thisObjectsPos;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "OtherFruit")
        {
            Debug.Log("entered");
            SpriteName = col.gameObject.GetComponent<SpriteRenderer>().sprite.name;
        }
    }
    //private void OnCollisionEnter2D(Collision2D col)
    //{

    //}

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "OtherFruit")
        {
            Debug.Log("exited");
            thisSprite = GetComponent<SpriteRenderer>();
            thisSprite.sprite = (Sprite)Resources.Load<Sprite>("Sprites/" + SpriteName) as Sprite;
        }
    }

    //private void OnCollisionExit2D(Collision2D c)
    //{
        
    //}

    float distance = 10;
    private void OnMouseDrag()
    {
        //player can only move Tokens when the Game is not paused
        if(Time.timeScale != 0)
        {
            //lets the player drag the token they select while making sure 
            //that the sprite is visable on a layer above the others while draging
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objectPos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = objectPos;
            thisSprite.sortingOrder = 21;
        }
    }
    private void OnMouseUp()
    {
        thisSprite.sortingOrder = 2;
    }


}
