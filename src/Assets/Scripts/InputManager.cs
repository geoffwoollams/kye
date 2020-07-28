using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class InputManager : MonoBehaviour
{
    public bool IncludeWSAD = true;
    public bool IncludeKeyboardArrows = true;
    public bool IncludeNumeric = true;
    public bool IncludeNumericDiagonal = true;
    public bool IncludeJoystick = true;
    public bool IncludeDpad = true;
    public bool IncludeLeftStick = true;
    public bool IncludeRightStick = true;
    public bool IncludeSwipes = true;
    public float MinSwipeDistance = 0.1f;
    public float MaxSwipeTime = 1f;

    [HideInInspector]
    public float x, y;
    [HideInInspector]
    public bool AnyKey, AnyKeyUp, AnyKeyDown, AnyKeyChange;
    [HideInInspector]
    public Key Up, Down, Left, Right;
    [HideInInspector]
    public bool SwipedUp, SwipedDown, SwipedLeft, SwipedRight;

    public class Key
    {
        public bool isPressed;
        public bool isDown;
        public bool isUp;

        public void Reset()
        {
            isPressed = isDown = isUp = false;
        }

        public void SetIsDownIfTrue(bool value)
        {
            isDown = isDown || value;
            isPressed = isPressed || value;
        }
    }

    private Vector2 _swipeStartPosition;
    private float _swipeStartTime;
    private bool _canEnd;

    void Start()
    {
        Up = new Key();
        Down = new Key();
        Left = new Key();
        Right = new Key();
    }

    void Update()
    {
        Reset();
        GetTouchInput();
        GetButtonInput();
        ProcessInput();

        if(AnyKeyDown)
            _canEnd = false;
    }

    private void Reset()
    {
        Up.Reset();
        Down.Reset();
        Left.Reset();
        Right.Reset();
        AnyKey = AnyKeyUp = AnyKeyDown = AnyKeyChange = false;
        SwipedUp = SwipedDown = SwipedLeft = SwipedRight = false;
        x = 0;
        y = 0;
    }

    private void GetTouchInput()
    {
        if(Touchscreen.current == null)
            return;

        if(Touchscreen.current.touches.Count > 0)
		{
            TouchControl touch = Touchscreen.current.primaryTouch;
            if(touch == null)
                return;

			if(touch.phase.ReadValue() == TouchPhase.Began)
			{
				_swipeStartPosition = new Vector2(touch.position.x.ReadValue() / (float)Screen.width, touch.position.y.ReadValue() / (float)Screen.width);
				_swipeStartTime = Time.time;
                _canEnd = true;
			}
            else if(_canEnd && touch.phase.ReadValue() == TouchPhase.Ended)
			{
				if (Time.time - _swipeStartTime > MaxSwipeTime)
					return;

                _canEnd = false;

				Vector2 endPos = new Vector2(touch.position.x.ReadValue() / (float)Screen.width, touch.position.y.ReadValue() / (float)Screen.width);
				Vector2 swipe = new Vector2(endPos.x - _swipeStartPosition.x, endPos.y - _swipeStartPosition.y);

				if (swipe.magnitude < MinSwipeDistance)
					return;

				if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y))
                {
					if (swipe.x > 0)
						SwipedRight = true;
					else
						SwipedLeft = true;
				}
				else
                {
					if (swipe.y > 0)
						SwipedUp = true;
					else
						SwipedDown = true;
				}

                if(IncludeSwipes)
                {
                    Up.SetIsDownIfTrue(SwipedUp);
                    Down.SetIsDownIfTrue(SwipedDown);
                    Left.SetIsDownIfTrue(SwipedLeft);
                    Right.SetIsDownIfTrue(SwipedRight);
                    CalcAnys();
                }
			}
		}
    }

    private void CheckButton(ref Key key, ButtonControl button)
    {
        if(button.isPressed)
            key.isPressed = true;
        if(button.wasPressedThisFrame)
            key.isDown = true;
        if(button.wasReleasedThisFrame)
            key.isUp = true;
    }

    private void GetButtonInput()
    {
        if(Keyboard.current != null)
        {
            if(IncludeKeyboardArrows)
            {
                CheckButton(ref Up, Keyboard.current.upArrowKey);
                CheckButton(ref Down, Keyboard.current.downArrowKey);
                CheckButton(ref Left, Keyboard.current.leftArrowKey);
                CheckButton(ref Right, Keyboard.current.rightArrowKey);
            }
            if(IncludeWSAD)
            {
                CheckButton(ref Up, Keyboard.current.wKey);
                CheckButton(ref Down, Keyboard.current.sKey);
                CheckButton(ref Left, Keyboard.current.aKey);
                CheckButton(ref Right, Keyboard.current.dKey);
            }
            if(IncludeNumeric)
            {
                CheckButton(ref Up, Keyboard.current.numpad8Key);
                CheckButton(ref Down, Keyboard.current.numpad2Key);
                CheckButton(ref Left, Keyboard.current.numpad4Key);
                CheckButton(ref Right, Keyboard.current.numpad6Key);

                if(IncludeNumericDiagonal)
                {
                    CheckButton(ref Up, Keyboard.current.numpad7Key);
                    CheckButton(ref Left, Keyboard.current.numpad7Key);

                    CheckButton(ref Up, Keyboard.current.numpad9Key);
                    CheckButton(ref Right, Keyboard.current.numpad9Key);

                    CheckButton(ref Down, Keyboard.current.numpad3Key);
                    CheckButton(ref Right, Keyboard.current.numpad3Key);

                    CheckButton(ref Down, Keyboard.current.numpad1Key);
                    CheckButton(ref Left, Keyboard.current.numpad1Key);
                }
            }
        }

        if(Gamepad.current != null)
        {
            if(IncludeDpad)
            {
                CheckButton(ref Up, Gamepad.current.dpad.up);
                CheckButton(ref Down, Gamepad.current.dpad.down);
                CheckButton(ref Left, Gamepad.current.dpad.left);
                CheckButton(ref Right, Gamepad.current.dpad.right);
            }

            if(IncludeLeftStick)
            {
                CheckButton(ref Up, Gamepad.current.leftStick.up);
                CheckButton(ref Down, Gamepad.current.leftStick.down);
                CheckButton(ref Left, Gamepad.current.leftStick.left);
                CheckButton(ref Right, Gamepad.current.leftStick.right);
            }

            if(IncludeRightStick)
            {
                CheckButton(ref Up, Gamepad.current.rightStick.up);
                CheckButton(ref Down, Gamepad.current.rightStick.down);
                CheckButton(ref Left, Gamepad.current.rightStick.left);
                CheckButton(ref Right, Gamepad.current.rightStick.right);
            }
        }

        if(IncludeJoystick && Joystick.current != null)
        {
            CheckButton(ref Up, Joystick.current.stick.up);
            CheckButton(ref Down, Joystick.current.stick.down);
            CheckButton(ref Left, Joystick.current.stick.left);
            CheckButton(ref Right, Joystick.current.stick.right);
        }

        CalcAnys();
    }

    private void ProcessInput()
    {
        float moveX = 0;
        float moveY = 0;

        if(Up.isPressed) moveY++;
        else if(Down.isPressed) moveY--;
        
        if(Left.isPressed) moveX--;
        else if(Right.isPressed) moveX++;

        x = moveX;
        y = moveY;
    }

    public void SetXY(Vector2 newInput, bool setAnys = true)
    {
        x = newInput.x;
        y = newInput.y;

        if(setAnys && (x != 0 || y != 0))
        {
            AnyKey = true;
            AnyKeyDown = true;
            AnyKeyChange = true;
        }
    }

    private void CalcAnys()
    {
        AnyKey = Up.isPressed || Down.isPressed || Left.isPressed || Right.isPressed;
        AnyKeyDown = Up.isDown || Down.isDown || Left.isDown || Right.isDown;
        AnyKeyUp = Up.isUp || Down.isUp || Left.isUp || Right.isUp;
        AnyKeyChange = AnyKeyDown || AnyKeyUp;
    }

    public Vector2Int Get101()
    {
        var xi = x == 0 ? 0 : (x < 0 ? -1 : 1);
        var yi = y == 0 ? 0 : (y < 0 ? -1 : 1);
        return new Vector2Int((int)xi, (int)yi);
    }
}
