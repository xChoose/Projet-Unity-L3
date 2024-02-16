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
        bouton.onClick.AddListener(() => OnClickButton());
    }

    // Update is called once per frame
    void Update()
    {   
    }

    public void OnClickButton()
    {
        GameObject slot = this.gameObject;
        InventoryUI inventoryUIScript = GetComponentInParent<InventoryUI>();

        inventoryUIScript.SwapCasesUI(slot);
    }


}
