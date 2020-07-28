using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject NextTimerPrefab;

    private float _goTime;
    private Item _item;
    
    void Awake()
    {
        _item = GetComponent<Item>();
        _goTime = Time.time + 1f;
    }
    
    public void Tick()
    {
        if(Time.time < _goTime)
            return;

        if(NextTimerPrefab != null)
            Instantiate(NextTimerPrefab, transform.position, transform.rotation, transform.parent);
        
        Common.DestroyItem(_item, false);
    }
}
