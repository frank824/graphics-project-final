using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{
    [SerializeField]
    public AudioClip[] clips;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void step()
    {
        AudioClip clip = clips[0];
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(clip);
    }

	void slash()
	{
		AudioClip clip = clips[1];
		audioSource.volume = 0.4f;
		audioSource.PlayOneShot(clip);
	}

	void onHit()
	{
		AudioClip clip = clips[2];
		audioSource.volume = 0.2f;
		audioSource.PlayOneShot(clip);
	}

    void die()
    {
        AudioClip clip = clips[3];
        audioSource.volume = 0.4f;
        audioSource.PlayOneShot(clip);
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
