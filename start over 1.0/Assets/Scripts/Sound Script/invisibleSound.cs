using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisibleSound : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] clips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void slash()
    {
        AudioClip clip = clips[0];
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(clip);
    }

    void onHit()
    {
        AudioClip clip = clips[1];
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(clip);
    }

    void die()
    {
        AudioClip clip = clips[2];
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(clip);
    }

    void step()
    {
        //just to receive but doesnt do anything, only player and boss got steps
    }
    // Update is called once per frame
    void Update()
    {

    }
}
