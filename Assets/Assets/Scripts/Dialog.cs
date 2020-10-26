using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textOnDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    [SerializeField] Animator anim;
    [SerializeField] GameObject conBtn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
        {
            textOnDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        conBtn.SetActive(true);
    }

    public void EndDialoge()
    {
        anim.SetTrigger("Close");
        StartCoroutine(CloseTimer());
    }

    IEnumerator CloseTimer()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
