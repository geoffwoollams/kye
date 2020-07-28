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

    private float _lastRotation;

    void Awake()
    {
        _lastRotation = Time.time;
    }
    
    void Update()
    {
        if(Time.time > (_lastRotation + Timer))
        {
            transform.Rotate(0, 0, RotateAmount * (Reverse ? -1 : 1));
            _lastRotation = Time.time;
        }
    }
}
