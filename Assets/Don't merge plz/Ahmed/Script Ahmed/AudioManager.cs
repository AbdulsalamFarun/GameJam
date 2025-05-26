using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----------- Audio Clips -----------")]
    public AudioClip point;


    private Dictionary<string, AudioClip> sfxClips;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        sfxClips = new Dictionary<string, AudioClip>
        {
            { "Hit",point},

        };
    }

    private void Start()
    {
        //musicSource.clip = background;
        musicSource.loop = true;
        musicSource.Play();
        SFXSource.volume = 0.5f;
    }

    public void PlaySFX(AudioClip clip)
    {

        if (!SFXSource.isPlaying)
        {
            SFXSource.PlayOneShot(clip);
        }
    }
}