using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video; 

public class VideoStream : MonoBehaviour
{
    [SerializeField] RawImage image;
    [SerializeField] VideoPlayer video;

    private void Start()
    {
        StartCoroutine(playVideo()); 
    }

    private void Update()
    {
        if(ButtonMethods.LoadPuzzleVideo)
        {
            StartCoroutine(playVideo());
            ButtonMethods.LoadPuzzleVideo = false; 
        }
    }

    private IEnumerator playVideo()
    {
        video.Prepare();
        while (!video.isPrepared)
        {
            yield return new WaitForSeconds(1);
            break; 
        }

        image.texture = video.texture;
        video.Play(); 
    }
}
