using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteLoop : MonoBehaviour
{
    public float Delay = 1;
    public Sprite[] SpritePool;

    private SpriteRenderer _spriteRenderer;
    private Image _image;
    private int _current;
    private float _nextChange;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _image = GetComponent<Image>();
    }

    void Update()
    {
        if(Time.unscaledTime >= _nextChange) {
            _current++;
            if(_current > SpritePool.Length - 1)
                _current = 0;
            
            if(_spriteRenderer != null)
                _spriteRenderer.sprite = SpritePool[_current];
            if(_image != null)
                _image.sprite = SpritePool[_current];

            _nextChange = Time.unscaledTime + Delay;
        }
    }
}
