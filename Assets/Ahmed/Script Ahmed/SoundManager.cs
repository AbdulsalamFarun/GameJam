using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;


    [Header("----------- Audio Source -----------")]
    [SerializeField] AudioSource musicSource;
    public AudioSource SFXSource;

    [Header("----------- Audio Clips -----------")]
    public AudioClip Point;



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
            { "Point",Point },
           

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
          SFXSource.PlayOneShot(clip);
        SFXSource.pitch += 0.01f;
        
    }
}