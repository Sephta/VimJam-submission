using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [Header("Settings Data")]
    [Range(0f, 1f)] public float _musicVolume = 0f;

    [Header("Instance Data")]
    public static GameSettings _instance;
    private bool gameStart = false;

    void Awake()
    {
        if (!gameStart)
        {
            _instance = this;
            DontDestroyOnLoad(_instance);
            gameStart = true;
        }
    }
}

