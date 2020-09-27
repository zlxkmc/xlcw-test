using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// 目标
    /// </summary>
    [SerializeField]
    private Transform _target;

    /// <summary>
    /// 位置偏差
    /// </summary>
    [SerializeField]
    private Vector3 _offset;

    /// <summary>
    /// 角度
    /// </summary>
    [SerializeField]
    private Vector3 _rotate;

    /// <summary>
    /// 跟随速度
    /// </summary>
    [SerializeField]
    private float _followSpeed = 12;

    /// <summary>
    /// 最小距离
    /// </summary>
    [SerializeField]
    private float _minDistance = 5;

    /// <summary>
    /// 最大距离
    /// </summary>
    [SerializeField]
    private float _maxDistance = 15;

    /// <summary>
    /// 当前距离
    /// </summary>
    [SerializeField]
    private float _distance = 10;

    /// <summary>
    /// 滚轮速度
    /// </summary>
    [SerializeField]
    private float _scrollWheelSpeed = 50;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScrollWheelControl();


        if(_distance < _minDistance)
        {
            _distance = _minDistance;
        }
        else if(_distance > _maxDistance)
        {
            _distance = _maxDistance;
        }
    }

    private void LateUpdate()
    {
        if(_target != null)
        {
            Vector3 targetPos = _target.position + _offset.normalized * _distance;
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * _followSpeed);
            transform.rotation = Quaternion.Euler(_rotate);
        }
    }

    private void ScrollWheelControl()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            _distance -= Time.deltaTime * _scrollWheelSpeed;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            _distance += Time.deltaTime * _scrollWheelSpeed;
        }
    }
}
