
public struct DamageInfo
{
    /// <summary>
    /// 攻击者
    /// </summary>
    public Unit attacker;

    /// <summary>
    /// 被攻击者
    /// </summary>
    public Unit beAttacker;

    /// <summary>
    /// 期望伤害
    /// </summary>
    public float expectDamage;

    /// <summary>
    /// 实际伤害
    /// </summary>
    public float actualDamage;

    public bool isPlayer;
}
