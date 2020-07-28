using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAndDestroy : MonoBehaviour
{
    public float Seconds = 0.5f;
    private SpriteRenderer _spriteRenderer;
    private Color _color;
    public  float OriginalAlpha;
    private float _currentVelocity;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
        OriginalAlpha = _color.a;
    }

    void Update()
    {
        _color.a = Mathf.SmoothDamp(_color.a, 0, ref _currentVelocity, Seconds);
        if(_color.a <= 0.001)
            Destroy(gameObject);
        _spriteRenderer.color = _color;
    }
}
