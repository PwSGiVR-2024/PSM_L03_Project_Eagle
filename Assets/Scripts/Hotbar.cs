using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Hotbar : MonoBehaviour
{
    [SerializeField] Image[] displaySlots;              // 3 sloty UI: lewy, środkowy, prawy
    public Sprite emptySlotSprite;            // Puste tło dla nieaktywnych
    [SerializeField] List<Item> itemList = new();  // Przedmioty w hotbarze
    [SerializeField] TMP_Text currentItemLabel;
    [SerializeField] Item hand;

    private int selectedIndex = 0;

    void Start()
    {
        AddItem(hand);
        UpdateDisplay();
    }

    void Update()
    {
        int totalSlots = itemList.Count; 

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll < 0f)
        {
            selectedIndex = (selectedIndex - 1 + totalSlots) % totalSlots;
            Debug.Log("Scroll up → selected index: " + selectedIndex);
            UpdateDisplay();
        }
        else if (scroll > 0f)
        {
            selectedIndex = (selectedIndex + 1) % totalSlots;
            Debug.Log("Scroll down → selected index: " + selectedIndex);
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        int total = itemList.Count;

        if (total == 0 && selectedIndex == 0)
        {
            // Tylko ręka
            displaySlots[0].sprite = emptySlotSprite;
            displaySlots[1].sprite = emptySlotSprite;
            displaySlots[2].sprite = emptySlotSprite;

            displaySlots[0].color = new Color(1f, 1f, 1f, 0.2f);
            displaySlots[1].color = new Color(1f, 1f, 1f, 1f);
            displaySlots[2].color = new Color(1f, 1f, 1f, 0.2f);
            return;
        }

        int left = (selectedIndex - 1 + total) % (total);
        int center = selectedIndex;
        int right = (selectedIndex + 1) % (total );

        Debug.Log($"Displaying - Left: {left}, Center: {center}, Right: {right}");

        DisplaySlot(0, left);
        DisplaySlot(1, center);
        currentItemLabel.text = itemList[selectedIndex].itemName;
        DisplaySlot(2, right);
    }

    void DisplaySlot(int slotIndex, int itemIndex)
    {

            int actualIndex = itemIndex;
            if (actualIndex >= 0 && actualIndex < itemList.Count)
            {
                displaySlots[slotIndex].sprite = itemList[actualIndex].itemIcon;
                displaySlots[slotIndex].color = slotIndex == 1 ? Color.white : new Color(1f, 1f, 1f, 0.4f);
            }
            else
            {
                displaySlots[slotIndex].sprite = emptySlotSprite;
                displaySlots[slotIndex].color = new Color(1f, 1f, 1f, 0.2f);
            }
        
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null && newItem.itemIcon != null)
        {
            if (itemList.Contains(newItem))
            {
                Debug.Log("Item already in hotbar.");
                return;
            }

            itemList.Add(newItem);
            UpdateDisplay();

            Debug.Log("Item added: " + newItem.name);
        }
        else
        {
            Debug.LogWarning("Dodawany item jest pusty lub nie ma przypisanego itemIcon.");
        }
    }
}
