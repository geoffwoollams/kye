using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForward : MonoBehaviour
{
    public float Timer = 0.1f;
    public bool ReverseOnFail;

    private Item _item;
    private float _lastMovement;

    void Awake()
    {
        _item = GetComponent<Item>();
        _lastMovement = Time.time;
    }

    public void Tick()
    {
        if(Time.time > (_lastMovement + Timer))
        {
            // one attempt
            _lastMovement = Time.time;

            var spot = transform.position + (Vector3)_item.Direction;
            var spotItem = Common.GetItem(spot.x, spot.y);
            var isEmpty = Common.IsEmpty(spot.x, spot.y);

            if(spotItem != null)
            {
                var isNextEmpty = Common.IsEmptyBy(_item.Direction, spotItem);

                if(spotItem.IsSploder)
                    spotItem.GetComponent<Sploder>().Tock();

                if(spotItem.IsBlackhole)
                {
                    spotItem.GetComponent<Blackhole>().Consume(_item);
                    return;
                }
                else if(spotItem.IsClocker && spotItem.IsAntiClocker)
                    Common.Rot90(ref _item, Common.GetRandom(2) == 1);
                else if(spotItem.IsClocker)
                    Common.Rot90(ref _item);
                else if(spotItem.IsAntiClocker)
                    Common.Rot90(ref _item, true);
                else if(_item.Roundness == 5 && spotItem.Roundness > 0)
                {
                    AdjustForRoundness(spotItem.Roundness);
                }
                else if(_item.CanPush && spotItem.IsPushable && isNextEmpty)
                {
                    spotItem.Move(_item.Direction, true);
                }
            }

            if(isEmpty)
            {
                Common.MoveItem(spot.x, spot.y, _item);
                _lastMovement = Time.time;
            }
            else
            {
                if(ReverseOnFail)
                {
                    _item.transform.Rotate(0, 0, 180);
                    _item.Direction *= -1f;
                    _lastMovement = Time.time;
                }
            }
        }
    }

    void AdjustForRoundness(float spotItemRoundness)
    {
        Vector2 spotLeft = Vector2.zero;
        Vector2 spotRight = Vector2.zero;
        Vector2 spotUpLeft = Vector2.zero;
        Vector2 spotUpRight = Vector2.zero;

        if(_item.Direction == Vector2.up)
        {
            spotUpLeft = Vector2.up + Vector2.left;
            spotUpRight = Vector2.up + Vector2.right;
            spotLeft = Vector2.left;
            spotRight = Vector2.right;
        }
        else if(_item.Direction == Vector2.down)
        {
            spotUpLeft = Vector2.down + Vector2.right;
            spotUpRight = Vector2.down + Vector2.left;
            spotLeft = Vector2.right;
            spotRight = Vector2.left;
        }
        else if(_item.Direction == Vector2.left)
        {
            spotUpLeft = Vector2.left + Vector2.down;
            spotUpRight = Vector2.left + Vector2.up;
            spotLeft = Vector2.down;
            spotRight = Vector2.up;
        }
        else if(_item.Direction == Vector2.right)
        {
            spotUpLeft = Vector2.right + Vector2.up;
            spotUpRight = Vector2.right + Vector2.down;
            spotLeft = Vector2.up;
            spotRight = Vector2.down;
        }

        var leftClear = Common.IsEmptyBy(spotLeft, _item) && Common.IsEmptyBy(spotUpLeft, _item);
        var rightClear = Common.IsEmptyBy(spotRight, _item) && Common.IsEmptyBy(spotUpRight, _item);

        if(!leftClear && !rightClear)
            return;

        Vector2 delta = Vector2.zero;

        if(leftClear && rightClear)
            delta = Common.GetRandom(2) == 0 ? spotUpLeft : spotUpRight;
        else
            delta = leftClear ? spotUpLeft : spotUpRight;
        
        _lastMovement = Time.time;
        _item.Move(delta);
    }
}
