using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData",menuName = "Weapon/Gun")]
public class GunData : ScriptableObject
{
    public new string name;
    [Header("Stats")]
    public float fireRate;
    public int damage;
    public int mag;
    public float bulletSpeed;
    public float knockback;
    public bool auto;
    [Header("Shotgun")]
    public bool shotgun;
    public int pellets;
    public float spread;
    [Header("SFXs")]
    public Sound[] shootSfx;
    [Header("Camera Shake")]
    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;
}
