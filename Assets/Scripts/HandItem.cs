public class HandItem : Item
{
    protected override void Awake()
    {
        maxUses = 5;
        base.Awake();
        id = "main_hand";
        itemName = "Pusta r�ka";
    }
    protected override void OnUse()
    {
        // u�ycie itemu
    }
}
