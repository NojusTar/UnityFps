using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static GunPlay;

public class ShootingVisuals : MonoBehaviour
{
    [Header("Shooting visuals")]
    [SerializeField]
    private GameObject bulletHole;
    [SerializeField]
    private GameObject muzzle;
    [SerializeField]
    private GameObject impact;


    private Vector3 currentRotation;
    private Vector3 startPosition;
    private Vector3 currentPosition;
    private GunInventory gunInventory;
    private GunPlay gunPlay;

    private void Start()
    {
        startPosition = transform.localPosition;
        currentPosition = startPosition;
        gunPlay = GetComponent<GunPlay>();
        gunPlay.OnFire += GunPlay_OnFire;
        gunPlay.OnFireHit += GunPlay_OnFireHit;
        gunInventory = GetComponent<GunInventory>();
    }

    

    private void Update()
    {
        RecoilControl();
        
    }

    private void GunPlay_OnFire(object sender, EventArgs e)
    {
        Recoil();
        MuzzleFlash();

    }

    private void GunPlay_OnFireHit(object sender, OnFireHitEventArgs e)
    {
        if (e.bulletTrace.transform.GetComponent<BaseEnemy>()) // if hit enemy
        {
            showImpact(e.bulletTrace);
        }
        if (!e.bulletTrace.transform.GetComponent<BaseEnemy>()) // if not hit enemy
        {
            DrawDecalOnHit(e.bulletTrace);
            showImpact(e.bulletTrace);
        }
    }

    private void DrawDecalOnHit(RaycastHit ray)
    {
        GameObject bulletHoleInst = Instantiate(bulletHole, ray.point, Quaternion.LookRotation(ray.normal));
        bulletHoleInst.transform.position += -bulletHoleInst.transform.forward * 0.001f;
        Destroy(bulletHoleInst, 5f);
    }

    private void RecoilControl()
    {
        if (gunInventory.currentGun != null)
        {
            //recoil kickback (move)
            currentPosition = Vector3.Lerp(currentPosition, startPosition, gunInventory.currentGun.returnSpeed * Time.deltaTime);
            transform.localPosition = currentPosition;
            //recoil rotation
            currentRotation = Vector3.Slerp(currentRotation, Vector3.zero, gunInventory.currentGun.snapiness * Time.deltaTime);
            transform.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    private void Recoil()
    {
        currentRotation += new Vector3(gunInventory.currentGun.recoilX, gunInventory.currentGun.recoilY, gunInventory.currentGun.recoilZ);
        currentPosition += new Vector3(gunInventory.currentGun.kickX, gunInventory.currentGun.kickY, gunInventory.currentGun.kickZ);
    }

    private void MuzzleFlash()
    {
        Transform muzzlePos = gunInventory.gunVisual.transform.Find("muzzle");
        if (muzzlePos != null)
        {
            GameObject flash = Instantiate(muzzle, muzzlePos.position, muzzlePos.rotation);
            flash.transform.parent = muzzlePos;
            Destroy(flash, 0.5f);

        }
    }

    private void showImpact(RaycastHit ray)
    {
        GameObject impactSpawn = Instantiate(impact, ray.point, Quaternion.identity);
        Destroy(impactSpawn, 0.5f);
    }
}
