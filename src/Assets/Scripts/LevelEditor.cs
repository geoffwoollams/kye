using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using SimpleFileBrowser;
using System.Linq;

public class LevelEditor : MonoBehaviour
{
    public GameController gameController;
    public Image _mouseImage;
    public RectTransform Bounds;
    public EditorGridItem editorGridItemPrefab;
    public Transform editorGridContainer;

    public bool IsEditing, HasChanges, IsPlaying, IsInMenu;

    public string _currentItem = "kye";

    private string _emptyLevel;

    public Sprite _newSprite;

    public EditorGridItem _kyeGridItem;

    public Vector3 m_GridSize = new(2.5f, 2.5f, 2.5f);

    public Sprite gridBackgroundSprite;

    public GameObject EditorMenu;

    List<EditorGridItem> GridItems = new List<EditorGridItem>();

    public TMPro.TMP_InputField LevelName, LevelHint, LevelFinish;
    public TMPro.TMP_Text CurrentItemText, DiamondCount;

    void Awake()
    {
        _newSprite = _mouseImage.sprite;
        //MouseItem.gameObject.SetActive(true);

        InitGridItems();
        NewLevel();
        HideMenu();
    }

    public void ClickOpen()
    {
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Kye Levels", ".kye"));
        FileBrowser.SetDefaultFilter(".kye");

        FileBrowser.ShowLoadDialog(
            FileBrowserOnOpenSuccess,
            FileBrowserOnCancel,
            FileBrowser.PickMode.Files, false, Application.dataPath
        );
    }

    public void SetTextFields(string name, string hint, string message)
    {
        LevelName.text = (name);
        LevelHint.text = (hint);
        LevelFinish.text = (message);
    }

    public void ClickSave()
    {
        if (CountDiamonds() == 0)
        {
            GameController.Instance.Message("Error", "You must have at least 1 diamond!");
            return;
        }

        FileBrowser.SetFilters(true, new FileBrowser.Filter("Kye Levels", ".kye"));
        FileBrowser.SetDefaultFilter(".kye");

        FileBrowser.ShowSaveDialog(
            FileBrowserOnSaveSuccess,
            FileBrowserOnCancel,
            FileBrowser.PickMode.Files, false, Application.dataPath
        );
    }

    public void FileBrowserOnSaveSuccess(string[] paths)
    {
        var level = GetLevel();
        string filePath = paths[0];
        System.IO.File.WriteAllText(filePath, "1\n" + level);
        HideMenu();
    }

    public void FileBrowserOnOpenSuccess(string[] paths)
    {
        string filePath = paths[0];

        if (System.IO.File.Exists(filePath))
        {
            var level = System.IO.File.ReadAllLines(filePath);
            var levelCount = Int32.Parse(level[0].Trim());

            level = level.Skip(1).ToArray();

            if (levelCount <= 1)
            {
                var levelLines = string.Join("\n", level);
                //levelLines.TrimStart('\n', ' ');
                LoadLevel(levelLines);
            }
            else
            {
                GameController.Instance.ShowLevelPicker();
                var KyeFile = GameController.Instance.Get23(level);
                GameController.Instance.LoadLevelPicker(KyeFile);

                /*
                foreach (var levelB64 in KyeFile.Values)
                {
                    // load first item
                    // todo: more
                    var levelDecoded = Common.Base64Decode(levelB64);
                    LoadLevel(levelDecoded);
                    return;
                }
                */
            }
        }
    }

    public void FileBrowserOnCancel()
    {
        // nothing?
    }

    public void ClickPlay()
    {
        if (CountDiamonds() == 0)
        {
            GameController.Instance.Message("Error", "You must have at least 1 diamond!");
            return;
        }
        
        IsEditing = false;
        HideMenu();
        GameController.Instance.HideMainMenu();
        GameController.Instance.FrameUI.SetActive(false);
        GameController.Instance.FrameSprites.SetActive(true);
        GameController.Instance.LoadLevelWithString(GetLevel());
    }

    public string GetLevel()
    {
        string level = LevelName.text + "\n";
        level += LevelHint.text + "\n";
        level += LevelFinish.text + "\n";

        for (int y = 19; y >= 0; y--)
        {
            for (int x = 0; x <= 29; x++)
            {
                foreach (EditorGridItem levelGridItem in GridItems)
                {
                    if (x == levelGridItem.x && y == levelGridItem.y)
                    {
                        level += levelGridItem.GetCode();
                        if (x == 29)
                            level += "\n";
                    }
                }
            }
        }

        return level;
    }

    public void ClearGrid()
    {
        foreach (EditorGridItem levelGridItem in GridItems)
        {
            levelGridItem.SetAs("clear");
        }
    }

    public void NewLevel()
    {
        SetTextFields("MYLEVEL", "Add a little hint here", "Yay you completed the level!");

        ClearGrid();

        foreach (EditorGridItem levelGridItem in GridItems)
        {
            if (levelGridItem.x == 0 || levelGridItem.x == 29 || levelGridItem.y == 0 || levelGridItem.y == 19)
            {
                levelGridItem.SetAs("wall5");
            }
            else if (levelGridItem.x == 15 && levelGridItem.y == 18)
            {
                levelGridItem.SetAs("kye");
            }
            else
            {
                levelGridItem.SetAs("clear");
            }
        }

        HideMenu();
    }

    public void LoadLevel(string level)
    {
        ClearGrid();

        string[] lines = level.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        //CurrentLevelID = LevelNameToID(lines[0]);
        var CurrentLevelName = lines[0];
        var CurrentLevelNotes = lines[1].Trim();
        var CurrentLevelFinishMessage = lines[2].Trim();

        SetTextFields(CurrentLevelName, CurrentLevelNotes, CurrentLevelFinishMessage);

        foreach (EditorGridItem levelGridItem in GridItems)
        {
            string itemChar = lines[3 + (19 - levelGridItem.y)][levelGridItem.x].ToString();
            var itemPrefab = Common.GetPrefabFromItemChar(itemChar);
            var itemName = Common.GetItemNameFromKyeCode(itemChar);

            levelGridItem.SetAs(itemName);
        }

        HideMenu();
    }

    public void SetKye(EditorGridItem that)
    {
        if (_kyeGridItem != null)
        {
            _kyeGridItem._setSprite = gridBackgroundSprite;
            _kyeGridItem._setItem = "clear";
            _kyeGridItem._image.sprite = gridBackgroundSprite;
            _kyeGridItem._rect.localScale = Vector3.one;
        }
        _kyeGridItem = that;
    }

    void InitGridItems()
    {
        for (int y = 19; y >= 0; y--)
        {
            for (int x = 0; x <= 29; x++)
            {
                try
                {
                    var item = Instantiate(editorGridItemPrefab, new Vector3(x, 19 - y, 1), editorGridItemPrefab.transform.rotation);
                    item.transform.SetParent(editorGridContainer);
                    item.name = "EditorGridItem (" + x + "x" + y + ")";
                    item._editor = this;
                    item.x = x;
                    item.y = y;
                    GridItems.Add(item);
                }
                catch (Exception ex)
                {
                    Debug.Log("[InitGridItems] " + ex.ToString());
                }
            }
        }
    }

    void Update()
    {
        m_GridSize.x = Bounds.rect.width / 30f;
        m_GridSize.y = Bounds.rect.height / 20f;

        Vector3 mouseSpot = Mouse.current.position.ReadValue();
        mouseSpot = new Vector3(
            Mathf.RoundToInt(mouseSpot.x / m_GridSize.x) * m_GridSize.x,
            Mathf.RoundToInt(mouseSpot.y / m_GridSize.y) * m_GridSize.y,
            Mathf.RoundToInt(mouseSpot.z / m_GridSize.z) * m_GridSize.z);

        if (InUIBounds(mouseSpot) && !IsInMenu)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public bool InUIBounds(Vector2 position) { return InUIBounds((int)position.x, (int)position.y); }
    public bool InUIBounds(float x, float y) { return InUIBounds((int)x, (int)y); }
    public bool InUIBounds(int x, int y)
    {
        Vector2 localMousePosition = Bounds.InverseTransformPoint(new Vector3(x, y, 0));
        if (Bounds.rect.Contains(localMousePosition))
        {
            return true;
        }
        return false;
    }

    public void Prep()
    {
        IsEditing = true;
        HasChanges = false;
        IsPlaying = false;

        gameController.EraseLevel();
        //gameController.InsertLevel(Common.Base64Encode(_emptyLevel), false);
    }

    public void GoHome()
    {
        //todo: check for unsaved changes etc
        IsEditing = false;
        HideMenu();
        gameController.ShowMainMenu();
    }

    public void ShowMenu()
    {
        EditorMenu.SetActive(true);
        Cursor.visible = true;
        IsInMenu = true;
    }

    public void HideMenu()
    {
        EditorMenu.SetActive(false);
        IsInMenu = false;
    }

    public void SetItem(string itemName)
    {
        _newSprite = Common.UnitPrefab(itemName).GetComponent<SpriteRenderer>().sprite;
    }

    public void SelectItem(string itemName)
    {
        _currentItem = itemName;

        CurrentItemText.text = "Current Item: " + Common.ItemNameToNiceName(itemName);

        if (itemName == "" || itemName == " " || itemName == "clear")
        {
            _newSprite = gridBackgroundSprite;
            _currentItem = "clear";
        }
        else
        {
            _newSprite = Common.UnitPrefab(itemName).GetComponent<SpriteRenderer>().sprite;
        }

        _mouseImage.sprite = _newSprite;
    }

    public int CountDiamonds()
    {
        int diamonds = 0;

        foreach (EditorGridItem levelGridItem in GridItems)
        {
            if (levelGridItem._setItem == "diamond")
                diamonds++;
        }

        DiamondCount.text = diamonds.ToString();

        return diamonds;
    }
}
