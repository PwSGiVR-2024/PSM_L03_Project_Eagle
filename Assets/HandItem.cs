public class HandItem : Item
{
    protected override void Awake()
    {
        maxUses = 5;
        base.Awake();
        id = "main_hand";
        itemName = "Pusta rêka";
    }
    protected override void OnUse()
    {
        // u¿ycie itemu
    }
}
