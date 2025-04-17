using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public int ID;
    public int MaxUses;
    public int CurrentUses;
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
            Debug.Log("Brak u�y� przedmiotu!");
        }
    }
    protected abstract void OnUse();

}
