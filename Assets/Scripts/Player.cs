using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    private float speed = 3f;

    private Stamina stamina = new Stamina(100);
    private bool needRegen = false;
    private float regenStamina = 0.01f;
    private float useStamina = 0.05f;

    private float dashSpeed = 50f; // Vitesse de déplacement pendant le dash
    private float dashDuration = 0.2f; // Durée du dash en secondes
    private bool isDashing = false; // Indique si le joueur est en train de dasher

    public enum Direction { Left, Right, Up, Down }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("LogMessage", 0f, 1f);
    }

    void LogMessage()
    {
        Debug.Log("L'endurance est de : " + stamina.GetCurrentStamina() + " !");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Arbre") {
            Debug.Log("Le joueur a touché un abre !");
        }
    }

    /*void Movement(float speedValue) {
        // Récupérer les valeurs des axes horizontal et vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculer le déplacement du joueur
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f) * speedValue * Time.deltaTime;

        // Appliquer le déplacement au joueur
        transform.Translate(movement);
    }*/

    void HandleMovement() {
        float moveX = 0f;
        float moveY = 0f;
        Vector3 movement;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1f;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            moveY = 1f;
        } 
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
        }

        if (Input.GetKey(KeyCode.Space) && stamina.GetCurrentStamina() > 0f && needRegen == false) {
            movement = new Vector3(moveX, moveY, 0f) * speed * 5f * Time.deltaTime;
            stamina.UseStamina(useStamina);
        } else if (Input.GetKeyDown(KeyCode.F) && !isDashing) {
            StartCoroutine(Dash());
            movement = new Vector3(moveX, moveY, 0f) * speed * dashSpeed * Time.deltaTime;
        } else {
            if (stamina.GetCurrentStamina() == 0f) {
                needRegen = true;
            } 
            if (stamina.GetCurrentStamina() >= 10f) {
                needRegen = false;
            }
            movement = new Vector3(moveX, moveY, 0f) * speed * Time.deltaTime;
            stamina.RegenStamina(regenStamina);
        }
        
        transform.Translate(movement);
    }

    IEnumerator Dash()
    {
        isDashing = true;

        float elapsedTime = 0f;
        float initialSpeed = speed;
        float targetSpeed = 2f * speed;

        while (elapsedTime < dashDuration)
        {
            // Accélérer progressivement pendant le dash
            float currentSpeed = Mathf.Lerp(initialSpeed, targetSpeed, elapsedTime / dashDuration);

            // Déplacer le joueur en utilisant la nouvelle vitesse
            HandleMovement();

            // Mettre à jour le temps écoulé
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Fin du dash
        isDashing = false;
    }
}
