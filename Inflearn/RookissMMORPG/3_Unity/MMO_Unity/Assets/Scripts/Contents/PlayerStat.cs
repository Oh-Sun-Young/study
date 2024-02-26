using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exe;
    [SerializeField]
    protected int _gold;

    public int Exe { get { return _exe; } set { _exe = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;

        _attack = 10;
        _defense = 5;

        _moveSpeed = 5.0f;

        _exe = 0;
        _gold = 0;
    }
}
