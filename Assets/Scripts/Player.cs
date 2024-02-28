using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite[] sprites;
    private float speed = 5f;

    public enum Direction { Left, Right, Up, Down }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement() {
        // Récupérer les valeurs des axes horizontal et vertical
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculer le déplacement du joueur
        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0f) * speed * Time.deltaTime;

        // Appliquer le déplacement au joueur
        transform.Translate(movement);
    }
}
