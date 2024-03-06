using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portail : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Charger la scène spécifiée
        SceneManager.LoadScene(sceneToLoad);
    }
}
