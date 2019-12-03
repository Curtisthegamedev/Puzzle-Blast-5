using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipLaserCanons : MonoBehaviour
{
    [SerializeField] Transform spawnBoltTop;
    [SerializeField] Transform spawnBoltBottom;
    private bool lastShotWasTop = false;
    private float TimeSinceLastShot; 
    [SerializeField] GameObject bolt;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip laserSoundEffect; 

    private void playLaserSound(AudioClip clip, float volume)
    {
        audio.clip = clip;
        audio.volume = volume;
        audio.Play();
    }

    private void Update()
    {
        if(TimeSinceLastShot < 0.5)
        {
            TimeSinceLastShot += Time.deltaTime; 
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && TimeSinceLastShot >= 0.5)
        {
            playLaserSound(laserSoundEffect, ChangeVolume.soundVolume); 
            if(lastShotWasTop)
            {
                Instantiate(bolt, spawnBoltBottom.position, spawnBoltBottom.rotation);
                lastShotWasTop = false;
            }
            else
            {
                Instantiate(bolt, spawnBoltTop.position, spawnBoltTop.rotation);
                lastShotWasTop = true;
            }
            TimeSinceLastShot = 0; 
        }
    }
}
