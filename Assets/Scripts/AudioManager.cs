using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] public List<AudioClip> _clips = new List<AudioClip>();
    [SerializeField, ReadOnly] public List<AudioSource> _sources = new List<AudioSource>();

    [Header("Instance Data")]
    public static AudioManager _instance;
    private bool gameStart = false;

    void Awake()
    {
        if (!gameStart)
        {
            _instance = this;
            gameStart = true;
        }
    }

    void Start()
    {
        // Initialize all audio clips with an associated source
        for (int i = 0; i < _clips.Count; i++)
        {
            _sources.Add(new AudioSource());
            _sources[i] = gameObject.AddComponent<AudioSource>();
            _sources[i].playOnAwake = false;
            _sources[i].clip = _clips[i];
        }
    }

    public void PlaySound(int index)
    {
        _sources[index].PlayOneShot(_sources[index].clip);
    }
}
