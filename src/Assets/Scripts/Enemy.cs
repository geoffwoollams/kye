using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1f;

    private float _nextMove;
    private bool _wandering;
    private Item _item;
    
    void Awake()
    {
        _item = GetComponent<Item>();
        _nextMove = Time.time + Speed;
    }

    public void Tick()
    {
        // todo: rewrite or credit python kye

        if(Time.time < _nextMove)
            return;
        
        if(_item.IsStuckToSticker)
            return;

        var x = _item.x;
        var y = _item.y;

        Vector2 direction = Vector2.zero;

        //int tx = 0;
        //int ty = 0;

        if(Common.GetRandom(2) == 0)
        {
            // I wander lonely as a cloud...
            var d = Common.GetRandom(5);
            if(d == 0)
                direction = Vector2.left;
            else if(d == 1)
                direction = Vector2.right;
            else if(d == 2)
                direction = Vector2.down;
            else if(d == 3)
                direction = Vector2.up;
            else if(d == 4)
            {
                _nextMove = Time.time + Speed;
                return; // Don't move at all
            }

            _wandering = true;
        }
        else
        {
            // Advance towards kye
            _wandering = false;
            
            int dx = GameController.Instance.Kye.x - _item.x;
            int dy = GameController.Instance.Kye.y - _item.y;

            // Step towards Kye. Really missing the ternary operator here...
            // Always step dy (up/down) in preference, as the orignial game does
            if(dy == 0)
                if(dx > 0)
                    direction = Vector2.right;
                else
                    direction = Vector2.left;
            else
                if(dy > 0)
                    direction = Vector2.up;
                else
                    direction = Vector2.down;

            // See if we can move that way.
            if(Common.GetItemBy(direction, _item) != null)
            {
                if(direction.x != 0)
                // Try the other direction
                //if(tx == x && dx != 0)
                {
                    if(dx > 0)
                        direction = Vector2.right;
                    else
                        direction = Vector2.left;
                }
                else if(direction.y != 0)
                //else if(ty == y && dy != 0)
                {
                    if(dy > 0)
                        direction = Vector2.up;
                    else
                        direction = Vector2.down;
                }
            }
        }

        // Now try to move to (tx,ty). We only fall into black holes if moving randomely.
        var t = Common.GetItemBy(direction, _item);
        if(t == null)
            Common.MoveItemBy(direction, _item);
        else if(_wandering && t.IsBlackhole)
            t._blackhole.Consume(_item);
        
        _nextMove = Time.time + Speed;
        return;
    }
}
