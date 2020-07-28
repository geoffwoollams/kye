using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sploder : MonoBehaviour
{
    public Sprite Danger;
    private int remainingMoves = 5;
    private Item _item;
    private SpriteRenderer _spriteRenderer;
    [HideInInspector]
    public float SplodeTime;
    [HideInInspector]
    public bool HasSploded;

    void Awake()
    {
        _item = GetComponent<Item>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Tick()
    {
        if(SplodeTime > 0 && Time.time > SplodeTime)
        {
            SplodeTime = 0;
            Splode();
        }
    }

    public void Tock(int moves = 1)
    {
        // Runs when item has been moved or exploded nearby
        remainingMoves -= moves;

        if(remainingMoves <= 1)
            _spriteRenderer.sprite = Danger;

        if(remainingMoves > 0)
            return;
        
        Splode();
    }

    private void Splode()
    {
        if(HasSploded)
            return;
        HasSploded = true;

        Boom(-1, 1);
        Boom(0, 1);
        Boom(1, 1);
        Boom(-1, 0);
        Boom(1, 0);
        Boom(-1, -1);
        Boom(0, -1);
        Boom(1, -1);

        Common.Effect(_item.x, _item.y);
        Common.DestroyItem(_item, false);
    }

    private void Boom(int x, int y)
    {
        var item = Common.GetItem(_item.x + x, _item.y + y);
        if(item == null)
            return;

        if(item.IsSploder)
        {
            item._sploder.SplodeTime = Time.time + 0.1f;
            return;
        }
        else if(!item.IsSplodable)
            return;
        else if(item.IsDiamond)
        {
            item.gameObject.SetActive(false);
            GameController.Instance.Kill("A diamond was destroyed!");
            return;
        }
        else if(item.IsKye)
        {
            GameController.Instance.KillPlayer("Be careful of Sploders!");
            return;
        }

        Common.DestroyItem(item);
    }
}
