using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField] private AudioMixerGroup musicMixerGroup;
    [SerializeField] private AudioMixerGroup soundEffectsMixerGroup;

    private void Awake()
    {
        Instance = this;
            
    }

    public void UpdateMixerVolume()
    {
        musicMixerGroup.audioMixer.SetFloat("MusicVol", Mathf.Log10(PauseMenu.musicVolume) * 20);
        soundEffectsMixerGroup.audioMixer.SetFloat("EffectsVol", Mathf.Log10(PauseMenu.soundEffectsVolume) * 20);
        
        
        
    }
}
