using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPit : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D col) {
        var hp = col.gameObject.GetComponent<Health>();
        if (hp) hp.isDead = true;
    }
}
