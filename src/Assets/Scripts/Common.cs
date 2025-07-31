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
        else if(itemCode == "H") return GameController.Instance.ItemBlackhole;
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
        else if(itemCode == "!") return GameController.Instance.ItemSploder;
        
        return null;
    }

    public static GameObject UnitPrefab(string itemName)
    {
        var code = GetKyeCodeFromItem(itemName);
        var prefab = GetPrefabFromItemChar(code);
        return prefab;
    }

    public static string ItemNameToNiceName(string itemName)
    {
        if (itemName == "clear") return "Eraser";
        else if (itemName == "kye") return "Kye";
        else if (itemName == "diamond") return "Diamond";

        else if (itemName == "wall1") return "Wall";
        else if (itemName == "wall2") return "Wall";
        else if (itemName == "wall3") return "Wall";
        else if (itemName == "wall4") return "Wall";
        else if (itemName == "wall5") return "Wall";
        else if (itemName == "wall6") return "Wall";
        else if (itemName == "wall7") return "Wall";
        else if (itemName == "wall8") return "Wall";
        else if (itemName == "wall9") return "Wall";

        else if (itemName == "earth") return "Earth";
        else if (itemName == "blocksquare") return "Block Square";
        else if (itemName == "blockrocky") return "Block Rocky";
        else if (itemName == "sliderup") return "Slider Up";
        else if (itemName == "sliderdown") return "Slider Down";
        else if (itemName == "sliderleft") return "Slider Left";
        else if (itemName == "sliderright") return "Slider Right";

        else if (itemName == "stickertb") return "Sticker TB";
        else if (itemName == "stickerlr") return "Sticker LR";
        else if (itemName == "bouncerup") return "Bouncer Up";
        else if (itemName == "bouncerdown") return "Bouncer Down";
        else if (itemName == "bouncerleft") return "Bouncer Left";
        else if (itemName == "bouncerright") return "Bouncer Right";
        else if (itemName == "rockyup") return "Rocky Up";
        else if (itemName == "rockydown") return "Rocky Down";
        else if (itemName == "rockyleft") return "Rocky Left";
        else if (itemName == "rockyright") return "Rocky Right";

        else if (itemName == "twister") return "Twister";
        else if (itemName == "gnasher") return "Gnasher";
        else if (itemName == "blob") return "Blob";
        else if (itemName == "virus") return "Virus";
        else if (itemName == "spike") return "Spike";

        else if (itemName == "anticlocker") return "Anti-Clocker";
        else if (itemName == "clocker") return "Clocker";
        else if (itemName == "autoslider") return "Auto Slider";
        else if (itemName == "autorocky") return "Auto Rocky";
        else if (itemName == "blackhole") return "Blackhole";
        else if (itemName == "doorlr") return "Door LR";
        else if (itemName == "doorrl") return "Door RL";
        else if (itemName == "doorud") return "Door UD";
        else if (itemName == "doordu") return "Door DU";

        else if (itemName == "timer3") return "Timer 3";
        else if (itemName == "timer4") return "Timer 4";
        else if (itemName == "timer5") return "Timer 5";
        else if (itemName == "timer6") return "Timer 6";
        else if (itemName == "timer7") return "Timer 7";
        else if (itemName == "timer8") return "Timer 8";
        else if (itemName == "timer9") return "Timer 9";

        else if (itemName == "sploder") return "Sploder";

        return "Unknown";
    }
    
    public static String GetKyeCodeFromItem(string itemName)
    {
        if (itemName == "clear") return " ";
        else if (itemName == "kye") return "K";
        else if (itemName == "diamond") return "*";

        else if (itemName == "wall1") return "7";
        else if (itemName == "wall2") return "8";
        else if (itemName == "wall3") return "9";
        else if (itemName == "wall4") return "4";
        else if (itemName == "wall5") return "5";
        else if (itemName == "wall6") return "6";
        else if (itemName == "wall7") return "1";
        else if (itemName == "wall8") return "2";
        else if (itemName == "wall9") return "3";

        else if (itemName == "earth") return "e";
        else if (itemName == "blocksquare") return "b";
        else if (itemName == "blockrocky") return "B";
        else if (itemName == "sliderup") return "u";
        else if (itemName == "sliderdown") return "d";
        else if (itemName == "sliderleft") return "l";
        else if (itemName == "sliderright") return "r";

        else if (itemName == "stickertb") return "s";
        else if (itemName == "stickerlr") return "S";
        else if (itemName == "bouncerup") return "U";
        else if (itemName == "bouncerdown") return "D";
        else if (itemName == "bouncerleft") return "L";
        else if (itemName == "bouncerright") return "R";
        else if (itemName == "rockyup") return "^";
        else if (itemName == "rockydown") return "v";
        else if (itemName == "rockyleft") return "<";
        else if (itemName == "rockyright") return ">";

        else if (itemName == "twister") return "T";
        else if (itemName == "gnasher") return "E";
        else if (itemName == "blob") return "C";
        else if (itemName == "virus") return "~";
        else if (itemName == "spike") return "[";

        else if (itemName == "anticlocker") return "a";
        else if (itemName == "clocker") return "c";
        else if (itemName == "autoslider") return "A";
        else if (itemName == "autorocky") return "F";
        else if (itemName == "blackhole") return "H";
        else if (itemName == "doorlr") return "f";
        else if (itemName == "doorrl") return "g";
        else if (itemName == "doorud") return "h";
        else if (itemName == "doordu") return "i";

        else if (itemName == "timer3") return "}";
        else if (itemName == "timer4") return "|";
        else if (itemName == "timer5") return "{";
        else if (itemName == "timer6") return "z";
        else if (itemName == "timer7") return "y";
        else if (itemName == "timer8") return "x";
        else if (itemName == "timer9") return "w";

        else if (itemName == "sploder") return "!";

        return " ";
    }

    public static string GetItemNameFromKyeCode(string kyeCode)
    {
        if (kyeCode == "") return "clear";
        else if (kyeCode == "K") return "kye";
        else if (kyeCode == "*") return "diamond";
        else if (kyeCode == "5") return "wall5";
        else if (kyeCode == "7") return "wall1";
        else if (kyeCode == "8") return "wall2";
        else if (kyeCode == "9") return "wall3";
        else if (kyeCode == "4") return "wall4";
        else if (kyeCode == "6") return "wall6";
        else if (kyeCode == "1") return "wall7";
        else if (kyeCode == "2") return "wall8";
        else if (kyeCode == "3") return "wall9";
        else if (kyeCode == "e") return "earth";
        else if (kyeCode == "b") return "blocksquare";
        else if (kyeCode == "B") return "blockrocky";
        else if (kyeCode == "u") return "sliderup";
        else if (kyeCode == "d") return "sliderdown";
        else if (kyeCode == "l") return "sliderleft";
        else if (kyeCode == "r") return "sliderright";
        else if (kyeCode == "s") return "stickertb";
        else if (kyeCode == "S") return "stickerlr";
        else if (kyeCode == "U") return "bouncerup";
        else if (kyeCode == "D") return "bouncerdown";
        else if (kyeCode == "L") return "bouncerleft";
        else if (kyeCode == "R") return "bouncerright";
        else if (kyeCode == "^") return "rockyup";
        else if (kyeCode == "v") return "rockydown";
        else if (kyeCode == "<") return "rockyleft";
        else if (kyeCode == ">") return "rockyright";
        else if (kyeCode == "T") return "twister";
        else if (kyeCode == "E") return "gnasher";
        else if (kyeCode == "C") return "blob";
        else if (kyeCode == "~") return "virus";
        else if (kyeCode == "[") return "spike";
        else if (kyeCode == "a") return "anticlocker";
        else if (kyeCode == "c") return "clocker";
        else if (kyeCode == "A") return "autoslider";
        else if (kyeCode == "F") return "autorocky";
        else if (kyeCode == "H") return "blackhole";
        else if (kyeCode == "f") return "doorlr";
        else if (kyeCode == "g") return "doorrl";
        else if (kyeCode == "h") return "doorud";
        else if (kyeCode == "i") return "doordu";
        else if (kyeCode == "}") return "timer3";
        else if (kyeCode == "|") return "timer4";
        else if (kyeCode == "{") return "timer5";
        else if (kyeCode == "z") return "timer6";
        else if (kyeCode == "y") return "timer7";
        else if (kyeCode == "x") return "timer8";
        else if (kyeCode == "w") return "timer9";
        else if (kyeCode == "!") return "sploder";

        return "clear";
    }

    public static bool CanOccupyBy(int x, int y, Item item)
    {
        if (item == null)
            return false;

        var direction = new Vector2(x, y);
        x += (int)item.transform.position.x;
        y += (int)item.transform.position.y;

        if (!InBounds(x, y))
            return false;

        var spotItem = GetItem(x, y);
        if (spotItem)
        {
            if (spotItem.IsBlackhole)
                return true;
            if (spotItem.IsDoor)
                return true;
            if (spotItem.DestroyOnPlayerContact)
                return true;
            if (!spotItem.CanCurrentlyBePushed(direction))
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

        Item spotItem = GetItem(x, y);
        if (spotItem && spotItem.IsDoor) {
            //return false;
        }
        else if (spotItem)
            return false;

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

        if (item.IsKye && newSpotItem != null && newSpotItem.IsDoor)
        {
            SetItem(item.transform.position.x, item.transform.position.y, null);
        }
        else if (item.IsKye && currentSpotItem != null && currentSpotItem.IsDoor)
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

    public static bool InOuterBounds(Vector2 position) { return InOuterBounds((int)position.x, (int)position.y); }
    public static bool InOuterBounds(float x, float y) { return InOuterBounds((int)x, (int)y); }
    public static bool InOuterBounds(int x, int y)
    {
        if(x >= 0f && x <= 30f && y >= 0f && y <= 20f)
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

