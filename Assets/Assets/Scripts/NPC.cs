using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool shown;

    public DialogueManager dialogueManager;

    public PlayerController playerController;

    public DialogueTrigger dialogueTrigger;

    public GameObject messageBox;
    public GameObject dialogueBox;

    public LayerMask playerDetection;

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit2DLeft = Physics2D.Raycast(transform.position, Vector2.left, 2f, playerDetection);
        RaycastHit2D hit2DRight = Physics2D.Raycast(transform.position, Vector2.right, 2f, playerDetection);

        if((hit2DLeft || hit2DRight)){
            messageBox.SetActive(true);
            dialogueBox.SetActive(true);
            shown = true;
        }
        
        else if (!hit2DLeft || !hit2DRight){
            messageBox.SetActive(false);
            dialogueBox.SetActive(false);
            shown = false;
        }

        if ((hit2DLeft || hit2DRight) && Input.GetKeyDown(KeyCode.E)){
            messageBox.SetActive(false);
            dialogueTrigger.TriggerDialogue();
        }
    }
}
