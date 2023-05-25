using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{

    [Header("Controller")]
    [SerializeField] KeyCode jumpKey = KeyCode.W;
    [SerializeField] KeyCode altJumpKey = KeyCode.Space;

    [Header("Stats")]
    public float speed;
    public float jumpHeight;
    public int extraJumpsValue;
    private float moveInput;
    public bool isInvincible = false;

    [Header("Ground Check")]
    private bool isGrounded, isAir;
    [SerializeField] Transform groundCheck;
    [SerializeField] float checkRadius = .2f;
    [SerializeField] LayerMask groundLayer;

    [Header("References")]
    [SerializeField] Transform sprite;
    [SerializeField] Animator animator;

    [Header("Effects")]
    [SerializeField] GameObject deathFx;
    [SerializeField] GameObject hitEffectPP;

    [Header("SFX")]
    [SerializeField] Sound jumpSfx;
    [SerializeField] Sound hurtSfx;
    [SerializeField] Sound landSfx;
    [SerializeField] Sound[] deathSfx;

    private int extraJumps;
    
    private bool facingRight = true;

    private Rigidbody2D rb;

    Health hp;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        hp = GetComponent<Health>();
    }

    void Update() {
        // Ground check
        GroundHandler();

        // Handle Jumping
        JumpHandler();

        // Flip player
        FlipHandler();

        // Animation;
        AnimationHandler();

        // HEALTH
        if (hp.isDead) {  
            AudioManager.Instance.PlaySFX(deathSfx[Random.Range(0,deathSfx.Length)]);          
            FindObjectOfType<HitStop>().Stop(.1f,2f);
            Instantiate(deathFx,transform.position,Quaternion.identity);
            CameraShaker.Instance.ShakeOnce(10,10,0,2);
            Destroy(gameObject);
        }
        else if (hp.isDamaged) {
            AudioManager.Instance.PlaySFX(hurtSfx);
            FindObjectOfType<HitStop>().Stop(0,.1f);
            StartCoroutine(HitEffectPP());
            hp.isDamaged = false;
        }
    }

    void GroundHandler() {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (!isGrounded) {
            if (!isAir) isAir = !isAir;
        } else {
            if (isAir) {
                AudioManager.Instance.PlaySFX(landSfx);
                isAir = false;
            }        
        }
    }

    void JumpHandler() {
        // Reset extra jumps
        if (isGrounded) extraJumps = extraJumpsValue;

        // Jumping
        if (Input.GetKeyDown(jumpKey) || Input.GetKeyDown(altJumpKey)) {
            if (isGrounded) {
                rb.velocity = Vector2.up * jumpHeight;
                animator.SetTrigger("jump");
                AudioManager.Instance.PlaySFX(jumpSfx); 
            }
            else if (extraJumps > 0) {
                rb.velocity = Vector2.up * jumpHeight;
                animator.SetTrigger("jump");
                AudioManager.Instance.PlaySFX(jumpSfx); 
                extraJumps--; 
            }
        }
    }

    void AnimationHandler() {
        if (moveInput==0) animator.SetBool("isRunning",false);
        else animator.SetBool("isRunning",true);

        if (isGrounded) animator.SetBool("isAir",false);
        else animator.SetBool("isAir",true);
    }

    void FlipHandler() {
        if (facingRight == false && moveInput > 0) Flip();
        else if (facingRight == true && moveInput < 0) Flip();
    }

    void FixedUpdate() {
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }

    IEnumerator HitEffectPP() {
        hitEffectPP.SetActive(true);
        yield return new WaitForSecondsRealtime(.1f);
        hitEffectPP.SetActive(false);
    }

    void Flip() {
        facingRight = !facingRight;
        Vector3 scaler = sprite.transform.localScale;
        scaler.x *= -1;
        sprite.transform.localScale = scaler;
    }
}
