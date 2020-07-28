using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blacky : MonoBehaviour
{
    public Sprite[] SpritePool;

    private SpriteRenderer _spriteRenderer;
    private int _munches;
    private float _nextChange;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _nextChange = Time.time + 0.5f;
    }

    public void Trigger()
    {
        _munches = 4;
        _nextChange = Time.time;
    }

    public bool Consume(Item item)
    {
        if(_munches > 0)
            return false;
        
        Common.DestroyItem(item, false);
        return true;
    }

    public void Tick()
    {
        if(Time.time > _nextChange)
        {
            if(_munches > 0)
            {
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
