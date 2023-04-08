using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance = null;
    public static AudioManager Instance { get => _instance; }

    [SerializeField]
    private AudioSource trafficNoiseAS;
    [SerializeField]
    private AudioSource backgroundMusicAS;
    [SerializeField]
    private AudioSource soundEffects2dAS;
    [SerializeField]
    private AudioSource dryerAS;
    [SerializeField]
    private AudioSource handWasherAS;
    [SerializeField]
    private AudioSource microwaveAS;
    [SerializeField]
    private AudioSource wcAS;
    [SerializeField]
    private AudioSource panicButtonAS;

    public AudioClip trafficNoise;
    public AudioClip backgroundMusic;
    public AudioClip dispenserBip;
    public AudioClip doorOpen;
    public AudioClip doorClose;
    public AudioClip dryer;
    public AudioClip handWasher;
    public AudioClip microwaveRunning;
    public AudioClip microwaveEnd;
    public AudioClip pcButtonPressed;
    public AudioClip wc;
    public AudioClip panicButton;
    public AudioClip pcExplosion;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else if (_instance != this)
            Destroy(gameObject);

        trafficNoiseAS.clip = trafficNoise;
        trafficNoiseAS.Play();
        backgroundMusicAS.clip = backgroundMusic;
        backgroundMusicAS.Play();
        dryerAS.clip = dryer;
        handWasherAS.clip = handWasher;
        wcAS.clip = wc;
        panicButtonAS.clip = panicButton;
    }

    public void PlayDispenserBip() => soundEffects2dAS.PlayOneShot(dispenserBip);
    public void PlayDoorOpen() => soundEffects2dAS.PlayOneShot(doorOpen);
    public void PlayDoorClose() => soundEffects2dAS.PlayOneShot(doorClose);
    public void PlayHandWasher() => handWasherAS.Play();
    public void StopHandWasher() => handWasherAS.Stop();
    public void PlayDryer() => dryerAS.Play();
    public void PlayWC() => wcAS.Play();
    public void PlayPanicButton() => panicButtonAS.Play();
    public void PlayPcExplosion() => soundEffects2dAS.PlayOneShot(pcExplosion);
    public void PlayPcButtonPressed() => soundEffects2dAS.PlayOneShot(pcButtonPressed);

    public void PlayMicrowave(bool running)
    {
        if (running)
        {
            microwaveAS.loop = true;
            microwaveAS.clip = microwaveRunning;
        }
        else
        {
            microwaveAS.loop = false;
            microwaveAS.clip = microwaveEnd;
        }
        microwaveAS.Play();
    }

}
