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
        if (collision.collider is BoxCollider2D)
        {
            // Si collision alors on met le portail en état
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
        // Si triggerEnter alors on ouvre le portail
        etat = 2;
        StartCoroutine(PortalOpen());
    }

    private void OnTriggerExit2D(Collider2D collision) {
        // Si triggerExit alors on ferme le portail
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
        yield return new WaitForSeconds(0.3f);
    }
    
}
