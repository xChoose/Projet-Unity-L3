using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portail : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    private int etat = 0;

    private Animation anim;

    private void Awake() {
        anim = gameObject.GetComponent<Animation>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Charger la scène spécifiée
        if (collision.collider is BoxCollider2D)
        {
            etat = 1;

            GameManager.Instance.SaveStats();
            StartCoroutine(PortalClose());
            SceneManager.LoadScene(sceneToLoad);
        }
        if (collision.collider is CircleCollider2D)
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        etat = 2;
        StartCoroutine(PortalOpen());
    }

    private void OnTriggerExit2D(Collider2D collision) {
        etat = 0;
    }

    public int GetEtat() {
        return etat;
    }

    IEnumerator PortalOpen() {
        anim.Play("Portal open");
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator PortalClose() {
        anim.Play("Portal close");
        yield return new WaitForSeconds(10f);
    }
    
}
