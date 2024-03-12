using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortailUI : MonoBehaviour
{
    private Portail portail;
    private bool isPortailActive = false;
    [SerializeField] private Sprite[] sprites; // 0 : Portail fermé, 1 : Portail ouvert, 2 : Portail actif

    // Start is called before the first frame update
    void Start()
    {
        portail = GameObject.Find("Portal").GetComponent<Portail>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeSprite(portail.GetEtat());
    }

    public bool GetIsPortailActive()
    {
        return isPortailActive;
    }

    public void SetIsPortailActive(bool value)
    {
        isPortailActive = value;
    }

    public void ChangeSprite(int index)
    {
        portail.GetComponent<SpriteRenderer>().sprite = sprites[index];
    }
}
