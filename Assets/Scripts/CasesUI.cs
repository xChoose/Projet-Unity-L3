using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasesUI : MonoBehaviour
{
    private Button bouton;
    private GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        bouton = GetComponent<Button>();
        go = bouton.onClick.AddListener(() => OnClickButton());
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public GameObject OnClickButton()
    {
        return this.gameObject;
        // Faites ce que vous voulez avec le GameObject du bouton ici
    }

    
}
