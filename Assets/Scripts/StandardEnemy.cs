using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardEnemy : EnemyBase
{
    [SerializeField] private GunBase currentWeapon;

    protected override void Update()
    {
        base.Update();

        if(isPlayerHit) currentWeapon.TryShoot();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        //currentWeapon = GetComponentInChildren<GunBase>();
    }
}
