using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Gun : MonoBehaviour
{
    public GunData data;
    [SerializeField] Bullet bullet;
    [SerializeField] ParticleSystem muzzleFx;
    [SerializeField] Transform shootPoint;

    Rigidbody2D rb;
    Collider2D coll;

    bool readyToShoot;

    private int currentAmmo;

    private void Start() {
        currentAmmo = data.mag;
        readyToShoot = true;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && !data.auto) Shoot();
        if (Input.GetKey(KeyCode.Mouse0) && readyToShoot && data.auto) Shoot();

        if (currentAmmo <= 0) Destroy(gameObject);
    }

    private void Shoot() {
        readyToShoot = false;
        currentAmmo--;
        muzzleFx.Play();

        if (data.shotgun) 
            for (int i = 0; i < data.pellets; i++)
            {
                ShootBullet();
            } 
        else ShootBullet();

        CameraShaker.Instance.ShakeOnce(data.magnitude, data.roughness, data.fadeIn, data.fadeOut);
        AudioManager.Instance.PlaySFX(data.shootSfx[Random.Range(0,data.shootSfx.Length)]);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().AddForce(data.knockback * -transform.right, ForceMode2D.Impulse);

        Invoke("ResetShoot",data.fireRate);
    }

    void ShootBullet() {
        Bullet bulletPrefab = Instantiate(bullet, shootPoint.position, transform.rotation);
        Vector2 dir = transform.rotation * Vector2.right;
        Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-data.spread,data.spread);
        bulletPrefab.GetComponent<Rigidbody2D>().velocity = (dir + pdir) * data.bulletSpeed;
        bulletPrefab.damage = data.damage;
    }

    private void ResetShoot() {
        readyToShoot = true;
    }
}
