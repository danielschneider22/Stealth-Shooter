using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun
{
    public float accuracy;
    public Color color;
    public int clipSize;
    public int ammo;
    public string gunName;
    public float bulletSpeed;
    public float reloadTime;
    public float cooldownTime;
    public int damageLow;
    public int damageHigh;
    public List<string> specialStats;
    public Rarity rarity;
    public enum GunType
    {
        Pistol,
        Rifle
    }
    public GunType gunType;


    public Gun(float accuracy, Color color, int clipSize, int ammo, string gunName, GunType gunType, float bulletSpeed, float reloadTime, float cooldownTime, int damageLow, int damageHigh, Rarity rarity, List<string> specialStats)
    {
        this.accuracy = accuracy;
        this.color = color;
        this.clipSize = clipSize;
        this.ammo = ammo;
        this.gunName = gunName;
        this.gunType = gunType;
        this.bulletSpeed = bulletSpeed;
        this.reloadTime = reloadTime;
        this.cooldownTime = cooldownTime;
        this.damageLow = damageLow;
        this.damageHigh = damageHigh;
        this.rarity = rarity;
        this.specialStats = specialStats;
    }
}
