using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Auto : MonoBehaviour
{
    public GameObject Prefab;
    public float RotateSpeed = 0.7f;
    private int MinDelay = 4;
    private int MaxDelay = 4;

    private Item _item;
    private float _nextRotate;
    private int _spawnDelay;
    private int _spawnAttempt;

    void Awake()
    {
        _item = GetComponent<Item>();
        SetRotateDelay();
        SetSpawnDelay();
    }

    public void Tick()
    {
        if(Time.time > _nextRotate)
        {
            Common.Rot90(ref _item);
            SetRotateDelay();
            _spawnDelay--;
        }

        if(_spawnDelay <= 0)
        {
            var spot = transform.position + (Vector3)_item.Direction;
            var spotItem = Common.GetItem(spot.x, spot.y);
            var isEmpty = Common.IsEmpty(spot.x, spot.y);

            if(isEmpty)
            {
                var spawn = Instantiate(Prefab, spot, transform.localRotation, transform.parent);
                spawn.transform.Rotate(0, 0, -90);
                var spawnItem = spawn.GetComponent<Item>();
                spawnItem.Direction = _item.Direction;
                GameController.Instance.NewItem(spawnItem);
                SetSpawnDelay();
            }
            else // prevent immediate spawn when path is clear
            {
                _spawnAttempt++;
                if(_spawnAttempt == 4)
                    SetSpawnDelay();
            }
        }
    }

    void SetRotateDelay()
    {
        _nextRotate = Time.time + RotateSpeed;
    }

    void SetSpawnDelay()
    {
        _spawnDelay = Common.GetRandom(MinDelay, MaxDelay);
    }
}
