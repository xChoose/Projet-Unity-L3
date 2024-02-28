using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private GameObject player;
    private Vector3 target;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player) {
             target = new Vector3(player.transform.position.x,player.transform.position.y, -10);
             transform.position = Vector3.SmoothDamp(target, target, ref velocity, 0,05f);
        }
    }
}
