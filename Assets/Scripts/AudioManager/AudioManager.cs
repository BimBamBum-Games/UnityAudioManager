using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// AudioSource initial degerinde AudioMixerine sahip olmalidir.
/// </summary>

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [SerializeField] AudioSource audioSource;
    [SerializeField] List<AudioClip> oneShots;

    private const string MUSIC_MIXER = "MusicMixer";
    private const string SFX_MIXER = "SfxMixer";

    public event EventHandler<VolumeArgs> OnGameStart;
    private VolumeArgs volumeArgs = VolumeArgs.Get();

    private void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    public void PlaySFX_Clap() {
        int rnd = Random.Range(0, oneShots.Count);
        AudioClip audioClip = oneShots[rnd];
        audioSource.PlayOneShot(audioClip);
    }
}

public class VolumeArgs : EventArgs {
    public float volume;
    public bool isOn;
    private VolumeArgs() { }
    public static VolumeArgs Get() {
        return new VolumeArgs();
    }
}