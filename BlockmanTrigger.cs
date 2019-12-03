using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockmanTrigger : MonoBehaviour
{
    private Rigidbody2D blockManRB;
    private Vector2 downForce = new Vector2(0, -500);
    private float speed = 6.0f;
    private bool startRiseing = false;
    private Vector3 startPos;
    [SerializeField] GameObject BlockMan;   

    private void Awake()
    {
        blockManRB = this.gameObject.GetComponentInParent<Rigidbody2D>();
    }
    private void Start()
    {
        startPos = BlockMan.transform.position; 
    }
    private IEnumerator CrushAndRiseUp()
    {
        startRiseing = false; 
        blockManRB.AddForce(downForce);
        yield return new WaitForSeconds(3.0f);
        startRiseing = true; 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("player"); 
            StartCoroutine(CrushAndRiseUp()); 
        }
    }

    private void Update()
    {
        if(startRiseing)
        {
            BlockMan.transform.position = Vector3.MoveTowards(BlockMan.transform.position, 
                startPos, speed * Time.deltaTime);
        }
    }
}
