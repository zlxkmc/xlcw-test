
using UnityEngine;

public class Player : Unit
{
    protected override void Awake()
    {
        base.Awake();
        IsPlayer = true;
    }
}
