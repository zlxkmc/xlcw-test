using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class Bullet : MonoBehaviour
{
    [Serializable]
    public class OnAttackSuccess : UnityEvent
    {

    }

    [Serializable]
    public class OnTargetMiss : UnityEvent
    {

    }

    /// <summary>
    /// 当攻击到目标时
    /// </summary>
    public OnAttackSuccess onAttackSuccess { get => _onAttackSuccess; }
    /// <summary>
    /// 当目标丢失
    /// </summary>
    public OnTargetMiss onTargetMiss{ get => _onTargetMiss; }

    [SerializeField]
    private OnAttackSuccess _onAttackSuccess = new OnAttackSuccess();
    [SerializeField]
    private OnTargetMiss _onTargetMiss = new OnTargetMiss();

    private Unit _attackTarget;
    private float _flySpeed;
    private Vector3 _initPos;
    private Quaternion _initRot;

    /// <summary>
    /// 是否攻击过
    /// </summary>
    private bool _isAttack = false;

    public void Init(Unit attackTarget, float flySpeed, Vector3 initPos, Quaternion initRot)
    {
        _attackTarget = attackTarget;
        _flySpeed = flySpeed;
        _initPos = initPos;
        _initRot = initRot;

        transform.position = initPos;
        transform.rotation = initRot;

        _isAttack = false;
    }

    private void Update()
    {
        if(_isAttack == false)
        {
            if (_attackTarget != null)
            {
                // 差距
                Vector3 offset = _attackTarget.transform.position - transform.position;
                Vector3 dir = offset.normalized;
                // 这帧移动的向量
                Vector3 moveV = dir * Time.deltaTime * _flySpeed;

                if (offset.sqrMagnitude < moveV.sqrMagnitude)
                {
                    onAttackSuccess.Invoke();
                    _isAttack = true;
                }
                else
                {
                    transform.Translate(moveV, Space.World);
                }
            }
            else
            {
                onTargetMiss.Invoke();
            }
        }
    }

    public void OnRecycle()
    {
        gameObject.SetActive(false);

        _isAttack = false;
        _attackTarget = null;
        _flySpeed = 0;
        _initPos = Vector3.zero;
        _initRot = Quaternion.identity;
        transform.position = _initPos;

        _onAttackSuccess.RemoveAllListeners();
        _onTargetMiss.RemoveAllListeners();

    }
}

