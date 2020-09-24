using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudioFromSource : MonoBehaviour
{
    public void PlayButtonSound(int index)
    {
        AudioManager._instance.PlaySound(index);
    }
}
