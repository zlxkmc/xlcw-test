using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<PlayerController>();
            }

            return _instance;
        }
    }

    public Player Player
    {
        get => _player;
    }

    private static PlayerController _instance;
    private Player _player;

    /// <summary>
    /// 攻击目标
    /// </summary>
    private Unit _attackTarget;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Utils.IsMouseOverUI() == false) // 按下右键
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("Unit")))
            {
                Unit target = hit.collider.GetComponent<Unit>();
                if (target != null && target.Team != _player.Team) // 设置攻击目标
                {
                    _attackTarget = target;
                }
            }
            else if (Physics.Raycast(ray, out hit, int.MaxValue, 1 << LayerMask.NameToLayer("Terrain")))
            {
                _attackTarget = null;
                _player.MoveTo(hit.point);
            }
        }

        if(_attackTarget != null) // 有攻击目标
        {
            if (_player.IsInAttackRange(_attackTarget)) // 在攻击范围内
            {
                _player.NormalAttack(_attackTarget);
            }
            else // 接近目标
            {
                _player.MoveTo(_attackTarget.transform.position);
            }
        }
    }
}
