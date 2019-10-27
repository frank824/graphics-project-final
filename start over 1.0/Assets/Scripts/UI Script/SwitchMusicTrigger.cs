using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMusicTrigger : MonoBehaviour
{
	public AudioClip newTrack;
	private AudioManager AM;
    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
    }


	void OnTriggerEnter(Collider other){
		if(other.tag == "Player") {
			if(newTrack != null) {
				AM.ChangeBGM(newTrack);
			}
		}
	}
}
