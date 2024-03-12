using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portail : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private int etat = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Charger la scène spécifiée
        if (collision.collider is BoxCollider2D)
        {
            Debug.Log("etat");
            etat = 1;

            GameManager.Instance.SaveStats();

            SceneManager.LoadScene(sceneToLoad);
        }
        if (collision.collider is CircleCollider2D)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Ouvert");
        etat = 2;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        Debug.Log("Fermer");
        etat = 0;
    }

    public int GetEtat() {
        return etat;
    }


    
}
