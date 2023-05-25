using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class EnemyMelee : MonoBehaviour
{
    [HideInInspector]
    public bool isAttacking = false;
    [SerializeField] MeleeData data;
    [SerializeField] Transform hitArea;
    public float radius;
    Animator anim;

    int type = 0;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Attack() {
        isAttacking = true;
        if (type >= data.anims.Length) type = 0;
        anim.SetTrigger(data.anims[type]);
        type++;

        Invoke("ResetAttack",data.attackRate);
    }

    void ResetAttack() {
        isAttacking = false;
    }

    void Hit() {
        AudioManager.Instance.PlaySFX(data.attackSfx[Random.Range(0,data.attackSfx.Length)]);
        Collider2D[] entities = Physics2D.OverlapCircleAll(hitArea.position,radius);
        foreach(var i in entities) {
            Health hp = i.GetComponent<Health>();
            if (hp && i.CompareTag("Player")) {                
                hp.TakeDamage(data.damage);       
                AudioManager.Instance.PlaySFX(data.hitSfx[Random.Range(0,data.hitSfx.Length)]);            
            }
        }
    }
}
