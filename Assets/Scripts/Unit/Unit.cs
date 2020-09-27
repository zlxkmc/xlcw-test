
using Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Unit : MonoBehaviour
{
    [Serializable]
    public class OnTakeDamage : UnityEvent<DamageInfo>
    {

    }

    [Serializable]
    public class OnDie : UnityEvent<DamageInfo>
    {

    }

    /// <summary>
    /// 生命上限
    /// </summary>
    public float MaxHp { get => _maxHp; }
    /// <summary>
    /// 当前生命值
    /// </summary>
    public float Hp
    {
        get
        {
            return _hp;
        }
        set
        {
            if (_hp != value)
            {
                float oldValue = _hp;
                _hp = value;
                if (_hp < 0)
                {
                    _hp = 0;
                }
                else if (_hp > MaxHp)
                {
                    _hp = MaxHp;
                }
            }
        }
    }
    /// <summary>
    /// 攻击力
    /// </summary>
    public float Attack { get => _attack; set => _attack = value; }
    /// <summary>
    /// 攻击速度
    /// </summary>
    public float AttackSpeed { get => _attackSpeed; set => _attackSpeed = value; }
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float AttackRange { get => _attackRange; set => _attackRange = value; }
    /// <summary>
    /// 防御力
    /// </summary>
    public float Def { get => _def; set => _def = value; }
    /// <summary>
    /// 移动速度
    /// </summary>
    public float MoveSpeed { get => _moveSpeed; set => _moveSpeed = value; }
    /// <summary>
    /// 子弹飞行速度
    /// </summary>
    public float BulletFlySpeed { get => _bulletFlySpeed; set => _bulletFlySpeed = value; }
    /// <summary>
    /// 子弹
    /// </summary>
    public Bullet Bullet { get => _bullet; set => _bullet = value; }
    /// <summary>
    /// 队伍
    /// </summary>
    public Team Team { get => _team; set => _team = value; }
    /// <summary>
    /// 攻击方式
    /// </summary>
    public AttackPattern AttackPattern { get => _attackPattern; set => _attackPattern = value; }

    /// <summary>
    /// 当受到伤害时
    /// </summary>
    public OnTakeDamage onTakeDamage { get => _onTakeDamage; }

    /// <summary>
    /// 当死亡时
    /// </summary>
    public OnDie onDie { get => _onDie; }
    /// <summary>
    /// 血条位置
    /// </summary>
    public Transform HpBarPoint { get => _hpBarPoint; }

    public bool IsPlayer { get; protected set;}


    [SerializeField]
    private float _maxHp = 500;
    [SerializeField]
    private float _hp = 500;
    [SerializeField]
    private float _attack = 100;
    [SerializeField]
    private float _attackSpeed = 1.5f;
    [SerializeField]
    private float _attackRange = 8;
    [SerializeField]
    private float _def = 30;
    [SerializeField]
    private float _moveSpeed = 3;
    [SerializeField]
    private float _bulletFlySpeed = 10;
    [SerializeField]
    private Bullet _bullet;
    [SerializeField]
    private Team _team;
    [SerializeField]
    private AttackPattern _attackPattern;
    [SerializeField]
    private OnTakeDamage _onTakeDamage = new OnTakeDamage();
    [SerializeField]
    private OnDie _onDie = new OnDie();
    [SerializeField]
    private Transform _hpBarPoint;
    [SerializeField]
    private HpBarUI _hpBarUIPb;

    private float _attackCd;
    private NavMeshAgent _navMeshAgent;
    private BulletPool _bulletPool;
    private HpBarUI _hpBarUI;

    protected virtual void Start()
    {
        _bulletPool = new BulletPool(new BulletFactory(_bullet));
        if(_hpBarUIPb != null)
        {
            _hpBarUI = Instantiate(_hpBarUIPb.gameObject,Utils.Canvas).GetComponent<HpBarUI>();
            _hpBarUI.Init(this);
        }
    }

    protected virtual void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        _navMeshAgent.speed = MoveSpeed;
        _attackCd -= Time.deltaTime * _attackSpeed / 1;
        if(_attackCd < 0)
        {
            _attackCd = 0;
        }
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="damage"></param>
    public void TakeDamage(Unit attacker, float damage)
    {
        // 防御减伤后的伤害
        float actualDamage = damage / (1 + Def / 100);
        DamageInfo damageInfo = new DamageInfo()
        {
            expectDamage = damage,
            actualDamage = actualDamage,
            attacker = attacker,
            beAttacker = this,
            isPlayer = attacker.IsPlayer
        };

        Debug.Log(damageInfo.beAttacker.gameObject.name + " 受到 " + " " + damageInfo.actualDamage + " 点伤害");

        Hp -= actualDamage;        
        onTakeDamage.Invoke(damageInfo);

        if(Hp <= 0)
        {
            Die(damageInfo);
            onDie.Invoke(damageInfo);
        }
    }


    /// <summary>
    /// 攻击目标
    /// </summary>
    /// <param name="target"></param>
    public void NormalAttack(Unit attackTarget)
    {
        if(_attackCd <= 0 && IsInAttackRange(attackTarget))
        {
            if (AttackPattern == AttackPattern.Melee) // 近战
            {
                attackTarget.TakeDamage(this, Attack);
            }
            else if (AttackPattern == AttackPattern.Range) // 远程
            {
                Bullet bullet = _bulletPool.Allocate();
                bullet.Init(attackTarget, BulletFlySpeed, transform.position, transform.rotation);
                bullet.onAttackSuccess.AddListener(() => {
                    attackTarget.TakeDamage(this, Attack);
                    _bulletPool.Recycle(bullet);
                });
                bullet.onTargetMiss.AddListener(() =>
                {
                    _bulletPool.Recycle(bullet);
                });
                bullet.gameObject.SetActive(true);
            }

            _attackCd = 1;

            StopMove();
        }
    }

    /// <summary>
    /// 移动到某位置
    /// </summary>
    /// <param name="pos"></param>
    public void MoveTo(Vector3 pos)
    {
        _navMeshAgent.SetDestination(pos);
    }

    /// <summary>
    /// 停止移动
    /// </summary>
    public void StopMove()
    {
        _navMeshAgent.ResetPath();
    }
    
    /// <summary>
    /// 是否在攻击范围内
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public bool IsInAttackRange(Unit target)
    {
        Vector3 offset = target.transform.position - transform.position;
        float sqrDis = offset.x * offset.x + offset.z * offset.z;

        return sqrDis < AttackRange * AttackRange;
    }

    /// <summary>
    /// 死亡
    /// </summary>
    protected virtual void Die(DamageInfo info)
    {
        Destroy(gameObject);
    }
}
