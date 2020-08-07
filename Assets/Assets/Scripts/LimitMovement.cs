using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LimitMovement : MonoBehaviour
{
    private CinemachineBrain cinemachine;
    // Start is called before the first frame update
    void Start()
    {
        cinemachine = GetComponent<CinemachineBrain>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) {
        if (other.collider.tag == "EndPoint"){
            cinemachine.enabled = false;
        }
    }

}
