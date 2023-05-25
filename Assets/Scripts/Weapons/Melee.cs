using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Melee : MonoBehaviour
{
    [SerializeField] MeleeData data;
    [SerializeField] Transform hitArea;
    public float radius;
    Animator anim;

    bool canAttack;
    int type = 0;

    void Start() {
        anim = GetComponent<Animator>();
        canAttack = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canAttack) Attack();
    }

    void Attack() {
        canAttack = false;
        if (type >= data.anims.Length) type = 0;
        anim.Play(data.anims[type],0,0);
        type++;

        Invoke("ResetAttack",data.attackRate);
    }

    void ResetAttack() {
        canAttack = true;
    }

    void Hit() {
        AudioManager.Instance.PlaySFX(data.attackSfx[Random.Range(0,data.attackSfx.Length)]);

        Collider2D[] entities = Physics2D.OverlapCircleAll(hitArea.position,radius);
        foreach(var i in entities) {
            Health hp = i.GetComponent<Health>();
            if (hp && !i.CompareTag("Player")) {                
                hp.TakeDamage(data.damage);      
                AudioManager.Instance.PlaySFX(data.hitSfx[Random.Range(0,data.hitSfx.Length)]);          
            }
        }
    }
}
