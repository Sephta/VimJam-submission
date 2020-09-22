using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    [Header("Object and Tween Data")]
    public RectTransform objectToTween = null;
    public LeanTweenType easeIn;
    public float timeToTween = 0f;

    [Header("Positional Data")]
    public PosType desiredPosType = PosType.posX;
    public float desiredPosX = 0f;
    public float desiredPosY = 0f;

    public enum PosType
    {
        posX,
        posY,
        both
    }

    void Awake()
    {
        if (objectToTween == null)
            Debug.Log("Warning. reference to 'objectToTween' on " + gameObject.name + " is null.");
    }

    void OnEnable()
    {
        if (objectToTween != null)
        {
            switch(desiredPosType)
            {
                case PosType.posX:
                    LeanTween.moveX(objectToTween, desiredPosX, timeToTween).setEase(easeIn);
                    break;

                case PosType.posY:
                    LeanTween.moveY(objectToTween, desiredPosY, timeToTween).setEase(easeIn);
                    break;

                case PosType.both:
                    LeanTween.move(objectToTween, new Vector3(desiredPosX, desiredPosY, 0f), timeToTween);
                    break;
            }
        }
    }

    void OnDisable() 
    {

    }

    // void Start() {}
    // void Update() {}
    // void FixedUpdate() {}
}
