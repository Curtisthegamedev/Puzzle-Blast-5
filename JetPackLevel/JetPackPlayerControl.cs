using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;

public class JetPackPlayerControl : MonoBehaviour
{
    [SerializeField] GameObject heart1, heart2, heart3, heart4, heart5, heart6, gunOne, gunTwo;

    private float speed = 10;
    int _points;
    private float moveX, moveY;

    [SerializeField] SpriteRenderer PlayerShipSprite;
    private Rigidbody2D rb; 

    private Collider2D myCollider;
    private bool isInvinsable = false;

    private int JetPackLife;

    private void Awake()
    {
        gunOne.SetActive(true);
        gunTwo.SetActive(true); 
    }

    private void Start()
    {
        myCollider = this.gameObject.GetComponent<Collider2D>();
        rb = this.gameObject.GetComponent<Rigidbody2D>(); 

        JetPackLife = 6;
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
        heart3.gameObject.SetActive(true);
        heart4.gameObject.SetActive(true);
        heart5.gameObject.SetActive(true);
        heart6.gameObject.SetActive(true);

    }

    private void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical"); 

        rb.velocity = new Vector2(moveX * speed, moveY * speed); 
    }

    private void Update()
    {
        if (JetPackLife > 6)
        {
            JetPackLife = 6; 
        }

        switch(JetPackLife)
        {
            case 6:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                heart4.gameObject.SetActive(true);
                heart5.gameObject.SetActive(true);
                heart6.gameObject.SetActive(true);
                break;
            case 5:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                heart4.gameObject.SetActive(true);
                heart5.gameObject.SetActive(true);
                heart6.gameObject.SetActive(false);
                break;
            case 4:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                heart4.gameObject.SetActive(true);
                heart5.gameObject.SetActive(false);
                heart6.gameObject.SetActive(false);
                break;
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                heart4.gameObject.SetActive(false);
                heart5.gameObject.SetActive(false);
                heart6.gameObject.SetActive(false);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                heart4.gameObject.SetActive(false);
                heart5.gameObject.SetActive(false);
                heart6.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                heart4.gameObject.SetActive(false);
                heart5.gameObject.SetActive(false);
                heart6.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                heart4.gameObject.SetActive(false);
                heart5.gameObject.SetActive(false);
                heart6.gameObject.SetActive(false);
                SceneManager.LoadScene("GameOverTwo"); 
                break; 
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "EProjectile")
        {
            Destroy(col.gameObject);
            if (!isInvinsable)
            {
                JetPackLife -= 1;
                StartCoroutine(TempInvinsibility());
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Obsticle")
        {
            JetPackLife = JetPackLife - 1; 
        }

        if (col.gameObject.tag == "EProjectile" && !isInvinsable)
        {
            Destroy(col.gameObject);
            JetPackLife -= 1;
            StartCoroutine(TempInvinsibility());
        }
    }

    private IEnumerator TempInvinsibility()
    {
        isInvinsable = true; 
        PlayerShipSprite.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(3);
        PlayerShipSprite.color = new Color(1f, 1f, 1f, 1f);
        isInvinsable = false; 
    }
}
