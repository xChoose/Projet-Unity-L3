using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    private float speed = 2f;

    private GameObject inventaire;
    private List<Cases> stockInventory;

    private Stamina stamina = new Stamina(100);
    private bool needRegen = false;

    private Health health = new Health(100);

    private bool isDashing = false; // Indique si le joueur est en train de dasher

    private Animator animator;
    private const string horizontal = "Horizontal";
    private const string vertical = "Vertical";
    private const string lastHorizontal = "LastHorizontal";
    private const string lastVertical = "LastVertical";

    public enum Direction { Left, Right, Up, Down }

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallRegenerateHealth",0f,5f);
        inventaire = GameObject.Find("Inventory");
        inventaire.gameObject.SetActive(false);
    }

    void Awake() {
        animator = GetComponent<Animator>();
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

    public Health GetHealth() {
        return health;
    }

    public void SetHealth(Health newHealth) {
        health = newHealth;
    }

    void CallRegenerateHealth() {
        health.RegenerateHealth();
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
            stamina.UseStamina(stamina.GetUseStamina());
        } else if (Input.GetKeyDown(KeyCode.F) && !isDashing && stamina.GetCurrentStamina() > 40f){
            StartCoroutine(Dash(lastMoveDirection));
            stamina.UseStamina(stamina.GetUseStamina()*400);
            return;
        } else {
            if (stamina.GetCurrentStamina() == 0f) {
                needRegen = true;
            } 
            if (stamina.GetCurrentStamina() >= 10f) {
                needRegen = false;
            }
            movement = new Vector3(moveX, moveY, 0f) * speed * Time.deltaTime;
            stamina.RegenStamina(stamina.GetRegen());
        }

        animator.SetFloat(horizontal,moveX);
        animator.SetFloat(vertical,moveY);
        if (movement != Vector3.zero) {
            animator.SetFloat(lastHorizontal,moveX);
            animator.SetFloat(lastVertical,moveY);
        }
        
        transform.Translate(movement);
    }

    IEnumerator Dash(Vector3 direction)
    {
        isDashing = true;

        float startTime = Time.time;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction.normalized * 5f;
        
        while (Time.time - startTime < 0.2f) //0.2f est la durée d'un dash
        {
            float t = (Time.time - startTime) / 0.2f;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        isDashing = false;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Arbre") {
            Debug.Log("Le joueur a touché un abre !");
        }
    }*/
}
