using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject hitFx;
    [SerializeField] Sound hitSfx;
    [HideInInspector]
    public int damage;

    private void Start() {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(),GameObject.FindWithTag("Player").GetComponent<Collider2D>());
        Destroy(gameObject,5);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        var hp = col.gameObject.GetComponent<Health>();
        if (hp && !col.gameObject.CompareTag("Player")) {
            AudioManager.Instance.PlaySFX(hitSfx);
            hp.TakeDamage(damage);
        }
        Instantiate(hitFx,transform.position,transform.rotation);
        Destroy(gameObject);
    }
}
