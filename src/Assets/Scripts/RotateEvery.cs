using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateEvery : MonoBehaviour
{
    [Range(0.25f, 10f)]
    public float Timer = 1f;
    [Range(0,180)]
    public float RotateAmount = 90;
    public bool Reverse;
    public bool RandomiseDirection;

    private float _lastRotation;

    private bool _isRect;
    private RectTransform _rectTransform;

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        if(_rectTransform != null) {
            _isRect = true;
        }

        _lastRotation = Time.unscaledTime;
    }
    
    void Update()
    {
        if(Time.unscaledTime > (_lastRotation + Timer))
        {
            if(RandomiseDirection)
                Reverse = UnityEngine.Random.Range(0f, 1f) > 0.5f ? true : false;
            
            if(_isRect) {
                _rectTransform.Rotate(new Vector3(0, 0, RotateAmount * (Reverse ? -1 : 1)));
            }
            else
                transform.Rotate(0, 0, RotateAmount * (Reverse ? -1 : 1));

            _lastRotation = Time.unscaledTime;
        }
    }
}
