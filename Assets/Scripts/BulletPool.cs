using Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class BulletPool : IPool<Bullet>
{
    private static Stack<Bullet> _cacheStack = new Stack<Bullet>();

    private IFactory<Bullet> _factory;

    public int Count
    {
        get { return _cacheStack.Count; }
    }

    public BulletPool(IFactory<Bullet> factory)
    {
        _factory = factory;
    }

    public void Recycle(Bullet obj)
    {
        obj.OnRecycle();
        _cacheStack.Push(obj);
    }

    public Bullet Allocate()
    {
        return Count > 0 ? _cacheStack.Pop() : _factory.Create();
    }
}

