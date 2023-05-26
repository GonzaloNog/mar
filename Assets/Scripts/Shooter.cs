using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour

{
    [SerializeField] GameObject shootDecal;
    [SerializeField] float shotsPerSecond = 1f;


    bool isShooting;
    float lastShootTime;

    [Header("Debug")]
    public bool debugShoot;
    public bool debugStartShooting;
    public bool debugStopShooting;

    private void OnValidate()
    {
        if (debugShoot)
        {
            debugShoot = false;
            PerformShot();
        }

        if (debugStartShooting)
        {
            debugStartShooting = false;
            isShooting = true;
        }

        if (debugStopShooting)
        {
            debugStopShooting = false;
            isShooting = false;
        }
    }
    private void Update()
    {
        if (isShooting && (Time.time - lastShootTime) > (1f / shotsPerSecond))
        {
            PerformShot();
        }

    }

    public void StartShooting()
    {
        isShooting = true;
    }

    public void StopShooting()
    {
        isShooting = false;
    }
    public void PerformShot()
    {
        lastShootTime = Time.time;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (shootDecal) { Instantiate(shootDecal, hit.point, Quaternion.identity); }
            //hit.collider.GetComponent<HurtBox>()?.NotifyHit(null);
        }
    }
}
