using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Dictionary<int, Items> instancesItems = new Dictionary<int, Items>();

    private bool lancement = true;
    public GameObject player;
    private Player playerScript;
    private Stamina stamina;
    private Health health;
    private List<Cases> inventory;
    private Items[] items;


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

        playerScript = GameObject.Find("Player").GetComponent<Player>();
    }

    public static GameManager Instance
    {
        get { return instance; }
    }

    public Stamina GetStamina() {
        return stamina;
    }

    public Health GetHealth() {
        return health;
    }

    public List<Cases> GetInventory() {
        return inventory;
    }

    public bool GetLancement() {
        return lancement;
    }

    void LoadInstancesItems() 
    {
        items = Resources.LoadAll<Items>("InstancesItems/");
    }

    public Items FindItemsDictionary(int index) 
    {
        return items[index];
    }

    public void SaveStats() {
        stamina = playerScript.GetStamina();
        health = playerScript.GetHealth();
        inventory = player.GetComponent<Inventory>().GetInventory();
        /*for (int i =0; i < inventory.Count; i++) {
            Debug.Log("Test :" + inventory[i].GetItem());
        }*/
        if (GameObject.Find("StatsNewScene") == null) {
            lancement = false;
        }
    }
}
