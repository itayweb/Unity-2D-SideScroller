using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldWoman : MonoBehaviour
{
    [SerializeField] GameObject dialoge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2DLeft = Physics2D.Raycast(transform.position, Vector2.left, 2f);
        RaycastHit2D hit2DRight = Physics2D.Raycast(transform.position, Vector2.right, 2f);

        if ((hit2DLeft || hit2DRight) && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Working");
            dialoge.SetActive(true);
        }

        else if (!hit2DLeft || !hit2DRight)
        {
            dialoge.SetActive(false);
        }
    }
}
