using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;

    public float speed = 5f;
    public float rotateSpeed =200f;
    public int damage = 30;
    public float radius = 2.5f;

    [SerializeField] GameObject explodeFx;
    [SerializeField] Sound[] explodeSfx;

    void Start() {
        player = GameObject.FindWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        if (player) {
            Vector2 direction = (Vector2)player.position - rb.position;
            direction.Normalize();
            float rotateAmount = Vector3.Cross(direction,transform.up).z;
            rb.angularVelocity = -rotateAmount * rotateSpeed;
            rb.velocity = transform.up * speed;
        }
        else rb.gravityScale = 5;
    }

    void OnCollisionEnter2D(Collision2D col) {
        Instantiate(explodeFx,transform.position,Quaternion.identity);
        AudioManager.Instance.PlaySFX(explodeSfx[Random.Range(0,explodeSfx.Length)]);

        Collider2D[] entities = Physics2D.OverlapCircleAll(transform.position,radius);
        foreach (var i in entities) {
            var hp = i.GetComponent<Health>();
            if (hp) hp.TakeDamage(damage);
        }

        Destroy(gameObject);
    }

}
