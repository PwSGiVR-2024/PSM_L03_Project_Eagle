using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int ID;
    public int MaxUses;
    public int CurrentUses;
    public Sprite itemIcon;  // Ikona 2D przedmiotu (do wyœwietlenia w hotbarze)

    protected virtual void Awake()
    {
        if (MaxUses > 0)
        {
            CurrentUses = MaxUses;
        }
    }

    public virtual void UseItem()
    {
        if (CurrentUses > 0)
        {
            CurrentUses--;
            OnUse();
        }
        else
        {
            Debug.Log("Brak u¿yæ przedmiotu!");
        }
    }
    public void Pickup(Hotbar hotbar)
    {
        // Dodaj siebie do hotbara i wy³¹cz siebie z gry
        hotbar.AddItem(this);
        gameObject.SetActive(false); // lub Destroy(gameObject);
    }
    protected abstract void OnUse();
}
