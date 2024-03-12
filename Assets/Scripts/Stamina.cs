using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina
{
    private float maxStamina = 100f;
    private float currentStamina;
    private float regenStamina = 0.02f;
    private float useStamina = 0.1f;

    public Stamina(float maxStamina)
    {
        this.maxStamina = maxStamina;
        currentStamina = maxStamina;
    }
    
    public float GetRegen() {
        return regenStamina;
    }

    public float GetUseStamina() {
        return useStamina;
    }

    public float GetCurrentStamina()
    {
        return currentStamina;
    }

    public void UseStamina(float amount)
    {
        currentStamina -= amount;
        if (currentStamina < 0)
        {
            currentStamina = 0;
        }
    }

    public void RegenStamina(float amount)
    {
        currentStamina += amount;
        if (currentStamina > maxStamina)
        {
            currentStamina = maxStamina;
        }
    }
}
