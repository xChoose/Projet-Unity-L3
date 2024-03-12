using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Dictionary<int, Items> instancesItems = new Dictionary<int, Items>();

    private bool lancement = true;
    private Player player;
    private Stamina stamina;


    // Instance statique du GameManager
    private static GameManager instance;

    void Awake() 
    {
        LoadInstancesItems();

        // Vérifie si une instance existe déjà
        if (instance == null)
        {
            // Si aucune instance n'existe, définissez celle-ci comme instance unique
            instance = this;

            // Ne détruisez pas ce GameObject lors du chargement d'une nouvelle scène
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si une instance existe déjà, détruisez celle-ci
            Destroy(gameObject);
        }

        player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Méthode pour accéder à l'instance du GameManager
    public static GameManager Instance
    {
        get { return instance; }
    }

    public Stamina GetStamina() {
        return stamina;
    }

    public bool GetLancement() {
        return lancement;
    }

    void LoadInstancesItems() 
    {
        string cheminDossierItems = "Assets/Scripts/InstancesItems";
        string[] fichiers = AssetDatabase.FindAssets("", new[] {cheminDossierItems});
        int i = 0;

        foreach(string fichierID in fichiers) {
            i++;
            string cheminItem = AssetDatabase.GUIDToAssetPath(fichierID);
            Items itemChargé = AssetDatabase.LoadAssetAtPath<Items>(cheminItem);
            if (itemChargé != null) {
                instancesItems.Add(itemChargé.GetIdItem(),itemChargé);
            }
            else {
                Debug.LogError("Impossible de charger l'item à partir du chemin : " + cheminItem);
            }
        }
    }

    public void SaveStats() {
        stamina = player.GetStamina();
        if (GameObject.Find("StatsNewScene") == null) {
            lancement = false;
        }
    }

    public Items FindItemsDictionary(int index) 
    {
        return instancesItems[index];
    }
}
