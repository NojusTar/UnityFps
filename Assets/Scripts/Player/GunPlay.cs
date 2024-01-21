using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPlay : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;

    [SerializeField]
    private GunInventory gunInventory;

    [SerializeField]
    private GameObject bulletHole;

    [SerializeField]
    private LayerMask enemyLayerMask;

    public float fireRate;
    public bool hasFired;

    private bool fire;
    private bool autoFire;
    private bool fireHitSomething;
    private RaycastHit bulletTrace;

    #region -Shooting Visuals-

    private ShootingVisuals shootingVisuals;
    public event EventHandler OnFire;
    public event EventHandler<OnFireHitEventArgs> OnFireHit;
    public class OnFireHitEventArgs : EventArgs
    {
        public RaycastHit bulletTrace;
    }

    #endregion
    void Start()
    {
        hasFired = false;
        if (gunInventory.currentGun == null)
        {
            fireRate = 0;
        }

        shootingVisuals = GetComponent<ShootingVisuals>();
    }

    void Update()
    {
        GunPlayInput();
        Fire();
        RateOfFire();
    }

    private void GunPlayInput()
    {
        fire = Input.GetMouseButtonDown(0);
        autoFire = Input.GetMouseButton(0);

    }    

    private void Fire()
    {
        // jei nepasirinktas ginklas return is funckijos
        if (gunInventory.currentGun == null) { return; }
        //semi auto fire
        if (gunInventory.currentGun.automatic == false)
        {
            if (fire && !hasFired)
            {
                StartFireDelay();

                FireRays();

                OnFire?.Invoke(this, EventArgs.Empty);

            }
        }
        //automatic fire
        if (gunInventory.currentGun.automatic == true)
        {
            if (autoFire && !hasFired)
            {
                StartFireDelay();

                FireRays();

                OnFire?.Invoke(this, EventArgs.Empty);

            }
        }

    }

    private void FireRays()
    {
        //multi bullet shot
        if (gunInventory.currentGun.bulletAmount > 1)
        {
            for (int i = 0; i < gunInventory.currentGun.bulletAmount; i++)
            {
                fireHitSomething = Physics.Raycast(playerCamera.transform.position, GetBulletSpread(), out bulletTrace, 200f);

                Debug.DrawLine(playerCamera.transform.position, bulletTrace.point, Color.red, 2f);

                IfHitSomething();
            }
        }
        //single bullet shot
        else
        {
            fireHitSomething = Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out bulletTrace, 200f);

            Debug.DrawLine(playerCamera.transform.position, bulletTrace.point, Color.red, 2f);

            IfHitSomething();
        }
        
    }

    private void IfHitSomething()
    {
        if (fireHitSomething) // pataike i kazka
        {
            OnFireHit?.Invoke(this, new OnFireHitEventArgs {  bulletTrace = bulletTrace });
            Debug.Log("pataike i kazka: " + bulletTrace.transform.name);

            if (bulletTrace.transform.gameObject.GetComponent<BaseEnemy>()) // pataike i priesa
            {
                bulletTrace.transform.GetComponent<BaseEnemy>().TakeDamage(gunInventory.currentGun.damage);
            }
        }
    }

    private void RateOfFire()
    {
        if (hasFired) 
        {
            fireRate -= Time.deltaTime;
            if (fireRate <= 0)
            {
                hasFired = false;
            }
        }
    }

    private void StartFireDelay()
    {
        hasFired = true;
        fireRate = gunInventory.currentGun.fireRate;

    }

    private Vector3 GetBulletSpread() 
    {
        Vector3 bulletSpread = playerCamera.transform.position + playerCamera.transform.forward * 200f;
        bulletSpread = new Vector3(bulletSpread.x + UnityEngine.Random.Range(-gunInventory.currentGun.bulletSpread, gunInventory.currentGun.bulletSpread),
                                  bulletSpread.y + UnityEngine.Random.Range(-gunInventory.currentGun.bulletSpread, gunInventory.currentGun.bulletSpread),
                                  bulletSpread.z + UnityEngine.Random.Range(-gunInventory.currentGun.bulletSpread, gunInventory.currentGun.bulletSpread));

        Vector3 shootingDir = bulletSpread - playerCamera.transform.position;
        return shootingDir.normalized;
    }

    
}
