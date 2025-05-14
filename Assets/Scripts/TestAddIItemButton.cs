using UnityEngine;
using UnityEngine.UI;

public class TestAddItemButton : MonoBehaviour
{
    public Hotbar hotbar; // Odniesienie do skryptu Hotbar
    public TestItem testItemInScene; // Odniesienie do obiektu na scenie
    public Button addItemButton; // Przycisk do dodawania przedmiotu

    void Start()
    {
        addItemButton.onClick.AddListener(AddItemToHotbar);
    }

    void AddItemToHotbar()
    {
        if (testItemInScene != null)
        {
            testItemInScene.Pickup(hotbar);
        }
    }
}
