using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeGameSettings : MonoBehaviour
{
    public Slider _slider = null;

    void Start()
    {
        if (GetComponent<Slider>() != null)
            _slider = GetComponent<Slider>();
        
        // Init settings
        GameSettings._instance._musicVolume = _slider.value;
    }

    public void ChangeMusicVolume()
    {
        GameSettings._instance._musicVolume = _slider.value;
    }
}
