using UnityEngine;
using System.Collections;

public class RotateRect : MonoBehaviour
{
    private RectTransform rect;

    public float Speed = -10;
	public Vector3 RotateSpeed = Vector3.zero;

	void Start ()
	{
	    rect = GetComponent<RectTransform>();
	}
	
	void Update () {
		if(Speed != 0f)
        	rect.Rotate(new Vector3(0, 0, Speed * Time.unscaledDeltaTime));
		else
			rect.Rotate(RotateSpeed * Time.unscaledDeltaTime);
    }
}
