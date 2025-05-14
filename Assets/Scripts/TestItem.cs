public class TestItem : Item
{
    protected override void Awake()
    {
        MaxUses = 5;
        base.Awake();
        ID = 1;
    }

    protected override void OnUse()
    {
        // u¿ycie itemu
    }
}