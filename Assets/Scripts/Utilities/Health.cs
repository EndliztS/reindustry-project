using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 100;
    public int currentHealth;
    public bool isDead;
    public bool isDamaged = false;
    [SerializeField] Slider hpUI;

    private void Start() {
        currentHealth = health;
        if (hpUI) hpUI.maxValue = health;
    }

    private void Update() {        
        if (currentHealth<=0) {
            isDead = true;
        }

        if (hpUI) hpUI.value = currentHealth;
    }

    public void TakeDamage(int damage) {
        if (damage>=currentHealth) currentHealth=0;
        else {            
            isDamaged = true;
            currentHealth -= damage;
        }
    }
}
