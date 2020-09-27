using AI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class MonsterSpawn : MonoBehaviour
{
    /// <summary>
    /// 对应单位的预制体
    /// </summary>
    public Monster UnitPb { get => _unitPb; set => _unitPb = value; }

    /// <summary>
    /// 单位容器
    /// </summary>
    public Transform UnitContent { get => _unitContent; }

    /// <summary>
    /// 对应的单位
    /// </summary>
    public Monster Unit { get; private set; }


    [SerializeField]
    private Monster _unitPb;

    [SerializeField]
    private Transform _unitContent;


    public void Relive()
    {
        if(Unit == null)
        {
            Unit = Instantiate<GameObject>(UnitPb.gameObject, transform.position, transform.rotation, UnitContent).GetComponent<Monster>();
            Unit.SpawnPos = transform.position;

        }
    }
}

