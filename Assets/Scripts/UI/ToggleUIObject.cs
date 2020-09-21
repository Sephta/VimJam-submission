using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUIObject : MonoBehaviour
{
    [Header("Object Data")]
    public GameObject objectToToggle = null;
    public bool startState = false;
    public Vector3 originalPos = Vector3.zero;

    [Header("Tween Data")]
    public LeanTweenType easeOut;
    public float timeToTween = 0f;
    public float delayTime = 0f;

    void Awake()
    {
        if (objectToToggle == null)
            Debug.Log("Warning. reference to 'objectToToggle' on " + gameObject.name + " is null.");
    }

    // void OnEnable() {}
    void OnDisable()
    {
        // if (objectToToggle != null)
        //     LeanTween.scale(objectToToggle, Vector3.zero, 1f).setEase(LeanTweenType.linear);
    }

    void Start() 
    {
        if (objectToToggle != null)
            objectToToggle.SetActive(startState);
    }

    // void Update() {}
    // void FixedUpdate() {}

    public void ToggleObject()
    {
        if (objectToToggle != null)
            objectToToggle.SetActive(!objectToToggle.activeSelf);
    }

    // Toggling an object that was moved
    public void ToggleObject_Moved()
    {
        if (objectToToggle != null)
        {
            if (!objectToToggle.activeSelf)
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            else
                LeanTween.move(objectToToggle.GetComponent<RectTransform>(), originalPos, timeToTween).setDelay(delayTime).setEase(easeOut).setOnComplete(SetActiveFalse);
        }
    }

    // Toggling an object that was scaled
    public void ToggleObject_Scaled()
    {
        if (objectToToggle != null)
        {
            if (!objectToToggle.activeSelf)
                objectToToggle.SetActive(!objectToToggle.activeSelf);
            else
            {
                LeanTween.scale(objectToToggle, Vector3.zero, timeToTween).setDelay(delayTime).setEase(easeOut).setOnComplete(SetActiveFalse);
            }
        }
    }

    void SetActiveFalse()
    {
        if (objectToToggle != null)
            objectToToggle.SetActive(false);
    }
}
