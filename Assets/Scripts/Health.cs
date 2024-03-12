using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    private float maxHealth = 100f;
    private float currentHealth;

    public Health(float maxHealth) {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public float GetCurrentHealth() {
        return currentHealth;
    }

    public void LooseHP(float damage) {
        currentHealth -= damage;
        if (currentHealth < 0) {
            currentHealth = 0;
        }
    }

    public void RegenerateHealth() {
        currentHealth += 10f;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth;
        }
    }
}
