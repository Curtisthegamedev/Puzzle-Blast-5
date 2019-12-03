using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class EnemyHealth : MonoBehaviour
{
    //enemys int health value 
    public int health = 5;
    //this is the enemy's health bar
    public bool enemyhealthChanged = false; 
    public RectTransform MushroomHealthBar;
    [SerializeField] bool isBossTypeEnemy;
    private bool hasStartedEnemyBossDeath = false; 
    //this variable is used to set the size of the health bar. 
    private int scale;
    [SerializeField] GameObject gem, healthDisplay; 
    [SerializeField] ParticleSystem enemyMonsterParticles;
    [SerializeField] GameObject explotion; 
    [SerializeField] Transform spotOne, spotTwo, SpotThree;
    [SerializeField] SpriteRenderer BossSprite;

    private void Start()
    {
        if(MushroomHealthBar)
        {
            //set the scale size of the health bar to go down when health is taken away
            scale = (int)MushroomHealthBar.sizeDelta.x / health; 
        }
    }

    private void OnCollisionEnter2D(Collision2D c)
    {
        //if the enemy collides with the sword, laserBolt or something with a death tag 
        //they take verying levels of damage.
        if(c.gameObject.tag == "SwordWeapon")
        {
            health -= 1; 
        }
        if(c.gameObject.tag == "LaserBolt")
        {
            Debug.Log("laserdetected"); 
            health -= 3; 
        }
        if(c.gameObject.tag == "Death")
        {
            health = 0; 
        }
        
        //chnages the healthbar size dependin on the scale variable and health variable
        MushroomHealthBar.sizeDelta = new Vector2(health * scale, MushroomHealthBar.sizeDelta.y); 
    }

    private IEnumerator waitAndDestroy()
    {
        BossSprite.color = new Color(1.0f, 1.0f, 1.0f, 0);
        Instantiate(explotion, transform.position, transform.rotation);
        healthDisplay.SetActive(false); 
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("LevelTwoWin"); 
    }

    private void Update()
    {
        if(enemyhealthChanged)
        {
            //changes the healthbar size dependin on the scale variable and health variable
            MushroomHealthBar.sizeDelta = new Vector2(health * scale, MushroomHealthBar.sizeDelta.y);
            enemyhealthChanged = false; 
        }
        //kill the enemy is their health is zero. 
        if (health <= 0)
        {
            if(enemyMonsterParticles)
            {
                Instantiate(enemyMonsterParticles, transform.position, transform.rotation);
                Instantiate(gem, spotOne.position, spotOne.rotation);
                Instantiate(gem, spotTwo.position, spotTwo.rotation);
                Instantiate(gem, SpotThree.position, SpotThree.rotation);
                if (isBossTypeEnemy)
                {
                    Debug.Log("sceneload");
                    SceneManager.LoadScene("LevelTwoWin");
                }
                Destroy(this.gameObject);
            }
            else if(explotion && !isBossTypeEnemy)
            {
                Instantiate(explotion, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }

            else if(explotion && isBossTypeEnemy && !hasStartedEnemyBossDeath)
            {
                StartCoroutine(waitAndDestroy());
                hasStartedEnemyBossDeath = true; 
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        //if the enemy collides with the ball they lose health 
        if (c.gameObject.tag == "SwordWeapon")
        {
            health -= 1;
        }
        if (c.gameObject.tag == "LaserBolt")
        {
            health -= 3;
        }

        //changes the healthbar size dependin on the scale variable and health variable
        MushroomHealthBar.sizeDelta = new Vector2(health * scale, MushroomHealthBar.sizeDelta.y);
    }
}
