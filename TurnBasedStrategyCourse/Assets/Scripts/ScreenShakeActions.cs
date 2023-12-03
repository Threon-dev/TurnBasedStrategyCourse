using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeActions : MonoBehaviour
{
    private void Start()
    {
        ShootAction.OnAnyShoot += ShootActionOnOnAnyShoot;
        GrenadeProjectile.OnAnyGrenadeExploded += GrenadeProjectileOnOnAnyGrenadeExploded;
    }

    private void GrenadeProjectileOnOnAnyGrenadeExploded()
    {
        ScreenShake.Instance.Shake(5);
    }

    private void ShootActionOnOnAnyShoot()
    {
        ScreenShake.Instance.Shake();
    }
}
