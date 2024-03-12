using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    private float speed = 2f;

    private GameObject inventaire;

    private Stamina stamina = new Stamina(100);
    private bool needRegen = false;
    private float regenStamina = 0.02f;
    private float useStamina = 0.1f;

    private float dashDistance = 6f; // Distance parcourue par le dash
    private float dashDuration = 0.2f; // Durée du dash en secondes
    private bool isDashing = false; // Indique si le joueur est en train de dasher

    public enum Direction { Left, Right, Up, Down }

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("LogMessage", 0f, 1f);
        inventaire = GameObject.Find("Inventory");
        inventaire.gameObject.SetActive(false);
    }

    void LogMessage()
    {
        Debug.Log("L'endurance est de : " + stamina.GetCurrentStamina() + " !");
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventaire.gameObject.activeSelf) {
                inventaire.gameObject.SetActive(false);
            } else {
                inventaire.gameObject.SetActive(true);
            }
        }
        if (inventaire.gameObject.activeSelf && Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<Inventory>().DropItem();
        }
    }

    public Stamina GetStamina() {
        return stamina;
    }

    public void SetStamina(Stamina newStamina) {
        stamina = newStamina;
    }

    public Inventory GetInventory() {
        return GetComponent<Inventory>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Arbre") {
            Debug.Log("Le joueur a touché un abre !");
        }
    }

    void HandleMovement() {
        float moveX = 0f;
        float moveY = 0f;
        Vector3 movement;
        Vector3 lastMoveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftArrow)) {
            moveX = -1f;
            lastMoveDirection = Vector3.left;
        }
        if (Input.GetKey(KeyCode.RightArrow)) {
            moveX = 1f;
            lastMoveDirection = Vector3.right;
        }
        if (Input.GetKey(KeyCode.UpArrow)) {
            moveY = 1f;
            lastMoveDirection = Vector3.up;
        } 
        if (Input.GetKey(KeyCode.DownArrow)) {
            moveY = -1f;
            lastMoveDirection = Vector3.down;
        }

        if (Input.GetKey(KeyCode.Space) && stamina.GetCurrentStamina() > 0f && needRegen == false) {
            movement = new Vector3(moveX, moveY, 0f) * speed * 2f * Time.deltaTime;
            stamina.UseStamina(useStamina);
        } else if (Input.GetKeyDown(KeyCode.F) && !isDashing && stamina.GetCurrentStamina() > 20f){
            StartCoroutine(Dash(lastMoveDirection));
            
            return;
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

    IEnumerator Dash(Vector3 direction)
    {
        isDashing = true;

        float startTime = Time.time;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction.normalized * dashDistance;
        Debug.Log("Start : " + startPos);
        Debug.Log("End : " + endPos);
        
        while (Time.time - startTime < dashDuration)
        {
            float t = (Time.time - startTime) / dashDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isDashing = false;
    }

}
