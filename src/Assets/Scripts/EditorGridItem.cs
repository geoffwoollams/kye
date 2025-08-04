using System;
using System.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditorGridItem : MonoBehaviour
{
    public LevelEditor _editor;
    public RectTransform _rect;
    public Image _image;
    public Sprite _setSprite;
    public string _setItem;


    public int x, y;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
    }

    void Start()
    {
        var xGrid = 800f / 30f;
        var yGrid = 545f / 20f;
        var setX = (x * xGrid) - 400f + (xGrid / 2f);
        var setY = (y * yGrid) - 545f + (yGrid / 2f);
        
        _rect.localPosition = new Vector3(setX, setY, 1);
        _rect.localScale = Vector3.one;
        _rect.sizeDelta = new Vector2(xGrid, yGrid);

    }

    public string GetCode()
    {
        return Common.GetKyeCodeFromItem(_setItem);
    }

    public void OnPointerEnter()
    {
        transform.SetAsLastSibling();
        _rect.localScale = Vector3.one * 1.2f;

        if (IsBorder() && !_editor._currentItem.StartsWith("wall"))
            return;

        _image.sprite = _editor._newSprite;
    }

    public void OnPointerExit()
    {
        _rect.localScale = Vector3.one;
        _image.sprite = _setSprite;
    }

    public void SetItem(string itemName)
    {
        if (itemName == "clear")
        {
            _editor._currentItem = itemName;
            _editor._newSprite = _editor.gridBackgroundSprite;
            return;
        }

        var kyeCode = Common.GetKyeCodeFromItem(itemName);
        var prefab = Common.GetPrefabFromItemChar(kyeCode);

        _editor._currentItem = itemName;
        _editor._newSprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public bool IsBorder() {
        if (x == 0 || x == 29)
            return true;
        else if (y == 0 || y == 19)
            return true;
        return false;
    }

    public void OnPointerClick(BaseEventData pointerEventData)
    {
        if (IsBorder() && !_editor._currentItem.StartsWith("wall"))
            return;

        var pointerEvent = pointerEventData as PointerEventData;
        if (pointerEvent.button == PointerEventData.InputButton.Right)
        {
            _setItem = "clear";
            _setSprite = _editor.gridBackgroundSprite;
            _image.sprite = _setSprite;
            _editor.CountDiamonds();
            return;
        }
        else if (_editor._currentItem == _setItem)
        {
            if (_setItem == "stickerlr") SetItem("stickertb");
            else if (_setItem == "stickertb") SetItem("stickerlr");

            else if (_setItem == "bouncerright") SetItem("bouncerdown");
            else if (_setItem == "bouncerdown") SetItem("bouncerleft");
            else if (_setItem == "bouncerleft") SetItem("bouncerup");
            else if (_setItem == "bouncerup") SetItem("bouncerright");

            else if (_setItem == "sliderright") SetItem("sliderdown");
            else if (_setItem == "sliderdown") SetItem("sliderleft");
            else if (_setItem == "sliderleft") SetItem("sliderup");
            else if (_setItem == "sliderup") SetItem("sliderright");

            else if (_setItem == "rockyright") SetItem("rockydown");
            else if (_setItem == "rockydown") SetItem("rockyleft");
            else if (_setItem == "rockyleft") SetItem("rockyup");
            else if (_setItem == "rockyup") SetItem("rockyright");

            else if (_setItem == "clocker") SetItem("anticlocker");
            else if (_setItem == "anticlocker") SetItem("clocker");

            else if (_setItem == "doorlr") SetItem("doorrl");
            else if (_setItem == "doorrl") SetItem("doorud");
            else if (_setItem == "doorud") SetItem("doordu");
            else if (_setItem == "doordu") SetItem("doorlr");

            else if (_setItem == "timer3") SetItem("timer4");
            else if (_setItem == "timer4") SetItem("timer5");
            else if (_setItem == "timer5") SetItem("timer6");
            else if (_setItem == "timer6") SetItem("timer7");
            else if (_setItem == "timer7") SetItem("timer8");
            else if (_setItem == "timer8") SetItem("timer9");
            else if (_setItem == "timer9") SetItem("timer3");

            //else SetItem("clear");
        }

        if (_editor._currentItem == "kye")
            _editor.SetKye(this);

        _setItem = _editor._currentItem;
        _setSprite = _editor._newSprite;
        _image.sprite = _setSprite;

        _editor.CountDiamonds();
        _editor.CurrentItemText.text = "Current Item: " + Common.ItemNameToNiceName(_editor._currentItem);
    }

    public void SetAs(String itemName)
    {
        if (itemName == "kye")
            _editor.SetKye(this);
        else if (itemName == "clear")
        {
            _setItem = "clear";
            _setSprite = _editor.gridBackgroundSprite;
            _image.sprite = _setSprite;
            _rect.localScale = Vector3.one;
            _editor.CountDiamonds();
            return;
        }

        _setItem = itemName;
        var kyeCode = Common.GetKyeCodeFromItem(itemName);
        var prefab = Common.GetPrefabFromItemChar(kyeCode);
        _setSprite = prefab.GetComponent<SpriteRenderer>().sprite;
        _image.sprite = _setSprite;

        _editor.CountDiamonds();
    }
}
