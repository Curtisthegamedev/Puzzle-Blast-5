using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement; 

public class Player : MonoBehaviour
{
    //sets bool variables needed to toggle flip and attack functions. 
    public bool isLookingRight;
    public static bool hasArmour = false, hasSword = false;
    public Text scoreText;

    private bool onGround, GravityFlip = false, isInvinsable = false, startSwordTimer = false,
        startCountdownTillDeath = false;
    private bool hasLaserGun = false;
    private float moveValue, speed = 10.0f, mass = 1.0f, swordTimer = 0.2f;
    private GameObject LaserSpawnPoint;
    private Rigidbody2D instanceOfBallShot, instanceOfLaserShot, rb;
    private Transform myTransform;
    private Vector3 velocity, accel;
    private SpriteRenderer foxSprite; 

    [SerializeField] float deathTimer;
    [SerializeField] AudioClip LaserSoundclip, gotPowerUpClip, SwordSwingClip, JumpClip, gemPickUpClip;
    [SerializeField] AudioSource audioSource; 
    [SerializeField] float JumpForce;
    [SerializeField] Animator anim;
    [SerializeField] GameObject LaserBolt, sword, LaserGun, SwordWhileSwinging;
    [SerializeField] SpriteRenderer playerSpriteRenderer; //Get the sprite renderer
    [SerializeField] Transform shotSpawn;
    int _points;

    float horizontal;
    private void Awake()
    {
        //makes sure script has ridgidbody component and that mass is set. 
        rb = GetComponent<Rigidbody2D>();
        rb.GetComponent<Rigidbody2D>();
        rb.mass = 1.0f;
        myTransform = GetComponent<Transform>();
        foxSprite = GetComponent<SpriteRenderer>(); 
    } 

    private void PlayASound(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play(); 
    }
    //flips character based on where the player faces. 
    void Flip(float horizontal)
    {
        if ((horizontal > 0 && !isLookingRight || horizontal < 0 && isLookingRight) && !startCountdownTillDeath)
        {
            isLookingRight = !isLookingRight;
            transform.Rotate(0.0f, 180f, 0f);  
        }
    }

    public int points
    {
        get
        {
            return _points;
        }
        set
        {
            _points = value;
            if (scoreText)

                scoreText.text = "Score: " + points;
        }
    }

    //if player colldes with ground set animator bool and script bool to true. 
    private void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag == "SwordAttackPowerUp")
        {
            hasSword = true;
            hasLaserGun = false; 
            sword.SetActive(true);
            LaserGun.SetActive(false); 
            Destroy(c.gameObject);
            PlayASound(gotPowerUpClip, ChangeVolume.soundVolume); 
        }

        if (c.gameObject.tag == "Ground")
        {
            onGround = true;
            anim.SetBool("onGround", true);
        }

        if (c.gameObject.CompareTag("MovingPlatform"))
        {
            Debug.Log("on platform"); 
            onGround = true;
            anim.SetBool("onGround", true);
            myTransform.parent = c.gameObject.transform; 
        }

        if (c.gameObject.tag == "LaserGun")
        {
            Destroy(c.gameObject); 
            hasSword = false;
            hasLaserGun = true;
            LaserGun.SetActive(true);
            sword.SetActive(false);
            SwordWhileSwinging.SetActive(false);
            PlayASound(gotPowerUpClip, ChangeVolume.soundVolume);
            //Instantiate(LaserGun, shotSpawn.position, shotSpawn.rotation); 
        }

        if (c.gameObject.tag == "Armour")
        {
            if(ArmourBar.armourAmount <= 3)
            {
                ArmourBar.armourAmount = ArmourBar.armourAmount + 1;
            }
            Destroy(c.gameObject);
            PlayASound(gotPowerUpClip, ChangeVolume.soundVolume);
        }

        if(!isInvinsable && (c.gameObject.CompareTag("Enemy") || c.gameObject.CompareTag("EProjectile")))
        {
            //starts the coroutine that makes the player invinsible
            StartCoroutine(TempInvinsibility());
            //projectiles will be destroyed when they touch the player
            if(c.gameObject.tag == "EProjectile")
            {
                Destroy(c.gameObject); 
            }

            if(c.gameObject.CompareTag("Enemy"))
            {
                anim.SetBool("onGround", true); 
            }

            if(!hasArmour)
            {
                Health.life--; 
            }
            else if (hasArmour)
            {
                ArmourBar.armourAmount--; 
            }
        }

        if (c.gameObject.tag == "Blockman")
        {
            startCountdownTillDeath = true;
            rb.velocity = new Vector3(0, 0, 0);
            myTransform.localScale = new Vector3(myTransform.localScale.x, 3.5f,
                myTransform.localScale.z);
            anim.enabled = false;
        }

        if (c.gameObject.tag == "CherryPowerUp")
        {
            speed = speed + 2;
            Destroy(c.gameObject);
            PlayASound(gotPowerUpClip, ChangeVolume.soundVolume); 
        }

        if (c.gameObject.tag == "Invincible")
        {
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
            isInvinsable = true;
            PlayASound(gotPowerUpClip, ChangeVolume.soundVolume); 
            Destroy(c.gameObject);
            StartCoroutine(waitAndTurnOffInvinsibility());
        }

        if (c.gameObject.tag == "Gem")
        {
            Gem gem = c.gameObject.GetComponent<Gem>(); 

            if (gem)
            {
                points += gem.Points;
                Destroy(c.gameObject);
                PlayASound(gemPickUpClip, ChangeVolume.soundVolume); 
            }
        }
    }

    private void OnCollisionStay2D(Collision2D c)
    {
        if(c.gameObject.CompareTag("Enemy") && !isInvinsable)
        {
            if (!hasArmour)
            {
                Health.life--;
            }
            else if (hasArmour)
            {
                ArmourBar.armourAmount--;
            }
            StartCoroutine(TempInvinsibility());
        }

        if(c.gameObject.CompareTag("Ground") && !onGround)
        {
            onGround = true;
            anim.SetBool("onGround", true);
        }
    }

    private IEnumerator TempInvinsibility()
    {
        isInvinsable = true;
        foxSprite.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        foxSprite.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        foxSprite.color = new Color(1f, 1f, 1f, 0.5f); 
        yield return new WaitForSeconds(0.5f);
        foxSprite.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.5f);
        foxSprite.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        foxSprite.color = new Color(1f, 1f, 1f, 1f);
        isInvinsable = false;
    }

    private IEnumerator waitAndTurnOffInvinsibility()
    {
        yield return new WaitForSeconds(45);
        playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        isInvinsable = false; 
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
       if(c.gameObject.CompareTag("RedLaser"))
        {
            if(hasArmour && !isInvinsable)
            {
                ArmourBar.armourAmount -= 1;
                StartCoroutine(TempInvinsibility());
            }
            else if(!isInvinsable && !hasArmour)
            {
                Health.life -= 1;
                StartCoroutine(TempInvinsibility());
            }
        }

        if (c.gameObject.CompareTag("Enemy"))
        {
            anim.SetBool("onGround", true);
            if (hasArmour && !isInvinsable)
            {
                ArmourBar.armourAmount -= 1;
                StartCoroutine(TempInvinsibility());
            }
            else if(!isInvinsable && !hasArmour)
            {
                Debug.Log(c.gameObject); 
                Health.life -= 1;
                StartCoroutine(TempInvinsibility());
            }
        }

        if (c.gameObject.tag == "Death")
        {
            startCountdownTillDeath = true;
        }

        if (c.gameObject.tag == "Gem")
        {
            Gem gem = c.GetComponent<Gem>();

            if (gem)
            {
                PlayASound(gemPickUpClip, ChangeVolume.soundVolume); 
                points += gem.Points;
                Destroy(c.gameObject);
            }
        }
    }
    //on ground bools are flase if player leaves the ground.
    private void OnCollisionExit2D(Collision2D c)
    {
        if(c.gameObject.tag == "Ground")
        {
            onGround = false;
            anim.SetBool("onGround", false); 
        }

        if(c.gameObject.CompareTag("MovingPlatform"))
        {
            onGround = false;
            anim.SetBool("onGround", false);
            myTransform.parent = null; 
        }
    }

    private void SwordAttack()
    {
        if(hasSword && Input.GetKeyDown(KeyCode.LeftShift) && swordTimer == 0.2f)
        {
            PlayASound(SwordSwingClip, ChangeVolume.soundVolume); 
            sword.SetActive(false);
            SwordWhileSwinging.SetActive(true);
            startSwordTimer = true; 
            if(swordTimer <= 0)
            {
                sword.SetActive(true);
                SwordWhileSwinging.SetActive(false);
                swordTimer = 0.2f;
            }
        }
        if(startSwordTimer)
        {
            swordTimer -= Time.deltaTime; 
        }

        if (swordTimer <= 0)
        {
            sword.SetActive(true);
            SwordWhileSwinging.SetActive(false);
            startSwordTimer = false;
            swordTimer = 0.2f; 
        }
    }


    private void Update()
    {
        SwordAttack();
        horizontal = Input.GetAxis("Horizontal");
        if(startCountdownTillDeath)
        {
            deathTimer += Time.deltaTime;
            if(deathTimer >= 3)
            {
                SceneManager.LoadScene("GameOver"); 
            }
        }
        if(!GravityFlip)
        {
            Flip(horizontal);
        }
        else
        {
            Flip(-horizontal); 
        }
        moveValue = Input.GetAxisRaw("Horizontal");

        //Debug.Log(moveValue); 
        if (onGround)
        {
            anim.SetBool("onGround", true); 
            //controls movement. 
            if(Input.GetButtonDown("Jump") && !GravityFlip)
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.up * JumpForce;
                PlayASound(JumpClip, ChangeVolume.soundVolume); 
            }
        }

        if (rb && !startCountdownTillDeath)
        {
            rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);
            anim.SetFloat("Moveing", Mathf.Abs(moveValue)); 
        }
        else
        {
            Debug.Log("this script does not have the rb");
        }
    }
}