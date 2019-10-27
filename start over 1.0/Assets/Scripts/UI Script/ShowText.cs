using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowText : MonoBehaviour
{
    public GameObject text;
    public AudioClip sayIt;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        text.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider other) {
		if(other.gameObject.tag == "Player"){
			text.SetActive(true);
            audioSource.volume = 0.5f;
            audioSource.PlayOneShot(sayIt);
            StartCoroutine("WaitForSec");
		}
	}

	IEnumerator WaitForSec () {
		yield return new WaitForSeconds(4);
		Destroy(text);
		Destroy(gameObject);
	}
}
