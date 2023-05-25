using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHit : MonoBehaviour
{    
    public bool isPlayer = false;
    [HideInInspector]
    public int damage;

    void OnTriggerEnter2D(Collider2D col) {
        if (col.GetComponent<Health>() && !col.CompareTag("Player") && !isPlayer) col.GetComponent<Health>().TakeDamage(damage);
        else if (col.GetComponent<Health>() && col.CompareTag("Player") && isPlayer) col.GetComponent<Health>().TakeDamage(damage);
    }
}
