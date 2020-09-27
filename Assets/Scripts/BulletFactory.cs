using Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class BulletFactory : IFactory<Bullet>
{
    private Bullet _bulletPb;
    public BulletFactory(Bullet bulletPb)
    {
        _bulletPb = bulletPb;
    }

    public Bullet Create()
    {
        GameObject go = GameObject.Instantiate(_bulletPb.gameObject);
        go.SetActive(false);

        return go.GetComponent<Bullet>();
    }
}

