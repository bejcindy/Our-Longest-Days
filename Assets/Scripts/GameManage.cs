using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    public static bool gameStarted;
    public static bool dialogFinished;
    public static bool dialog;

    public GameObject Title, UI, Dialog, DialogManager;
    public GameObject mentalBar;
    public GameObject fridge;

    float fadeInSpeed = .75f;
    int finishedFadeIns = 0;
    public static Animator fridgeAnim;
    bool doingTutorial;

    // Start is called before the first frame update
    void Start()
    {
        doingTutorial = false;
        gameStarted = false;
        Title.SetActive(true);
        UI.SetActive(false);
        Dialog.SetActive(false);
        DialogManager.SetActive(false);
        dialog = false;
        dialogFinished = false;
        foreach(Transform t in UI.transform)
        {
            if (t.GetComponent<CanvasGroup>())
            {
                t.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        fridgeAnim = fridge.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted && Input.GetMouseButtonDown(0) && !dialog)
        {
            //gameStarted = true;
            //Title.SetActive(false);
            //UI.SetActive(true);
            dialog = true;
            Title.SetActive(false);
            Dialog.SetActive(true);
            DialogManager.SetActive(true);
        }
        if (dialogFinished)
        {
            //gameStarted = true;
            if (dialog)
            {
                dialog = false;
                Dialog.SetActive(false);
                DialogManager.SetActive(false);
                UI.SetActive(true);
                fridgeAnim.SetBool("flash", true);
            }
            //Debug.Log("didTutorial, " + DataHolder.didTutorial);
            if (!DataHolder.didTutorial)
            {
                if (UI.transform.Find("foodBG").gameObject.GetComponent<CanvasGroup>().alpha != 1)
                {
                    FadeIn("foodBG");
                }
            }
            if(DataHolder.didTutorial)
            {
                gameStarted = true;
                if (UI.transform.Find("EntertainmentBG").gameObject.GetComponent<CanvasGroup>().alpha != 1)
                {
                    FadeIn("EntertainmentBG");
                }
                if (UI.transform.Find("RestBG").gameObject.GetComponent<CanvasGroup>().alpha != 1)
                {
                    FadeIn("RestBG");
                }
                if (UI.transform.Find("MentalBG").gameObject.GetComponent<CanvasGroup>().alpha != 1)
                {
                    FadeIn("MentalBG");
                }
            }
        }
        UI.GetComponent<CanvasGroup>().alpha = mentalBar.GetComponent<Image>().fillAmount;
    }
    void FadeIn(string name)
    {
        Debug.Log("function called");
        GameObject go = UI.transform.Find(name).gameObject;
        if (go.GetComponent<CanvasGroup>().alpha < 1)
        {
            go.GetComponent<CanvasGroup>().alpha += Time.deltaTime * fadeInSpeed;
        }
        else
        {
            go.GetComponent<CanvasGroup>().alpha = 1;
            return;
        }
    }
}
