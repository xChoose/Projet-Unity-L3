using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject inventaire;

    private Dictionary<string, Items> instancesItems = new Dictionary<string, Items>();

    // Start is called before the first frame update
    void Start()
    {
        inventaire = GameObject.Find("Inventory");
        inventaire.gameObject.SetActive(false);
        LoadInstancesItems();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventaire.gameObject.activeSelf) {
                inventaire.gameObject.SetActive(false);
            } else {
                inventaire.gameObject.SetActive(true);
            }
        }
    }

    void LoadInstancesItems() 
    {
        string cheminDossierItems = "Assets/Scripts/InstancesItems";
        string[] fichiers = AssetDatabase.FindAssets("", new[] {cheminDossierItems});
        Dictionary<string, Items> instancesItems = new Dictionary<string,Items>();
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
            Debug.Log("Items " + i + ": " + itemChargé.name);
        }
    }
}
