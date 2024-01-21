using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    [Header("Gun")]
    public int id;
    public string gunName;
    public GameObject gunPrefab;

    [Header("Gun settings")]
    public float damage;
    public float fireRate;
    public bool automatic;
    public int bulletAmount;
    public float bulletSpread;

    [Header("recoil")]
    [Header("Kick back")]
    public float returnSpeed;
    public float kickX;
    public float kickY;
    public float kickZ;

    [Header("rotation")]
    public float snapiness;
    public float recoilX;
    public float recoilY;
    public float recoilZ;
    

}
