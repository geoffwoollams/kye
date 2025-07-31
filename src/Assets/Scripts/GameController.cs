using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
using System.Linq;
using SimpleFileBrowser;
using System.Buffers.Text;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public InputManager inputManager;

    [HideInInspector]
    public Vector2 StartPoint;
    [HideInInspector]
    public Item Kye;

    public Transform CameraTransform;

    public Canvas MainCanvas;

    public GameObject GamePanel, MainMenu, PauseMenu, HelpPanel, EditorPanel, LevelPickerPanel, LevelFailed, LevelComplete;
    public GameObject PlayButton, LoadLevelButton, ResumeButton, LevelFailedButton, LevelCompleteButton;
    public GameObject HelpContentContainer;
    public TMPro.TMP_InputField LoadLevelInput;
    public GameObject LevelPickerItem, LevelPickerContainer;
    public GameObject LevelPickerContainerContainer;
    
    public Transform ItemContainer;
    public TMPro.TMP_Text Diamonds;
    public TMPro.TMP_Text LevelNotes, LevelName;
    public Image Life1, Life2, Life3;

    public GameObject Item1, Item2, Item3, Item4, Item5, Item6, Item7, Item8, Item9;
    public GameObject ItemKye, ItemEarth, ItemDiamond;
    public GameObject ItemBlockSquare, ItemBlockRound;
    public GameObject ItemSliderUp, ItemSliderDown, ItemSliderLeft, ItemSliderRight;
    public GameObject ItemStickerTB, ItemStickerLR;
    public GameObject ItemBouncerUp, ItemBouncerDown, ItemBouncerLeft, ItemBouncerRight;
    public GameObject ItemRockyUp, ItemRockyDown, ItemRockyLeft, ItemRockyRight;
    public GameObject ItemTwister, ItemGnasher, ItemBlob, ItemVirus, ItemSpike;
    public GameObject ItemAntiClocker, ItemClocker, ItemAutoSlider, ItemAutoRocky, ItemBlackhole;
    public GameObject ItemDoorLR, ItemDoorRL, ItemDoorUD, ItemDoorDU;
    public GameObject ItemTimer3, ItemTimer4, ItemTimer5, ItemTimer6, ItemTimer7, ItemTimer8, ItemTimer9;
    public GameObject ItemSploder, SplodeEffect;
    public GameObject KyeGhost;

    private bool _autoFollowMouse;
    private float _lastAutoMove;
    private GameObject _autoFollowGhost;

    public Item[,] Items = new Item[30, 20];
    //public Item.ItemCollection[,] ItemCollections = new Item.ItemCollection[30, 20];

    private Vector2 _input;
    public Vector2Int _inputClickPosition, _lastInputClickPosition;
    private int _startLives = 3;
    private int _lives;
    private int _diamonds;
    private float _nextMoveTime;
    private float _movementDelayInitial = 0.4f;
    private float _movementDelay = 0.05f;
    private bool _canStartMoving;
    private int _itemCount;

    public int CurrentLevelID = 0;
    public string CurrentLevelName = "";
    public string CurrentLevelNameR = "";
    public string CurrentLevelNotes = "";
    public string CurrentLevelFinishMessage = "";

    private float _timeSinceLevelLoaded;
    private float _lastTimeUpdate;

    public LevelEditor levelEditor;
    public GameObject levelEditorBackground;

    public GameObject FrameUI, FrameSprites;

    public static Dictionary<string, string> KyeFile = new Dictionary<string, string>();
    
    void Awake()
    {
        Instance = this;

        LoadLocalFiles();

        LoadLevelPicker();
        ShowMainMenu();
    }

    void Start()
    {
        if(Kye == null)
            Kye = GameObject.Find("kye").GetComponent<Item>();

        ResetLevel();
        _canStartMoving = true;

        UpdateUI();
    }

    private void AddToLevelPicker()
    {
        
    }

    private void LoadLocalFiles() {
        //Levels.Classic.Add("test33333333","RklSU1QKSnVzdCBmb3IgcHJhY3RpY2UNClRoZSBmaXJzdCBsZXZlbCB3YXMgZm9yIHByYWN0aWNlLg0KNTU1NTU1NTU1NTU1NTU1NTU1NTU1NTU1NTU1NTU1DQo1VCAgIGUgICAgICAgSyogIGEgICAgZCBlICAgRTUNCjUgICAgYiA0NTU1NTYgICAgICAgIGEgIGIgICAgNQ0KNSAgICBiIGR2dnZ2ZCAgICAgICAgICAgYiAgICA1DQo1ICAgIGIgZHZ2dnZkICAgICAgICAgIGFiICAgIDUNCjVlYmJiZSBlZUJCZWUgICAgICAgYyAgIGViYmJlNQ0KNSAgICAgICAgICAgICAgIGEgICAgICAgICAgICA1DQo1IDhycmUgICAgICAgICAgICAgICAgYSBlbGw4IDUNCjUgNT4+ZSAgICAgIHMgIFMgICAgICAgIGU8PDUgNQ0KNSA1Pj5CICAgICAgICAgICAgICAgICAgQjw8NSA1DQo1IDU+PkIgICAgICAgICAgICAgICBiICBCPDw1IDUNCjUgNT4+ZSAgICAgIFMgIHMgICAgIFUgIGU8PDUgNQ0KNSAycnJlICAgICAgICAgICAgICAgYiAgZWxsMiA1DQo1ICAgICAgICAgICAgICAgICBiUmJiICAgICAgIDUNCjVlYmJiZSBlZWVlZWUgIDc1NTU1NTkgIGViYmJlNQ0KNSAgICBiIHVeXl5edSAgNSAgICAgNSAgYiAgICA1DQo1ICAgIGIgdV5eXl51ICA1ICAgICA1ICBiICAgIDUNCjUgICAgYiA0NTU1NTYgIDUgICAgIDUgIGIgICAgNQ0KNUMgICBlICAgICAgICAgZSAgWyAgZSAgZSAgIH41DQo1NTU1NTU1NTU1NTU1NTU1NTU1NTU1NTU1NTU1NTUNCg==");
    }

    private void LevelPickerItemInit(ref GameObject levelItem, string levelB64, int x, int y)
    {
        var LevelName = levelItem.transform.Find("LevelName").GetComponent<TMPro.TMP_Text>();
        var LevelNotes = levelItem.transform.Find("LevelNotes").GetComponent<TMPro.TMP_Text>();
        var LevelButton = levelItem.GetComponent<Button>();

        var levelItemRect = levelItem.GetComponent<RectTransform>();
        //Common.SetLeft(ref levelItemRect, 20);
        //Common.SetRight(ref levelItemRect, 20);


        var levelItemRectPosition = levelItemRect.anchoredPosition;
        //levelItemRectPosition.x = x;
        levelItemRectPosition.y = y;
        levelItemRect.anchoredPosition = levelItemRectPosition;

        var level = Common.Base64Decode(levelB64);

        string[] lines = level.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        LevelName.text = lines[0].Trim();
        LevelNotes.text = lines[1].Trim();

        levelItem.name = "LevelItem " + LevelName.text;

        LevelButton.onClick.AddListener(delegate{LevelPickerClick();});
    }

    public string GetLevelName(string b64) {
        var level = Common.Base64Decode(b64);

        string[] lines = level.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        return lines[0].Trim();
    }

    public void ClearLevelList()
    {
        LoadLevelInput.text = "";
        KyeFile = new Dictionary<string, string>();
        LoadLevelPicker();
    }

    public void LoadLevelPicker()
    {
        LoadLevelPicker(Levels.Classic);
    }

    public void LoadLevelPicker(Dictionary<string, string> levels)
    {
        while (LevelPickerContainer.transform.childCount > 0)
        {
            DestroyImmediate(LevelPickerContainer.transform.GetChild(0).gameObject);
        }

        var filter = LoadLevelInput.text.Trim().ToLower();

        int y = -5;
        int x = 0;

        //var levels = Common.ShuffledLevels();
        //var levels = Levels.Classic.Values;

        foreach (var levelB64 in levels.Values)
        {
            if (filter == "" || GetLevelName(levelB64).ToLower().Contains(filter))
            {
                var levelItem = Instantiate(LevelPickerItem);
                levelItem.transform.SetParent(LevelPickerContainer.transform, false);
                LevelPickerItemInit(ref levelItem, levelB64, x, y);
                y -= 55;
            }
        }

        //y -= 5;

        var container = LevelPickerContainer.GetComponent<RectTransform>();

        var sizeDelta = container.sizeDelta;
        sizeDelta.y = -y;
        container.sizeDelta = sizeDelta;
    }

    void LevelPickerClick()
    {
        if(EventSystem.current.currentSelectedGameObject == null)
            return;

        HideMainMenu();
        
        var LevelName = EventSystem.current.currentSelectedGameObject.transform.Find("LevelName").GetComponent<TMPro.TMP_Text>();
        LoadLevelWithNoR(LevelName.text);
    }

    public void SetCameraPosition(bool isMainMenu)
    {
        var p = CameraTransform.position;
        p.y = isMainMenu ? 9.5f : 10.5f;
        CameraTransform.position = p;
    }

    public void QuitGame()
    {
        AppController.Instance.Save();
        Application.Quit();
    }

    public void ShowMainMenu()
    {
        LoadMenuLevel();
        GamePanel.SetActive(false);
        PauseMenu.SetActive(false);
        HelpPanel.SetActive(false);
        EditorPanel.SetActive(false);
        levelEditorBackground.SetActive(false);
        LevelPickerPanel.SetActive(false);
        LevelPickerContainerContainer.SetActive(false);
        //LevelPickerContainerFunContainer.SetActive(false);
        //LevelPickerContainerCommunityContainer.SetActive(false);
        LevelFailed.SetActive(false);
        LevelComplete.SetActive(false);

        FrameSprites.SetActive(true);
        FrameUI.SetActive(false);

        MainMenu.SetActive(true);
        SetCameraPosition(true);

        SetCameraTouchX(false);

        if(MainMenu.activeSelf)
            //if (!EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(PlayButton);

        _canStartMoving = true;
    }

    public void HideMainMenu()
    {
        GamePanel.SetActive(false);
        PauseMenu.SetActive(false);
        MainMenu.SetActive(false);
        HelpPanel.SetActive(false);
        EditorPanel.SetActive(false);
        levelEditorBackground.SetActive(false);
        LevelPickerPanel.SetActive(false);
        LevelFailed.SetActive(false);
        LevelComplete.SetActive(false);
        SetCameraPosition(false);
    }

    public void BackToMainMenu()
    {
        ShowMainMenu();
    }

    public void PlayGame()
    {
        LoadLevelWithNoR(AppController.Instance.settings.LastLevelPlayed);
    }

    public void ShowHelp()
    {
        HideMainMenu();
        HelpPanel.SetActive(true);
    }

    public void ShowEditor()
    {
        HideMainMenu();

        FrameSprites.SetActive(false);
        FrameUI.SetActive(true);

        EditorPanel.SetActive(true);
        levelEditorBackground.SetActive(true);
        levelEditor.Prep();
    }

    public void ShowLevelPicker()
    {
        HideMainMenu();

        FrameSprites.SetActive(true);
        FrameUI.SetActive(false);
        
        ClearLevelList();
        LoadLevelInput.text = "";  //AppController.Instance.settings.LastLevelPlayed;
        LevelPickerContainerContainer.SetActive(true);
        //LevelPickerContainer.GetComponent<ScrollRect>().SetY(0);
        LevelPickerPanel.SetActive(true);
    }

    public void PauseToggle()
    {
        PauseMenu.SetActive(!PauseMenu.activeSelf);
        Time.timeScale = PauseMenu.activeSelf ? 0 : 1;

        if(PauseMenu.activeSelf)
        {
            if (!EventSystem.current.alreadySelecting)
                EventSystem.current.SetSelectedGameObject(ResumeButton);
            _canStartMoving = false;
        }
        else
            _canStartMoving = true;
    }

    public void RandomPicker()
    {
        HideMainMenu();
        LoadRandomLevel();
        UpdateUI();
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMenuLevel()
    {
        //LoadLevel(Levels.Menu);
        string level = "MAINMENU" + "\n";
        level += "Main Menu Level" + "\n";
        level += "Yay!" + "\n";
        level += "555555555555555555555555555555" + "\n";
        level += "5Fv*                      * K5" + "\n";
        level += "5vv8 RL  RL   RL   RL  RL 8a 5" + "\n";
        level += "5vv555555555555555556 75555 a5" + "\n";
        level += "5vv5!!  !!            2F155  5" + "\n";
        level += "5vv5!R   !                5  5" + "\n";
        level += "5vv5  **                  2  5" + "\n";
        level += "5wBH  **                     5" + "\n";
        level += "5  5!   L!                8d 5" + "\n";
        level += "5B 5!!  !!                5  5" + "\n";
        level += "5  5                      5 C5" + "\n";
        level += "5 B5     C          T     5  5" + "\n";
        level += "5  5                      5  5" + "\n";
        level += "5B 5                      5 c5" + "\n";
        level += "5  5    [E          ~     5c 5" + "\n";
        level += "5 B5                      5  5" + "\n";
        level += "5H*155555556llllll4H5555553hh5" + "\n";
        level += "5 e e e e            g xw   A5" + "\n";
        level += "5Te[e~eEe         sLefayz{|}l5" + "\n";
        level += "555555555555555555555555555555" + "\n";
        LoadLevelWithString(level);
    }

    public void LoadRandomLevel()
    {
        List<string> keyList = new List<string>(Levels.Classic.Keys);        
        string randomKey = keyList[Common.GetRandom(0, keyList.Count)];
        AppController.Instance.settings.LastLevelPlayed = randomKey;
        LoadLevelWithNoR(randomKey);
    }

    public void Dev()
    {
        string level = "DEV" + "\n";
        level += "Development Test Level" + "\n";
        level += "Yay!" + "\n";

        level += "555555555555555555555555F55555" + "\n";
        level += "5r                           5" + "\n";
        level += "5R                           5" + "\n";
        level += "5>                           5" + "\n";
        level += "5K     5 55 555555555        5" + "\n";
        level += "5      5s555 55555555        5" + "\n";
        level += "5      5B5555 5555555B       5" + "\n";
        level += "5      BB55555 555555BB      5" + "\n";
        level += "5     BBB555555555555BBB *   5" + "\n";
        level += "5    BBBB555555555555BBB     5" + "\n";
        level += "5   BBBBB555555555555BBB B   5" + "\n";
        level += "5  BBBBBB555555555555BBB BB  5" + "\n";
        level += "5 BBBBBBB555555555555BBBBBBB 5" + "\n";
        level += "5 BBBBBBB555555555555BBBBBBB 5" + "\n";
        level += "5       *555555555555*       5" + "\n";
        level += "5        555555555555        5" + "\n";
        level += "5        555555555555        5" + "\n";
        level += "5        555555555555        5" + "\n";
        level += "5        555555555555        5" + "\n";
        level += "555555555555555555555555555555" + "\n";

        LoadLevelWithString(level);
        AppController.Instance.settings.LastLevelPlayed = "DEV";
    }

    public void LoadLevelWithString(string level)
    {
        LoadLevelWithB64(Common.Base64Encode(level));
    }

    public void LoadLevelWithB64(string levelB64)
    {
        HideMainMenu();
        CurrentLevelID = 0;
        LoadLevel(levelB64);
        LoadLevelInput.text = "";
    }

    public void ClickOpen()
    {
        FileBrowser.SetFilters( true, new FileBrowser.Filter( "Kye Levels", ".kye" ) );
        FileBrowser.SetDefaultFilter( ".kye" );

        FileBrowser.ShowLoadDialog(
            FileBrowserOnSuccess,
            FileBrowserOnCancel,
            FileBrowser.PickMode.Files, false, Application.dataPath
        );
    }

    public void FileBrowserOnSuccess(string[] paths)
    {
        string filePath = paths[0];
        LoadLevelWithKyeFile(filePath);
    }
    
    public void FileBrowserOnCancel()
    {
        // nothing?
    }

    public void LoadLevelWithKyeFile(string file)
    {
        if (System.IO.File.Exists(file))
        {
            var level = System.IO.File.ReadAllLines(file);
            var levelCount = Int32.Parse(level[0].Trim());

            level = level.Skip(1).ToArray();

            if (levelCount <= 1)
            {
                var levelLines = string.Join("\n", level);
                //levelLines.TrimStart('\n', ' ');
                LoadLevelWithString(levelLines);
            }
            else
            {
                KyeFile = Get23(level);
                LoadLevelPicker(KyeFile);
            }
        }
    }

    public Dictionary<string, string> Get23(string[] levels)
    {
        int i = 0;
        //string[] levelsRet;
        string thisLevel = "";

        Dictionary<string, string> LoadKyeFile = new Dictionary<string, string>();

        foreach (string levelLine in levels)
        {
            thisLevel = thisLevel + "\n" + levelLine;
            i++;

            if (i == 23)
            {
                thisLevel = thisLevel.Substring(1);

                var b64 = Common.Base64Encode(thisLevel);
                var lName = GetLevelName(b64);
                if (LoadKyeFile.ContainsKey(lName))
                {
                    Debug.Log("Existing Key: " + lName);
                    lName = lName + "_" + UnityEngine.Random.Range(1000, 9999);
                }
                LoadKyeFile.Add(lName, b64);
                thisLevel = "";
                i = 0;
            }
        }

        return LoadKyeFile;
    }

    public void LoadLevelWithNoR(string levelName, bool setInputOnFail = false)
    {
        HideMainMenu();

        if (levelName.Trim() == "")
            levelName = "first";

        AppController.Instance.settings.LastLevelPlayed = levelName;
        levelName = levelName.ToLower();

        if (KyeFile.Count > 0 && KyeFile.ContainsKey(levelName))
        {
            CurrentLevelID = 0;
            LoadLevel(KyeFile[levelName]);
            return;
        }
        else if (levelName == "dev")
        {
            Dev();
            return;
        }
        else if (Levels.Classic.ContainsKey(levelName))
        {
            CurrentLevelID = LevelNameToID(levelName);
            LoadLevel(Levels.Classic[levelName]);
            return;
        }
        else
        {
            levelName = "first";
            CurrentLevelID = LevelNameToID(levelName);
            LoadLevel(Levels.Classic[levelName]);
            return;
        }

        /*
        if (setInputOnFail)
            LoadLevelInput.text = "";
        else
            HasError();
        */
    }

    public void PrevLevel()
    {
        HideMainMenu();
        CurrentLevelID--;
        CurrentLevelID = Mathf.Clamp(CurrentLevelID, 0, Levels.Classic.Count - 1);
        LoadLevel(CurrentLevelID);
    }

    public void NextLevel()
    {
        HideMainMenu();
        CurrentLevelID++;
        CurrentLevelID = Mathf.Clamp(CurrentLevelID, 0, Levels.Classic.Count - 1);
        LoadLevel(CurrentLevelID);
    }

    private int LevelNameToID(string levelName)
    {
        if(!Levels.Classic.ContainsKey(levelName))
            return 0;
        
        int id = 0;
        foreach(var level in Levels.Classic)
        {
            if(level.Key == levelName)
                return id;
            id++;
        }

        return 0;
    }

    private string LevelIDToName(int levelID)
    {
        int id = 0;
        foreach(var level in Levels.Classic)
        {
            if(id == levelID)
                return level.Key;
            id++;
        }

        return "";
    }

    public void SaveTheChildren()
    {
        Application.OpenURL("https://www.savethechildren.org/");
    }

    private void CountDiamonds()
    {
        _diamonds = 0;
        foreach(var item in Items)
        {
            if(item == null)
                continue;
            
            if(item.IsDiamond)
                _diamonds++;
        }
    }

    public void PrepInput()
    {
        if(LoadLevelInput.text == "No such level...")
            LoadLevelInput.text = "";
    }

    public void LoadLevelFromMenu()
    {
        var attempt = LoadLevelInput.text.ToLower();

        // cheat code first so it can also be a level - for one off level cheats etc
        CheatManager.TryCheat(attempt);

        if(attempt == "dev")
        {
            Dev();
            return;
        }
        else if(!Levels.Classic.ContainsKey(attempt.ToLower()))
        {
            LoadLevelInput.text = "";
            return;
        }

        LoadLevelWithNoR(attempt, true);
    }

    public void LoadLevel(int levelID)
    {
        LoadLevelWithNoR(LevelIDToName(levelID));
    }

    private void LoadLevel(string level)
    {
        try
        {
            EraseLevel();
            InsertLevel(level);
            AppController.Instance.Save();
            _canStartMoving = true;
            Time.timeScale = 1;
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
            HasError();
        }
    }

    public void EraseLevel()
    {
        foreach (Transform item in ItemContainer.transform)
        {
            item.gameObject.SetActive(false);
            Destroy(item.gameObject);
        }

        Items = new Item[30, 20];
        _lives = _startLives;
        _itemCount = 0;
    }

    public void InsertLevel(string level, bool isEditor = false)
    {
        level = Common.Base64Decode(level);

        string[] lines = level.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );

        //CurrentLevelID = LevelNameToID(lines[0]);
        CurrentLevelName = lines[0];
        CurrentLevelNotes = lines[1].Trim();
        CurrentLevelFinishMessage = lines[2].Trim();

        for(int y = 19; y >= 0; y--)
        {
            for(int x = 0; x <= 29; x++)
            {
                try
                {
                    string itemChar = lines[3 + y][x].ToString();
                    var itemPrefab = Common.GetPrefabFromItemChar(itemChar);

                    if(itemPrefab != null)
                    {
                        var item = Instantiate(itemPrefab, new Vector3(x, 19 - y, 0), itemPrefab.transform.rotation);
                        item.transform.SetParent(ItemContainer);
                        
                        NewItem(item.GetComponent<Item>());

                        if(itemChar == "K")
                        {
                            Kye = item.GetComponent<Item>();
                            StartPoint = item.transform.position;
                        }
                    }
                }
                catch(Exception ex)
                {
                    Debug.Log("[InsertLevel] " + ex.ToString());
                }
            }
        }

        if(isEditor && false) {
            Time.timeScale = 0;
        }
        else {

            if(Kye == null)
            {
                var itemPrefab = Common.GetPrefabFromItemChar("K");

                var three = Common.GetItem(3, 3);
                if(three != null)
                {
                    Common.DestroyItem(three, false);
                }

                var item = Instantiate(itemPrefab, new Vector3(3, 3, 0), itemPrefab.transform.rotation);
                Kye = item.GetComponent<Item>();
            }

            //if(!CurrentLevelName.Contains("MENU"))
            //    LoadLevelInput.text = CurrentLevelName;
            
            Time.timeScale = 1;
            _timeSinceLevelLoaded = 0;
            GamePanel.SetActive(true);
            ResetLevel();
            UpdateUI();
        }
    }

    public void NewItem(Item item)
    {
        item.x = (int)item.transform.position.x;
        item.y = (int)item.transform.position.y;
        item.Thinker = _itemCount;
        _itemCount++;
    }

    public void RestartLevel()
    {
        try
        {
            HideMainMenu();
            LoadLevelWithNoR(AppController.Instance.settings.LastLevelPlayed);
        }
        catch(Exception ex)
        {
            Debug.Log(ex.ToString());
            HasError();
        }
    }

    public void HasError()
    {
        ShowMainMenu();
    }

    // not a restart of the level (things back in place), but a reset (kye back to start)
    private void ResetLevel()
    {
        _canStartMoving = false;
        CountDiamonds();
        Common.MoveItem(StartPoint.x, StartPoint.y, Kye);

        if(GamePanel.activeSelf)
        {
            // todo: dont do this on life loss. need new keydown for movement start etc
            _canStartMoving = true;
        }
    }

    private void StepToClickPosition()
    {
        Vector2Int direction = Vector2Int.zero;
        if(Kye.position != _inputClickPosition)
        {
            var spotItem = Common.GetItem(_inputClickPosition);
            if(spotItem == null || spotItem.IsDoor || spotItem.DestroyOnPlayerContact || spotItem.IsDiamond)
            {
                if(_autoFollowMouse)
                {
                    // Auto move, move the transparent Kye to _inputClickPosition
                    var ghostSpot = new Vector3(_inputClickPosition.x, _inputClickPosition.y, 0);
                    if(_autoFollowGhost == null)
                    {
                        _autoFollowGhost = Instantiate(KyeGhost, ghostSpot, KyeGhost.transform.rotation);
                        Destroy(_autoFollowGhost.GetComponent<FadeAndDestroy>());
                    }
                    else
                        _autoFollowGhost.transform.position = ghostSpot;
                }
                else
                {
                    // Manual click, put a ghost Kye there.
                    var ghostPosition = new Vector3(_inputClickPosition.x, _inputClickPosition.y, 0);
                    var kyeGhost = Instantiate(KyeGhost, ghostPosition, KyeGhost.transform.rotation);
                }
            }

            if(_autoFollowMouse && Time.time - _lastAutoMove < (_movementDelay * 3))
                return;

            direction = _inputClickPosition - Kye.position;

            if(direction.y != 0)
                direction.y = direction.y > 0 ? 1 : -1;
            if(direction.x != 0)
                direction.x = direction.x > 0 ? 1 : -1;
            
            /*
            if(direction.y != 0 && Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
            {
                direction.y = direction.y > 0 ? 1 : -1;
            }
            else if(direction.x != 0 && Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                direction.x = direction.x > 0 ? 1 : -1;
            }
            else if(Kye.y != _inputClickPosition.y && Common.IsEmptyBy(Vector2.up * (direction.y > 0 ? 1 : -1), Kye))
            {
                direction.y = direction.y > 0 ? 1 : -1;
            }
            else if(Kye.x != _inputClickPosition.x && Common.IsEmptyBy(Vector2.right * (direction.x > 0 ? 1 : -1), Kye))
            {
                direction.x = direction.x > 0 ? 1 : -1;
            }
            */

            GameController.Instance.inputManager.SetXY(direction);
            _lastAutoMove = Time.time;
        }
        // use a inputmanager function to override the input for the upcoming Get101()
        // use code from enemies to move towards kye
        // set inputmanager.anygetdown etc to true so mouse movement triggers things
        // ^ maybe an inputmanager feature - IsMouseMovementATrigger
    }

    private void ProcessInput()
    {
        if(Keyboard.current != null)
        {
            if(Keyboard.current.rKey.wasPressedThisFrame)
                RestartLevel();
            if(Keyboard.current.qKey.wasPressedThisFrame)
                QuitGame();
            if(Keyboard.current.escapeKey.wasPressedThisFrame)
                PauseToggle();
            if(Keyboard.current.oKey.wasPressedThisFrame)
                Dev();
        }

        if(Mouse.current != null)
        {
            _lastInputClickPosition = _inputClickPosition;
            _inputClickPosition = Vector2Int.down;

            if(Mouse.current.rightButton.wasPressedThisFrame)
            {
                _autoFollowMouse = !_autoFollowMouse;

                if(!_autoFollowMouse && _autoFollowGhost != null)
                    Destroy(_autoFollowGhost);
            }

            if(Mouse.current.leftButton.wasPressedThisFrame || _autoFollowMouse) 
            {
                var clickPosition = CameraTransform.GetComponent<Camera>().ScreenToWorldPoint(Mouse.current.position.ReadValue());
                _inputClickPosition.x = Mathf.RoundToInt(clickPosition.x);
                _inputClickPosition.y = Mathf.RoundToInt(clickPosition.y);
            }

            if(_inputClickPosition != Vector2Int.down && Common.InBounds(_inputClickPosition))
                StepToClickPosition();
        }

        var input = inputManager.Get101();

        int moveX = input.x;
        int moveY = input.y;

        if(inputManager.AnyKeyDown && !LevelFailed.activeSelf && !LevelComplete.activeSelf && !PauseMenu.activeSelf)
            _canStartMoving = true;
        
        if(inputManager.AnyKeyChange)
            _nextMoveTime = 0;

        // Set input to 0 if blocked to allow the other direction to move
        if(moveX != 0f && !Common.CanOccupyBy(moveX, 0, Kye))
            moveX = 0;
        if(moveY != 0f && !Common.CanOccupyBy(0, moveY, Kye))
            moveY = 0;

        if (moveX != 0f && moveY != 0f)
        {
            if(!Common.CanOccupyBy(moveX, moveY, Kye))
                moveX = moveY = 0;
            else if (!Common.CanOccupyBy(moveX, 0, Kye) && !Common.CanOccupyBy(0, moveY, Kye))
                moveX = moveY = 0;
        }

        _input.x = moveX;
        _input.y = moveY;
    }

    void Update()
    {
        if(levelEditor.IsEditing)
            return;

        CheckInputForGameStuff();

        if(!GamePanel.activeSelf)
        {
            /*
            if(Keyboard.current != null)
            {
                if(Keyboard.current.escapeKey.wasPressedThisFrame)
                    PauseToggle();
            }
            */

            Tick();
            return;
        }

        _timeSinceLevelLoaded += Time.deltaTime;

        TickKye();
        Tick();

        if(_diamonds == 0 && _canStartMoving)
            FinishLevel();
    }

    private void CheckInputForGameStuff()
    {
        float scrollY = GameController.Instance._input.y;

        if(Mouse.current != null)
        {
            var mouseScrollY = Mouse.current.scroll.ReadValue().y;
            if(mouseScrollY != 0)
                scrollY = mouseScrollY;
        }

        if(scrollY != 0)
        {
            scrollY *= -0.01f;

            if (LevelPickerPanel.activeSelf)
            {
                LevelPickerContainer.GetComponent<ScrollRect>().Scroll(scrollY);
            }
            else if(HelpPanel.activeSelf)
            {
                HelpContentContainer.GetComponent<ScrollRect>().Scroll(scrollY);
            }
        }
    }

    private bool CheckKyeForBeasts()
    {
        if(Kye == null)
            return false;
        
        var left = Common.GetItem(Kye.x - 1, Kye.y);
        var right = Common.GetItem(Kye.x + 1, Kye.y);
        var down = Common.GetItem(Kye.x, Kye.y - 1);
        var up = Common.GetItem(Kye.x, Kye.y + 1);

        if(
            (left && left.KillsPlayer && !left.IsStuckToSticker) || 
            (right && right.KillsPlayer && !right.IsStuckToSticker) || 
            (up && up.KillsPlayer && !up.IsStuckToSticker) || 
            (down && down.KillsPlayer && !down.IsStuckToSticker))
        {
            KillPlayer();
            return true;
        }

        return false;
    }

    private void TickKye()
    {
        bool canMove = true;

        Vector2 prevPos = new Vector2(Kye.x, Kye.y);

        ProcessInput();

        if(!_canStartMoving)
            return;

        if(CheckKyeForBeasts())
            return;

        if(_input == Vector2.zero)
            return; // No input

        if(Time.time < _nextMoveTime)
            return; // Too soon since last movement
        
        int x = (int)Kye.x + (int)_input.x;
        int y = (int)Kye.y + (int)_input.y;

        var item = Common.GetItem(x, y);

        ProcessItem(item, ref canMove);

        if(canMove)
        {
            Common.MoveItem(x, y, Kye);

            float thisDelay = _movementDelay;
            if(inputManager.AnyKeyChange)
                thisDelay = _movementDelayInitial;

            _nextMoveTime = Time.time + thisDelay;

            if(CheckKyeForBeasts())
                return;

            Vector2 currentPos = new Vector2(Kye.x, Kye.y);
            if(currentPos != prevPos)
                Common.StickersFollowKye(currentPos);
        }
    }

    private void Tick()
    {
        if(!_canStartMoving)
            return;
        
        var Thinkers = ItemContainer.GetComponentsInChildren<Item>();

        for(int thinker = 0; thinker <= _itemCount; thinker++)
        {
            foreach (Item item in Thinkers)
            {
                if(item == null) continue;                
                if(item.Thinker != thinker) continue;
                item.Tick();
            }
        }
    }

    private void FinishLevel()
    {
        _canStartMoving = false;
        AppController.Instance.Save();
        var message = LevelComplete.transform.Find("Message").GetComponent<TMPro.TMP_Text>();
        message.text = CurrentLevelFinishMessage;
        LevelComplete.SetActive(true);
        EventSystem.current.SetSelectedGameObject(LevelCompleteButton);
        Time.timeScale = 0;
    }

    private void ProcessItem(Item item, ref bool canMove)
    {
        if(item == null)
            return;
        
        if(item.IsDiamond)
        {
            _diamonds--;
            Common.DestroyItem(item, false);
            UpdateUI();
            return;
        }

        if(item.IsSploder)
        {
            item._sploder.Tock();
        }

        if((item.KillsPlayer || item.IsBlackhole) && !CheatManager.GodMode)
        {
            if(item.IsBlackhole)
            {
                item.GetComponent<Blackhole>().Consume(Kye);
            }
            
            KillPlayer();
            canMove = false;
            return;
        }
        if(item.DestroyOnPlayerContact)
        {
            Common.DestroyItem(item, false);
            return;
        }
        if(item.IsDoor)
        {
            canMove = item.Direction == _input;
            return;
        }
        
        if(item.IsPushable)
        {
            Vector2 spot = (Vector2)item.transform.position + _input;
            if(!Common.InBounds(spot.x, spot.y))
            {
                canMove = false;
                return;
            }

            var itemAtSpot = Common.GetItem(spot);
            if (itemAtSpot == null)
            {
                item.Move((int)_input.x, (int)_input.y, true);
                item.IsStuckToSticker = false;
            }
            else if (itemAtSpot.IsBlackhole)
            {
                itemAtSpot.GetComponent<Blackhole>().Consume(item);

                KillPlayer();
                canMove = false;
                return;
            }
            else
                canMove = false;
        }
        else
        {
            canMove = false;
        }
    }

    public void Kill(string message = "Beware of beasts and blackholes!")
    {
        _lives = 0;
        _canStartMoving = false;
        LevelFailed.transform.Find("Reason").GetComponent<TMPro.TMP_Text>().text = message;
        LevelFailed.SetActive(true);
        EventSystem.current.SetSelectedGameObject(LevelFailedButton);
        Time.timeScale = 0;
    }

    public void KillPlayer(string message = "You ran out of lives!")
    {
        if(!CheatManager.UnlimitedLives)
            _lives--;
        
        if(_lives > 0)
        {
            var ghostPosition = new Vector3(Kye.x, Kye.y, 0);
            var kyeGhost = Instantiate(KyeGhost, ghostPosition, KyeGhost.transform.rotation);

            UpdateUI();

            if(Common.IsEmpty(StartPoint.x, StartPoint.y) || Common.GetItem(StartPoint.x, StartPoint.y).IsKye)
            {
                ResetLevel();
            }
            else
            {
                Debug.Log("handle this killplayer() respawn not empty");
                RestartLevel();
                //Kye.transform.position = StartPoint;
            }
        }
        else
        {
            UpdateUI();
            Kill(message);
        }
    }

    private void UpdateUI()
    {
        Diamonds.text = _diamonds.ToString();
        LevelName.text = CurrentLevelName;
        LevelNotes.text = CurrentLevelNotes;

        if(_lives == 3)
        {
            Life1.color = Common.ChangeAlpha(Life1.color, 1f);
            Life2.color = Common.ChangeAlpha(Life1.color, 1f);
            Life3.color = Common.ChangeAlpha(Life1.color, 1f);
        }
        else if(_lives == 2)
        {
            Life1.color = Common.ChangeAlpha(Life1.color, 1f);
            Life2.color = Common.ChangeAlpha(Life1.color, 1f);
            Life3.color = Common.ChangeAlpha(Life1.color, 0.2f);
        }
        else if(_lives == 1)
        {
            Life1.color = Common.ChangeAlpha(Life1.color, 1f);
            Life2.color = Common.ChangeAlpha(Life1.color, 0.2f);
            Life3.color = Common.ChangeAlpha(Life1.color, 0.2f);
        }
        else if (_lives == 0)
        {
            Life1.color = Common.ChangeAlpha(Life1.color, 0.2f);
            Life2.color = Common.ChangeAlpha(Life1.color, 0.2f);
            Life3.color = Common.ChangeAlpha(Life1.color, 0.2f);
        }

        var stick = GamePanel.transform.Find("Stick");

        if(Touchscreen.current == null)
        {
            stick.gameObject.SetActive(false);
            SetCameraTouchX(false);
        }
        else
        {
            stick.gameObject.SetActive(true);
            SetCameraTouchX(true);
        }
    }

    private void SetCameraTouchX(bool offset)
    {
        float GameXOffset = 0;

        if(offset && GamePanel.activeSelf)
        {
            var GameUnitsWide = Camera.main.aspect * Camera.main.orthographicSize * 2f;
            GameXOffset = (GameUnitsWide - 30) / 2f;
            GameXOffset--; // Give them 1 extra block
        }

        var camPos = CameraTransform.position;
        camPos.x = 14.5f + GameXOffset;
        CameraTransform.position = camPos;
    }
}

public static class DictionaryExtensions
{
   public static Dictionary<TKey, TValue> Shuffle<TKey, TValue>(
      this Dictionary<TKey, TValue> source)
   {
      System.Random r = new System.Random();
      return source.OrderBy(x => r.Next())
         .ToDictionary(item => item.Key, item => item.Value);
   }
}