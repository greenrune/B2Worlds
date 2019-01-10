using UnityEngine;
using UnityEngine.Audio;
using System;

public class SoundManager : MonoBehaviour {

	public AudioSource bsoEmitter;
	public AudioSource effectEmitter;
	 
	 public AudioMixer masterMixer;
	 
	
	void Start()
	{
		if(!PlayerPrefs.HasKey("Sound")) {
			PlayerPrefs.SetInt("Sound", 0);
			masterMixer.SetFloat("masterVol", 0);
		}
		else if(PlayerPrefs.GetInt("Sound") == -80){
			masterMixer.SetFloat("masterVol", -80);
		}
		else if(PlayerPrefs.GetInt("Sound") == 0){
			masterMixer.SetFloat("masterVol", 0);
			
		}
		
	}
	void Update()
	{
		// LowPass();
	}
	public void SetCurrentBSO (AudioClip track) {
		bsoEmitter.Stop();
		bsoEmitter.clip = track;
		bsoEmitter.Play();
	}
	
	public void SetCurrentEffect (AudioClip track) {
		effectEmitter.PlayOneShot(track);
	}

	// void LowPass()
	// {
	// 	if(voiceEmitter.isPlaying) voice.TransitionTo(0.1F);
	// 	else bso.TransitionTo(0.1F);
	// }

	public void SetBSOPitch(float offSet)
	{
		bsoEmitter.pitch += offSet;
	}

    internal void SetCurrentEffect(object denyFX)
    {
        throw new NotImplementedException();
    }

	
	public void SetSFXLevel(float level)
	{
		masterMixer.SetFloat("sfxVol", level);
	}
	public void SetAudioLevel(float level)
	{
		masterMixer.SetFloat("masterVol", level);
		if(level == 0){
			PlayerPrefs.SetInt("Sound", 0);
		} 
		else{
			PlayerPrefs.SetInt("Sound", -80);
		}
	}
	
}
