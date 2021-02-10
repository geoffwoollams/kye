using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    [HideInInspector]
    public int Thinker;
    public bool IsKye;
    public bool IsSploder;
    public bool IsSplodable;
    public bool IsClocker, IsAntiClocker;
    public bool IsDiamond;
    public bool IsStickerLR;
    public bool IsStickerUD;
    public bool IsStickable;
    [HideInInspector]
    public bool IsStuckToSticker;
    public bool IsBlackhole;
    public bool IsDoor;
    public bool IsPushable;
    public bool CanPush;
    public bool DestroyOnPlayerContact;
    public bool KillsPlayer;
    public int Roundness;
    public Vector2 Direction;

    public int x, y;
    public Vector2Int position
    {
        get { return new Vector2Int(x, y); }
    }

    private GoForward _goForward;
    private Auto _auto;
    public Blackhole _blackhole;
    private Enemy _enemy;
    private Timer _timer;
    public Sploder _sploder;

    public class ItemCollection
    {
        public List<Item> Items;
    }

    public void Tick()
    {
        if(IsStickable)
            Common.PullToSticker(this);

        try
        {
            if(_goForward && !IsStuckToSticker) _goForward.Tick();
            if(_enemy && !IsStuckToSticker) _enemy.Tick();

            if(_auto) _auto.Tick();
            if(_blackhole) _blackhole.Tick();
            if(_timer) _timer.Tick();
            if(_sploder) _sploder.Tick();
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString(), this);
        }

        if(IsStickable)
            Common.PullToSticker(this);
        
        if(IsStuckToSticker)
        {
            bool stuck = false;
            
            var left = Common.GetItemBy(Vector2.left, this);
            var right = Common.GetItemBy(Vector2.right, this);
            var down = Common.GetItemBy(Vector2.down, this);
            var up = Common.GetItemBy(Vector2.up, this);

            if(up && up.IsStickerUD) stuck = true;
            if(down && down.IsStickerUD) stuck = true;
            if(left && left.IsStickerLR) stuck = true;
            if(right && right.IsStickerLR) stuck = true;

            if(!stuck)
                IsStuckToSticker = false;
        }
    }

    void Awake()
    {
        _goForward = GetComponent<GoForward>();
        _auto = GetComponent<Auto>();
        _blackhole = GetComponent<Blackhole>();
        _enemy = GetComponent<Enemy>();
        _timer = GetComponent<Timer>();
        _sploder = GetComponent<Sploder>();

        Common.SetItem(transform.position.x, transform.position.y, this);

        if(IsKye)
            GameController.Instance.StartPoint = transform.position;
    }

    public void Move(Vector2 delta, bool shoved = false, bool pulled = false) { Move((int)delta.x, (int)delta.y, shoved, pulled); }
    public void Move(int x, int y, bool shoved = false, bool pulled = false)
    {
        if(pulled)
            shoved = true;
        
        if(IsStuckToSticker && !shoved)
            return;

        Item stuckItem = null;
        if(pulled && (IsStickerUD || IsStickerLR))
            stuckItem = Common.GetItemBy(-x, -y, this);

        int xPos = x + (int)transform.position.x;
        int yPos = y + (int)transform.position.y;

        if(Common.CanMoveTo(xPos, yPos, this))
        {
            Common.MoveItem(xPos, yPos, this);

            if(stuckItem != null && stuckItem.IsStickable)
                stuckItem.Move(x, y, false, true);
            
            if(_sploder && !pulled) _sploder.Tock();
        }
    }

    public bool CanCurrentlyBePushed(Vector2 direction)
    {
        if(!IsPushable)
            return false;

        var spot = (Vector2)transform.position + direction;

        if(!Common.InBounds(spot))
            return false;
        
        var spotItem = Common.GetItem(spot);

        if(spotItem == null)
            return true;

        if(spotItem.IsBlackhole)
            return true;
        
        return false;
    }
}
