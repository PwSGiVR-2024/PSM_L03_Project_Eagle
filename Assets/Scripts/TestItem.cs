public class TestItem : Item
{
    protected override void Awake()
    {
        maxUses = 5;
        base.Awake();
        id = "test_item";
        itemName = "Testowy przedmiot";
    }

    protected override void OnUse()
    {
        // u�ycie itemu
    }
}