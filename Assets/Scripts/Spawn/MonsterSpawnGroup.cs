using System;
using System.Collections.Generic;
using UnityEngine;


public class MonsterSpawnGroup : MonoBehaviour
{
    [SerializeField]
    private List<MonsterSpawn> _spawns = new List<MonsterSpawn>();

    /// <summary>
    /// 复活时间
    /// </summary>
    [SerializeField]
    private float _reliveTime;

    /// <summary>
    /// 复活cd
    /// </summary>
    private float _reliveCd;

    /// <summary>
    /// 复活中
    /// </summary>
    private bool _reliving;

    private void Start()
    {
        ReliveAll();
    }

    private void Update()
    {
        if (_reliving)
        {
            _reliveCd -= Time.deltaTime;
            if(_reliveCd < 0)
            {
                _reliveCd = 0;
                ReliveAll();
                _reliving = false;
            }
        }
        else
        {
            if (IsAllDeath())
            {
                _reliveCd = _reliveTime;
                _reliving = true;
            }
        }
        
    }

    public void ReliveAll()
    {
        _spawns.ForEach(spawn =>
        {
            spawn.Relive();
        });
    }

    /// <summary>
    /// 是否全部死亡
    /// </summary>
    /// <returns></returns>
    public bool IsAllDeath()
    {
        foreach (var spawn in _spawns)
        {
            if(spawn.Unit != null)
            {
                return false;
            }
        }

        return true;
    }
}

