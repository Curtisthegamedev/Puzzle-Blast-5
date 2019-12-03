using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//THIS SCRIPT IS ATTACHED TO THE PLAYER
public class LazerGun : MonoBehaviour
{

    [SerializeField] Transform firepoint;
    [SerializeField] GameObject bulletPrefab;
    private float timeSinceLastShot;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip laserSoundClip; 

    private void Update()
    {
        audioSource.volume = ChangeVolume.soundVolume; 
        if(timeSinceLastShot < 0.5)
        {
            timeSinceLastShot += Time.deltaTime; 
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && timeSinceLastShot >= 0.5)
        {
            shoot();
            timeSinceLastShot = 0; 
        }
    }

    private void playLaserSound(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play(); 
    }

    private void shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        playLaserSound(laserSoundClip, ChangeVolume.soundVolume); 
    }

}
