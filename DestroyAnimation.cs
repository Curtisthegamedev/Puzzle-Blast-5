using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimation : MonoBehaviour
{
    [SerializeField] float delay = 0;
    [SerializeField] AudioClip Explotionclip;
    [SerializeField] AudioSource audioSource; 


    private void PlayASound(AudioClip clip, float volume)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    private void Start()
    {
        PlayASound(Explotionclip, ChangeVolume.soundVolume); 
        Destroy(this.gameObject, this.GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).length); 
    }
}
