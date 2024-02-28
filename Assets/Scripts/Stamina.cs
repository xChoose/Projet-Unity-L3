using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    public float maxStamina = 100;
    public float currentStamina;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Stamina(float maxStamina)
    {
        this.maxStamina = maxStamina;
        currentStamina = maxStamina;
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
