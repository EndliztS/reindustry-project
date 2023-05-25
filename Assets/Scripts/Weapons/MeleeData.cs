using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunData",menuName = "Weapon/Melee")]
public class MeleeData : ScriptableObject
{
    public new string name;
    public float attackRate;
    public int damage;
    public string[] anims;
    [Header("SFX")]
    public Sound[] attackSfx;
    public Sound[] hitSfx; 
    [Header("Camera Shake")]
    public float magnitude;
    public float roughness;
    public float fadeIn;
    public float fadeOut;
}
