using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UZI : WeaponsBase
{
    [SerializeField] Shooter shooter;

    public override void StartShooting()
    {
        shooter.StartShooting();
    }

    public override void stopShooting()
    {
        shooter.StopShooting();
    }

    public override bool AllowsContinousShoot()
    {
        return true;
    }
}

