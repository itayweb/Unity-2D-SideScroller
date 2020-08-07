using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public GameObject coin;
    public GameObject openedChest;
    public GameObject key;

    public LayerMask chestDetection;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2D = Physics2D.Raycast(gameObject.transform.position, transform.TransformDirection(Vector2.right), 2f, chestDetection);

        if (hit2D){
            key.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)){
                key.SetActive(false);
                this.gameObject.SetActive(false);
                openedChest.SetActive(true);
                coin.SetActive(true);
            }
        }
        else if (!hit2D){
            key.SetActive(false);
        }
    }
}
