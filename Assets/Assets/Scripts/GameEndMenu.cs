using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour
{
    [SerializeField] GameObject player;
    private Rigidbody2D rb;

    private void Awake()
    {
        StartCoroutine(DisableVelocity());
        StartCoroutine(Timer());
    }

    IEnumerator DisableVelocity()
    {
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().enabled = false;
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
