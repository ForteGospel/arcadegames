using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonaVoiceController : MonoBehaviour
{
    [SerializeField]
    AudioClip[] voices;

    AudioSource source;

    public static sonaVoiceController instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPerfect()
    {
        if (!source.isPlaying)
        {
            source.clip = voices[6];
            source.Play();
        }
    }

    public void playVoices()
    {
        if (!source.isPlaying)
        {
            source.clip = voices[Random.Range(0, 6)];
            source.Play();
        }
    }
}
