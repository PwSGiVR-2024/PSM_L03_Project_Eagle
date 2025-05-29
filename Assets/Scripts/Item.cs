using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string id;
    public int maxUses;
    public int currentUses;
    public string itemName;
    public Sprite itemIcon;  // Ikona 2D przedmiotu (do wyœwietlenia w hotbarze)
    [SerializeField] Hotbar hotbar;

    private void Start()
    {
        hotbar = GameObject.FindGameObjectWithTag("HotBar").GetComponent<Hotbar>();
    }
    protected virtual void Awake()
    {
        if (maxUses > 0)
        {
            currentUses = maxUses;
        }
    }

    public virtual void UseItem()
    {
        if (currentUses > 0)
        {
            currentUses--;
            OnUse();
        }
        else
        {
            Debug.Log("Brak u¿yæ przedmiotu!");
        }
    }
    public void Pickup()
    {
        // Dodaj siebie do hotbara i wy³¹cz siebie z gry
        hotbar.AddItem(this);
        gameObject.SetActive(false); // lub Destroy(gameObject);
    }
    protected abstract void OnUse();
}
