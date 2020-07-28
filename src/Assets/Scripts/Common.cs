using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;

public class Common : MonoBehaviour
{
    public static GameObject GetPrefabFromItemChar(string itemCode)
    {
        if(itemCode == "K") return GameController.Instance.ItemKye;
        else if(itemCode == "1") return GameController.Instance.Item7;
        else if(itemCode == "2") return GameController.Instance.Item8;
        else if(itemCode == "3") return GameController.Instance.Item9;
        else if(itemCode == "4") return GameController.Instance.Item4;
        else if(itemCode == "5") return GameController.Instance.Item5;
        else if(itemCode == "6") return GameController.Instance.Item6;
        else if(itemCode == "7") return GameController.Instance.Item1;
        else if(itemCode == "8") return GameController.Instance.Item2;
        else if(itemCode == "9") return GameController.Instance.Item3;
        else if(itemCode == "e") return GameController.Instance.ItemEarth;
        else if(itemCode == "*") return GameController.Instance.ItemDiamond;
        else if(itemCode == "b") return GameController.Instance.ItemBlockSquare;
        else if(itemCode == "B") return GameController.Instance.ItemBlockRound;
        else if(itemCode == "u") return GameController.Instance.ItemSliderUp;
        else if(itemCode == "d") return GameController.Instance.ItemSliderDown;
        else if(itemCode == "l") return GameController.Instance.ItemSliderLeft;
        else if(itemCode == "r") return GameController.Instance.ItemSliderRight;
        else if(itemCode == "s") return GameController.Instance.ItemStickerTB;
        else if(itemCode == "S") return GameController.Instance.ItemStickerLR;
        else if(itemCode == "U") return GameController.Instance.ItemBouncerUp;
        else if(itemCode == "D") return GameController.Instance.ItemBouncerDown;
        else if(itemCode == "L") return GameController.Instance.ItemBouncerLeft;
        else if(itemCode == "R") return GameController.Instance.ItemBouncerRight;
        else if(itemCode == "^") return GameController.Instance.ItemRockyUp;
        else if(itemCode == "v") return GameController.Instance.ItemRockyDown;
        else if(itemCode == "<") return GameController.Instance.ItemRockyLeft;
        else if(itemCode == ">") return GameController.Instance.ItemRockyRight;
        else if(itemCode == "T") return GameController.Instance.ItemTwister;
        else if(itemCode == "E") return GameController.Instance.ItemGnasher;
        else if(itemCode == "C") return GameController.Instance.ItemBlob;
        else if(itemCode == "~") return GameController.Instance.ItemVirus;
        else if(itemCode == "[") return GameController.Instance.ItemSpike;
        else if(itemCode == "a") return GameController.Instance.ItemAntiClocker;
        else if(itemCode == "c") return GameController.Instance.ItemClocker;
        else if(itemCode == "A") return GameController.Instance.ItemAutoSlider;
        else if(itemCode == "F") return GameController.Instance.ItemAutoRocky;
        else if(itemCode == "H") return GameController.Instance.ItemBlacky;
        else if(itemCode == "f") return GameController.Instance.ItemDoorLR;
        else if(itemCode == "g") return GameController.Instance.ItemDoorRL;
        else if(itemCode == "h") return GameController.Instance.ItemDoorUD;
        else if(itemCode == "i") return GameController.Instance.ItemDoorDU;
        else if(itemCode == "}") return GameController.Instance.ItemTimer3;
        else if(itemCode == "|") return GameController.Instance.ItemTimer4;
        else if(itemCode == "{") return GameController.Instance.ItemTimer5;
        else if(itemCode == "z") return GameController.Instance.ItemTimer6;
        else if(itemCode == "y") return GameController.Instance.ItemTimer7;
        else if(itemCode == "x") return GameController.Instance.ItemTimer8;
        else if(itemCode == "w") return GameController.Instance.ItemTimer9;
        // v3 new
        else if(itemCode == "!") return GameController.Instance.ItemSploder;
        
        return null;
    }

    public static bool CanOccupyBy(int x, int y, Item item)
    {
        if (item == null)
            return false;

        var spotItem = GetItem(x, y);
        var direction = new Vector2(x, y);
        x += (int)item.transform.position.x;
        y += (int)item.transform.position.y;

        if(!InBounds(x, y))
            return false;
        
        if(!IsEmpty(x, y))
        {
            if(spotItem && !spotItem.CanCurrentlyBePushed(direction))
                return false;
        }
        
        return true;
    }

    public static bool CanMoveBy(int x, int y, Item item)
    {
        if (item == null)
            return false;

        x += (int)item.transform.position.x;
        y += (int)item.transform.position.y;

        return CanMoveTo(x, y, item);
    }

    public static bool CanMoveTo(int x, int y, Item item)
    {
        if (item == null)
            return false;

        // intended to put multiple checks in here but apparently only doing 1 which doesnt even need item
        if (!InBounds(x, y)) return false;

        return true;
    }

    public static Item GetItemBy(Vector2 pos, Item item) { return GetItem(item.transform.position.x + pos.x, item.transform.position.y + pos.y); }
    public static Item GetItemBy(int x, int y, Item item) { return GetItem(item.transform.position.x + x, item.transform.position.y + y); }
    public static Item GetItem(Vector2 pos) { return GetItem((int)pos.x, (int)pos.y); }
    public static Item GetItem(float x, float y) { return GetItem((int)x, (int)y); }
    public static Item GetItem(int x, int y)
    {
        if(!InBounds(x, y)) return null;
        return GameController.Instance.Items[x, y];
    }

    public static void MoveItemBy(Vector2 delta, Item item) { MoveItem(item.transform.position.x + delta.x, item.transform.position.y + delta.y, item); }
    public static void MoveItem(float x, float y, Item item) { MoveItem((int)x, (int)y, item); }
    public static void MoveItem(int x, int y, Item item)
    {
        if(item == null) return;
        if(!InBounds(x, y)) return;

        var currentSpotItem = GetItem(item.transform.position.x, item.transform.position.y);
        var newSpotItem = GetItem(x, y);

        if(item.IsKye && newSpotItem != null && newSpotItem.IsDoor)
        {
            SetItem(item.transform.position.x, item.transform.position.y, null);
        }
        else if(item.IsKye && currentSpotItem != null && currentSpotItem.IsDoor)
        {
            SetItem(x, y, item);
        }
        else
        {
            SetItem(item.transform.position.x, item.transform.position.y, null);
            SetItem(x, y, item);
        }

        item.IsStuckToSticker = false;
        item.transform.position = new Vector3(x, y, 0);
        item.x = x;
        item.y = y;
    }

    public static void Effect(int x, int y)
    {
        Instantiate(GameController.Instance.SplodeEffect, new Vector3(x, y, 0), GameController.Instance.SplodeEffect.transform.rotation, GameController.Instance.ItemContainer);
    }

    public static void DestroyItem(Item item, bool showEffect = true)
    {
        SetItem(item.x, item.y, null);
        if(showEffect)
            Effect(item.x, item.y);
        item.gameObject.SetActive(false);
        Destroy(item.gameObject);
    }

    public static void SetItem(float x, float y, Item item) { SetItem((int)x, (int)y, item); }
    public static void SetItem(int x, int y, Item item)
    {
        GameController.Instance.Items[x, y] = item;
    }

    public static bool IsEmptyBy(Vector2 delta, Item item) { return IsEmpty((Vector2)item.transform.position + delta); }
    public static bool IsEmpty(Vector2 spot) { return IsEmpty((int)spot.x, (int)spot.y); }
    public static bool IsEmpty(float x, float y) { return IsEmpty((int)x, (int)y); }
    public static bool IsEmpty(int x, int y)
    {
        if(!InBounds(x, y)) return false;
        var item = GameController.Instance.Items[x, y];
        if(item == null)
            return true;
        return false;
    }

    public static bool InBounds(Vector2 position) { return InBounds((int)position.x, (int)position.y); }
    public static bool InBounds(float x, float y) { return InBounds((int)x, (int)y); }
    public static bool InBounds(int x, int y)
    {
        if(x >= 1 && x <= 28 && y >= 1 && y <= 18)
            return true;
        return false;
    }

    public static Color ChangeAlpha(Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static int GetRandom(int maxExclusive) { return GetRandom(0, maxExclusive); }
    public static int GetRandom(int min, int maxExclusive)
    {
        //RandomSeed();
        return UnityEngine.Random.Range(min, maxExclusive);
    }

    public static string Base64Encode(string text)
    {
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(text);
        return Convert.ToBase64String(bytesToEncode);
    }

    public static string Base64Decode(string text)
    {
        byte[] decodedBytes = Convert.FromBase64String(text);
        return Encoding.UTF8.GetString (decodedBytes);
    }

    public static void Rot90(ref Item item, bool anti = false)
    {
        if(item == null)
            return;
        
        if(anti == true)
        {
            if(item.Direction == Vector2.up) item.Direction = Vector2.right;
            else if(item.Direction == Vector2.right) item.Direction = Vector2.down;
            else if(item.Direction == Vector2.down) item.Direction = Vector2.left;
            else if(item.Direction == Vector2.left) item.Direction = Vector2.up;
        }
        else
        {
            if(item.Direction == Vector2.up) item.Direction = Vector2.left;
            else if(item.Direction == Vector2.right) item.Direction = Vector2.up;
            else if(item.Direction == Vector2.down) item.Direction = Vector2.right;
            else if(item.Direction == Vector2.left) item.Direction = Vector2.down;
        }

        item.transform.Rotate(0, 0, 90 * (anti ? -1 : 1));
    }

    public static void StickersFollowKye(Vector2 kye)
    {
        var left = GetItem(kye.x - 2, kye.y);
        var right = GetItem(kye.x + 2, kye.y);
        var down = GetItem(kye.x, kye.y - 2);
        var up = GetItem(kye.x, kye.y + 2);

        if(up && up.IsStickerUD) up.Move(Vector2.down, true, true);
        if(down && down.IsStickerUD) down.Move(Vector2.up, true, true);
        if(left && left.IsStickerLR) left.Move(Vector2.right, true, true);
        if(right && right.IsStickerLR) right.Move(Vector2.left, true, true);
    }

    public static void PullToSticker(Item item)
    {
        if (item == null)
            return;

        if (item.IsStuckToSticker)
            return;

        if(StickToTouching(item))
            return;

        var movement = SurroundingStickers(item.x, item.y);
        if(movement == Vector2.zero)
        {
            item.IsStuckToSticker = false;
            return;
        }
        if(Common.IsEmpty(item.x + movement.x, item.y + movement.y))
        {
            item.Move(movement);
            item.IsStuckToSticker = true;
        }
    }

    private static bool StickToTouching(Item item)
    {
        if (item == null)
            return false;

        var x = item.x;
        var y = item.y;

        var left = GetItem(x - 1, y);
        var right = GetItem(x + 1, y);
        var down = GetItem(x, y - 1);
        var up = GetItem(x, y + 1);

        item.IsStuckToSticker = false;

        if(left && left.IsStickerLR) item.IsStuckToSticker = true;
        if(right && right.IsStickerLR) item.IsStuckToSticker = true;
        if(down && down.IsStickerUD) item.IsStuckToSticker = true;
        if(up && up.IsStickerUD) item.IsStuckToSticker = true;

        return item.IsStuckToSticker;
    }

    private static Vector2 SurroundingStickers(int x, int y)
    {
        var left = GetItem(x - 2, y);
        var right = GetItem(x + 2, y);
        var down = GetItem(x, y - 2);
        var up = GetItem(x, y + 2);

        if((left && left.IsStickerLR) || (right && right.IsStickerLR) || (up && up.IsStickerUD) || (down && down.IsStickerUD))
        {
            return new Vector2(
                (left && left.IsStickerLR) ? -1 : ((right && right.IsStickerLR) ? 1 : 0),
                (down && down.IsStickerUD) ? -1 : ((up && up.IsStickerUD) ? 1 : 0)
            );
        }

        return Vector2.zero;
    }

    public static void SetLeft(ref RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }
 
    public static void SetRight(ref RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }
 
    public static void SetTop(ref RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
 
    public static void SetBottom(ref RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    public static Dictionary<string, string>.ValueCollection ShuffledLevels()
    {
        System.Random r = new System.Random();
        return Levels.Classic.OrderBy(x => r.Next()).ToDictionary(item => item.Key, item => item.Value).Values;
    }
}

