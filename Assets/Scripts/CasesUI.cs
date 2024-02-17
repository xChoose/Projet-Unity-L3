using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CasesUI : MonoBehaviour
{
    private Button bouton;
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
        InventoryUI inventoryUIScript = GetComponentInParent<InventoryUI>();
        GameObject slot = this.gameObject;
        if (inventoryUIScript.GetSlotStock() == null) {
            inventoryUIScript.SetSlotStock(slot);
        }
        else {
            inventoryUIScript.SwapCasesUI(slot);
        }
    }
}
