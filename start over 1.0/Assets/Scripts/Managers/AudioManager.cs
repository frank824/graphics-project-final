using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	
	public AudioSource BGM;
 

	public void ChangeBGM(AudioClip music) {
		if(BGM.clip.name == music.name){
			return;
		}

		BGM.Stop();
		BGM.clip = music;
		BGM.Play();
	
	}
}
