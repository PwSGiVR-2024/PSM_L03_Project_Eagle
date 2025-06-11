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
        if (!GameStateManager.Instance.IsNormal) return;
        int totalSlots = itemList.Count; 

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll < 0f)
        {
            selectedIndex = (selectedIndex - 1 + totalSlots) % totalSlots;
            UpdateDisplay();
        }
        else if (scroll > 0f)
        {
            selectedIndex = (selectedIndex + 1) % totalSlots;
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
            displaySlots[3].sprite = emptySlotSprite;
            displaySlots[4].sprite = emptySlotSprite;

            displaySlots[0].color = new Color(1f, 1f, 1f, 0.1f);
            displaySlots[1].color = new Color(1f, 1f, 1f, 0.5f);
            displaySlots[2].color = new Color(1f, 1f, 1f, 1f);
            displaySlots[3].color = new Color(1f, 1f, 1f, 0.5f);
            displaySlots[4].color = new Color(1f, 1f, 1f, 0.1f);
            return;
        }
        int leftSmall = (selectedIndex - 2 + total) % (total);
        int left = (selectedIndex - 1 + total) % (total);
        int center = selectedIndex;
        int right = (selectedIndex + 1) % (total );
        int rightSmall = (selectedIndex + 2) % (total);

        DisplaySlot(0, leftSmall);
        DisplaySlot(1, left);
        DisplaySlot(2, center);
        DisplaySlot(3, right);
        DisplaySlot(4, rightSmall);
        currentItemLabel.text = itemList[selectedIndex].itemName;
    }

    void DisplaySlot(int slotIndex, int itemIndex)
    {

            int actualIndex = itemIndex;
            if (actualIndex >= 0 && actualIndex < itemList.Count)
            {
                displaySlots[slotIndex].sprite = itemList[actualIndex].itemIcon;
                displaySlots[slotIndex].color = slotIndex == 2 ? Color.white : new Color(1f, 1f, 1f, 0.8f);
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
                return;
            }

            itemList.Add(newItem);
            UpdateDisplay();
        }
    }
}
