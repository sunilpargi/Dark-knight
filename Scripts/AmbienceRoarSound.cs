using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceRoarSound : MonoBehaviour
{
    public AudioClip[] roarClips;
    AudioSource audioSource;
     void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayRoarSound());
    }

  
    IEnumerator PlayRoarSound()
    {
        yield return new WaitForSeconds(2f);

        audioSource.PlayOneShot(roarClips[Random.Range(0,roarClips.Length)]);

        StartCoroutine(PlayRoarSound());
    }
}
