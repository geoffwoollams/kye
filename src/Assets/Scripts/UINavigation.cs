using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UINavigation : MonoBehaviour
{
    private EventSystem eventSystem;
    private GameObject lastSelectedElement;

    void Awake()
    {
        eventSystem = FindAnyObjectByType<EventSystem>();
        lastSelectedElement = eventSystem.firstSelectedGameObject;
    }

    void FixedUpdate()
    {
        if (eventSystem.currentSelectedGameObject && lastSelectedElement != eventSystem.currentSelectedGameObject)
            lastSelectedElement = eventSystem.currentSelectedGameObject;

        if (!eventSystem.currentSelectedGameObject && lastSelectedElement)
            eventSystem.SetSelectedGameObject(lastSelectedElement);

        if (GameController.Instance.GamePanel.activeSelf && !GameController.Instance.MainMenu.activeSelf)
        {
            // In-game shortcuts

            if (Keyboard.current != null)
            {
                if (Keyboard.current.rKey.wasPressedThisFrame)
                    GameController.Instance.RestartLevel();
                if (Keyboard.current.qKey.wasPressedThisFrame)
                    GameController.Instance.QuitGame();
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                    GameController.Instance.PauseToggle();
                if (Keyboard.current.oKey.wasPressedThisFrame)
                    GameController.Instance.Dev();
            }

            if (Gamepad.current != null)
            {
                if (Gamepad.current.selectButton.wasPressedThisFrame)
                    GameController.Instance.PauseToggle();
            }
        }

        if (GameController.Instance.EditorPanel.activeSelf)
        {
            // In-game shortcuts
            var editor = GameController.Instance.EditorPanel.GetComponent<LevelEditor>();

            if (Keyboard.current != null)
            {
                if (Keyboard.current.pKey.wasPressedThisFrame)
                    editor.ClickPlay();
                if (Keyboard.current.escapeKey.wasPressedThisFrame)
                    editor.ShowMenu();
            }

            if (Gamepad.current != null)
            {
                if (Gamepad.current.selectButton.wasPressedThisFrame)
                    editor.ShowMenu();
            }
        }
    }
}
