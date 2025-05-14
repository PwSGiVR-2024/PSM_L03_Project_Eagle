using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Hotbar : MonoBehaviour
{
    public Image[] displaySlots;              // 3 sloty UI: lewy, środkowy, prawy
    public Sprite emptySlotSprite;            // Puste tło dla nieaktywnych
    public Sprite handIcon;                   // Ikona pustej ręki
    public List<Item> itemIcons = new List<Item>();  // Przedmioty w hotbarze

    private int selectedIndex = 0;  // 0 = ręka, 1+ = indeks w itemIcons

    void Start()
    {
        UpdateDisplay();
    }

    void Update()
    {
        int totalSlots = itemIcons.Count + 1; // +1 bo ręka

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f)
        {
            selectedIndex = (selectedIndex - 1 + totalSlots) % totalSlots;
            Debug.Log("Scroll up → selected index: " + selectedIndex);
            UpdateDisplay();
        }
        else if (scroll < 0f)
        {
            selectedIndex = (selectedIndex + 1) % totalSlots;
            Debug.Log("Scroll down → selected index: " + selectedIndex);
            UpdateDisplay();
        }
    }

    void UpdateDisplay()
    {
        int total = itemIcons.Count;

        if (total == 0 && selectedIndex == 0)
        {
            // Tylko ręka
            displaySlots[0].sprite = emptySlotSprite;
            displaySlots[1].sprite = handIcon;
            displaySlots[2].sprite = emptySlotSprite;

            displaySlots[0].color = new Color(1f, 1f, 1f, 0.2f);
            displaySlots[1].color = new Color(1f, 1f, 1f, 1f);
            displaySlots[2].color = new Color(1f, 1f, 1f, 0.2f);
            return;
        }

        int left = (selectedIndex - 1 + total + 1) % (total + 1);   // +1 bo ręka
        int center = selectedIndex;
        int right = (selectedIndex + 1) % (total + 1);

        Debug.Log($"Displaying - Left: {left}, Center: {center}, Right: {right}");

        DisplaySlot(0, left);
        DisplaySlot(1, center);
        DisplaySlot(2, right);
    }

    void DisplaySlot(int slotIndex, int itemIndex)
    {
        if (itemIndex == 0)
        {
            displaySlots[slotIndex].sprite = handIcon;
            displaySlots[slotIndex].color = slotIndex == 1 ? Color.white : new Color(1f, 1f, 1f, 0.2f);
        }
        else
        {
            int actualIndex = itemIndex - 1;
            if (actualIndex >= 0 && actualIndex < itemIcons.Count)
            {
                displaySlots[slotIndex].sprite = itemIcons[actualIndex].itemIcon;
                displaySlots[slotIndex].color = slotIndex == 1 ? Color.white : new Color(1f, 1f, 1f, 0.4f);
            }
            else
            {
                displaySlots[slotIndex].sprite = emptySlotSprite;
                displaySlots[slotIndex].color = new Color(1f, 1f, 1f, 0.2f);
            }
        }
    }

    public void AddItem(Item newItem)
    {
        if (newItem != null && newItem.itemIcon != null)
        {
            if (itemIcons.Contains(newItem))
            {
                Debug.Log("Item already in hotbar.");
                return;
            }

            itemIcons.Add(newItem);
            UpdateDisplay();

            Debug.Log("Item added: " + newItem.name);
        }
        else
        {
            Debug.LogWarning("Dodawany item jest pusty lub nie ma przypisanego itemIcon.");
        }
    }
}
