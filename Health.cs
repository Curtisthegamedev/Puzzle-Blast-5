using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class Health : MonoBehaviour
{
    //sets variables eqaul to game objects attached in unity. I placed different heart images of a heart depleting here. 
    [SerializeField] GameObject heart, heart1, heart2;
    //creats player life variable.  
    public static int life;
    private float gameOverTimer = 3;
    private bool playerHasDied = false;
    private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }
    void Start()
    {
        //sets all parts of health active at the start.  
        life = 3;
        heart.gameObject.SetActive(true);
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(true);
    }
    private void waitThenGameOver()
    {
        gameOverTimer -= Time.deltaTime;
    }

    void Update()
    {
        if (life > 3)
        {
            life = 3;
        }
        else if (life < 0)
        {
            Debug.LogError("LIFE BELOW ZERO IN HEALTH SCRIPT"); 
        }

        if(playerHasDied)
        {
            waitThenGameOver(); 
        }

        if(gameOverTimer <= 0)
        {
            SceneManager.LoadScene("GameOver"); 
        }
        switch (life)
        {
            //these case statements erase a heart image causeing the heart in game to empty every time life goes down one. 
            case 3:
                heart.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                break;
            case 2:
                heart.gameObject.SetActive(true);
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                break;
            case 1:
                heart.gameObject.SetActive(true);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                break;
            case 0:
                playerHasDied = true;
                player.SetActive(false); 
                heart.gameObject.SetActive(false);
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                break;
        }
    }
}
