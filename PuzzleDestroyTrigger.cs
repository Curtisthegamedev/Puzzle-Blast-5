using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PuzzleDestroyTrigger : MonoBehaviour
{
    [SerializeField] GameObject puzzleBoard;
    private bool playerIsHere = false; 
    [SerializeField] float timeTillDestruction;
    [SerializeField] Text TimeDisplayText; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerIsHere = true; 
        }
    }

    private void Update()
    {
        if(playerIsHere)
        {
            timeTillDestruction -= Time.deltaTime; 
            if(TimeDisplayText)
            {
                TimeDisplayText.text = "Puzzle will self destruct in: " + (int)timeTillDestruction; 
            }
            if(timeTillDestruction <= 0)
            {
                TimeDisplayText.text = " "; 
                Destroy(puzzleBoard); 
            }
        }
    }
}
