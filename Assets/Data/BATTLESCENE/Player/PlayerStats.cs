public class PlayerStats : EntityStats
{
    protected override void Start()
    {
        base.ResetValues();
        currentHP = maxHP;

        PlayerText.Instance.HP.SetText($"{currentHP}/{maxHP}");
        PlayerText.Instance.VHP.SetText($"{vHP}");
        PlayerText.Instance.MP.SetText($"{mana}%");
        PlayerText.Instance.ShieldCount.SetText($"{shieldCount}");
        PlayerText.Instance.ShieldStack.SetText($"{shieldStack}");
    }
}