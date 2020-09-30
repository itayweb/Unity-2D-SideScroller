using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    private Animator anim;

    public Transform creditsCheck;
    public Transform creditsEnding;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AlertObservers(string message)
    {
        if (message == "AnimationEnded")
        {
            SceneManager.LoadScene(0);
        }
    }
}
