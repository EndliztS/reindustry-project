using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource musicSource, sfxSource;
    [Header("Music")]
    [SerializeField] AudioClip[] theme;

    void Awake() {
        if (!Instance) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else Destroy(gameObject);
    }

    void Start() {
        if ((theme != null) && (theme.Length>0)) {
            musicSource.clip = theme[0];
            musicSource.Play();
            musicSource.pitch = 1;
        }
    }

    public void PlaySFX(Sound sound) {
        sfxSource.pitch = Random.Range(sound.minPitch,sound.maxPitch);
        sfxSource.PlayOneShot(sound.clip);
    }
}
