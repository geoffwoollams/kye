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

        Item newTimerItem = null;

        if (NextTimerPrefab != null)
        {
            GameObject newTimer = Instantiate(NextTimerPrefab, transform.position, transform.rotation, transform.parent);
            newTimerItem = newTimer.GetComponent<Item>();
        }
        
        Common.DestroyItem(_item, false);
        Common.SetItem(transform.position.x, transform.position.y, newTimerItem);
    }
}
