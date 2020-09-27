
using UnityEngine;

public class Monster : Unit
{
    /// <summary>
    /// 最小赏金
    /// </summary>
    public int MinReward { get => _minReward; set => _minReward = value; }
    /// <summary>
    /// 最大赏金
    /// </summary>
    public int MaxReward { get => _maxReward; set => _maxReward = value; }

    /// <summary>
    /// 出生位置
    /// </summary>
    public Vector3 SpawnPos { get; set; }

    [SerializeField]
    private int _minReward;

    [SerializeField]
    private int _maxReward;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        onDie.AddListener((info) =>
        {
            if(info.attacker != null)
            {
                int reward = Random.Range(MinReward, MaxReward);
                if (info.isPlayer)
                {
                    Data.Instance.Money += reward;
                }
            }
        });
    }
}
