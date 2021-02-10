using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : MonoBehaviour
{
    public Sprite[] SpritePool;

    private SpriteRenderer _spriteRenderer;
    private int _munches;
    private float _nextChange;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _nextChange = Time.time + 0.5f;
        _munches = -1;
    }

    public void Trigger()
    {
        _munches = 4;
        _nextChange = Time.time;
    }

    public bool Consume(Item item)
    {
        if (_munches > 0)
            return false;
        else if (_munches == 0)
        {
            Common.DestroyItem(item, false);
            _munches = -1;
            return true;
        }
        else
        {
            Trigger();
            return false;
        }
    }

    public void Tick()
    {
        if(Time.time > _nextChange)
        {
            if(_munches > 0 )
            {
                //_spriteRenderer.sprite = SpritePool[Common.GetRandom(4, 7)];
                _spriteRenderer.sprite = SpritePool[8 - _munches];
                _munches--;
            }
            else
            {
                _spriteRenderer.sprite = SpritePool[Common.GetRandom(0, 3)];
            }

            _nextChange = Time.time + 0.5f;
        }
    }
}
