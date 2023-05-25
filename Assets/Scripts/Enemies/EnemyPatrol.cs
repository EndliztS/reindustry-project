using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Stats")]
    public float speed;
    public float idleTime;
    public float attackRange;
    [SerializeField] Vector2 pointToAttack;

    [Header("References")]
    [SerializeField] Transform center;
    [SerializeField] Transform checkPoint;
    [SerializeField] Transform sprite;
    [SerializeField] Animator anims;
    [SerializeField] EnemyMelee melee;

    [Header("FXs")]
    [SerializeField] GameObject spawnFx;
    [SerializeField] GameObject deathFx;

    [Header("SFX")]
    [SerializeField] Sound spawnSfx;
    [SerializeField] Sound[] deathSfx;

    Health hp;
    public string runState;
    int direction;

    bool isIdle;

    Rigidbody2D rb;
    Transform player;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").transform;
        hp = GetComponent<Health>();

        AudioManager.Instance.PlaySFX(spawnSfx);
        Instantiate(spawnFx,transform.position,Quaternion.identity);

        int[] dir = {-1, 1};
        direction = dir[Random.Range(0,dir.Length)];
        Flip();
    }

    private void Update() {

        if (hp.isDead) {
            AudioManager.Instance.PlaySFX(deathSfx[Random.Range(0,deathSfx.Length)]);
            Instantiate(deathFx,center.position,Quaternion.identity);
            FindObjectOfType<Score>().score++;
            Destroy(gameObject);
        }

        if (melee.isAttacking) return;

        if (player) {
            float dist = Vector2.Distance(player.transform.position,center.transform.position);
            if (dist <= attackRange) {
                if (((transform.position.x<player.position.x) && direction<0) || ((transform.position.x>player.position.x) && direction>0)) Flip();
                melee.Attack();
            }
        }
    }

    private void FixedUpdate() {
        if (melee.isAttacking) {
            rb.velocity = Vector2.zero; 
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(checkPoint.position,Vector2.down,1f,LayerMask.GetMask("Ground"));
        if (hit) {
            rb.velocity = new Vector2(speed*direction,rb.velocity.y);
            anims.SetBool(runState,true);
            if (isIdle) isIdle = false;
        } else {
            Stop();
        }
    }

    private void Stop() {
        rb.velocity = Vector2.zero;
        anims.SetBool(runState,false);
        if (!isIdle) {
            Invoke("Flip",idleTime);
            isIdle=true;
        }
    }

    private void Flip() {
        direction = -direction;
        sprite.transform.localScale = new Vector3(-direction,1,1);
    }
}
