using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollRect : MonoBehaviour
{
    public float Speed = 200f;

    private RectTransform _rectTransform;
    private float _desiredY;
    private float _currentVelocity;
    private bool  _scrolling;
    
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        SetY(0);
    }

    void Update()
    {
        if(_scrolling)
        {
            SetY(Mathf.SmoothDamp(_rectTransform.anchoredPosition.y, _desiredY, ref _currentVelocity, 0.1f));
            if(Mathf.Abs(_desiredY - _rectTransform.anchoredPosition.y) < 1f)
            {
                SetY(_desiredY);
                _scrolling = false;
            }
        }
    }

    public void SetY(float y)
    {
        if(y < 0)
            y = 0;
        
        var ItemRectPosition = _rectTransform.anchoredPosition;
        ItemRectPosition.y = y;
        _rectTransform.anchoredPosition = ItemRectPosition;
    }

    public void Scroll(float direction)
    {
        if(!_scrolling)
            _desiredY = _rectTransform.anchoredPosition.y;
        
        _desiredY += direction * Speed;
        _desiredY = Mathf.Clamp(_desiredY, 0, _rectTransform.sizeDelta.y);
        _scrolling = true;
    }
}
