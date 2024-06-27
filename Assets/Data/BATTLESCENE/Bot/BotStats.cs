public class BotStats : EntityStats
{
    protected override void Start()
    {
        base.ResetValues();
        currentHP = maxHP;
        
        OpText.Instance.HP.SetText($"{currentHP}/{maxHP}");
        OpText.Instance.VHP.SetText($"{vHP}");
        OpText.Instance.MP.SetText($"{mana}%");
        OpText.Instance.ShieldCount.SetText($"{shieldCount}");
        OpText.Instance.ShieldStack.SetText($"{shieldStack}");
    }
}