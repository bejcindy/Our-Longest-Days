using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [Header("Status Changes to Player once Entered")]
    public float hunger;
    [InspectorName("entertainment")]
    public float stamina;

    [InspectorName("rest")]
    public float health;

    public float mental;
    public float actionTime;

    public GameObject hoverIcon;

    public bool InTutorial;

    bool actionStart;

    // Start is called before the first frame update
    void Start()
    {
        actionStart = false;
        hoverIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (actionStart)
        {
            NewPlayerController.actTime += Time.deltaTime;
            NewPlayerController.coolDownFill = NewPlayerController.actTime / actionTime;
            NewPlayerController.actionDone = false;
        }
        if (NewPlayerController.actTime >= actionTime && actionStart)
        {
            NewPlayerController.actTime = 0;
            actionStart = false;
            NewPlayerController.actionDone = true;
            Debug.Log("set true");
            DataHolder.didTutorial = true;
            GameManage.gameStarted = true;
            NewPlayerController.Food += hunger;
            NewPlayerController.SMN += stamina;
            NewPlayerController.Health += health;
            MentalBarController.Mental += mental;
        }
        if (hoverIcon)
        {
            hoverIcon.transform.position = Input.mousePosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (InTutorial && GameManage.dialogFinished)
            {
                NewPlayerController.actTime = 0;
                actionStart = true;
                NewPlayerController.actionDone = false;
                NewPlayerController.coolDownFill = 0;
                GameManage.fridgeAnim.SetBool("flash", false);
                Debug.Log("set false");
            }
            if(!InTutorial && GameManage.gameStarted)
            {
                NewPlayerController.actTime = 0;
                actionStart = true;
                NewPlayerController.actionDone = false;
                NewPlayerController.coolDownFill = 0;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(InTutorial && GameManage.dialogFinished)
            {
                actionStart = false;
                NewPlayerController.actTime = 0;
                NewPlayerController.actionDone = false;
                NewPlayerController.coolDownFill = 0;
                if (!DataHolder.didTutorial)
                {
                    GameManage.fridgeAnim.SetBool("flash", true);
                    Debug.Log("set true");
                }
            }
            if (!InTutorial && GameManage.gameStarted)
            {
                actionStart = false;
                NewPlayerController.actTime = 0;
                NewPlayerController.actionDone = false;
                NewPlayerController.coolDownFill = 0;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (GameManage.dialogFinished)
        {
            Cursor.visible = false;
            hoverIcon.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        if (GameManage.dialogFinished)
        {
            Cursor.visible = true;
            hoverIcon.SetActive(false);
        }
    }
    
}
