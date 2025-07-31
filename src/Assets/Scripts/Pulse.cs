using UnityEngine;
using System.Collections;

public class Pulse : MonoBehaviour
{
    private float _elapsedTime;

    public float ScaleLow = 0.8f;
    public float ScaleHigh = 1f;

    public float Speed = 1;

    private bool _isRect;
    private RectTransform _rectTransform;

    void Awake() {
        _rectTransform = GetComponent<RectTransform>();
        if(_rectTransform != null) {
            _isRect = true;
        }
    }

 	void Update () {
        var scaleT = (_elapsedTime % 1) / 1;

        if (((int)_elapsedTime % 2f) != 0f)
        {
            var scale = Mathf.SmoothStep(ScaleLow, ScaleHigh, scaleT);
            if(_isRect)
                _rectTransform.localScale = new Vector3(scale, scale, scale);
            else
                transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            var scale = Mathf.SmoothStep(ScaleHigh, ScaleLow, scaleT);
            if(_isRect)
                _rectTransform.localScale = new Vector3(scale, scale, scale);
            else
                transform.localScale = new Vector3(scale, scale, scale);
        }

        //var scale = Mathf.SmoothStep(1f, 1.1f, scaleT);
        //transform.localScale = new Vector3(scale, scale, scale);

        _elapsedTime += (Time.unscaledDeltaTime * Speed);
    }
}
