using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class NewPlayerController : MonoBehaviour
{
    public LayerMask GroundLayer, InteractLayer;

    private NavMeshAgent myAgent;

    public Image FoodBar;
    public static float Food;
    public float HungerPerSec = .3f;

    public Image SMNBar;
    public static float SMN;

    public Image HealthBar;
    public static float Health;
    
    //possible destinations for player to go
    Vector3[] dest;
    GameObject[] interactables;
    //whether the character is in auto route finding mode
    bool auto = false;
    //whether the character is running the route finding coroutine
    bool findingRoute = false;
    //a timer couting how long there's no control input from player
    [Tooltip("How long should be waited after player does a mouse click")]
    public float waitTime;
    float noInputTime = 0;

    bool entered = false;
    
    public static float actTime;
    public GameObject coolDown;
    public static float coolDownFill;
    public static bool actionDone;

    // Start is called before the first frame update
    void Start()
    {
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        dest = new Vector3[interactables.Length];
        for(int i = 0; i < interactables.Length; i++)
        {
            dest[i] = interactables[i].transform.GetChild(0).position;
        }
        myAgent = GetComponent<NavMeshAgent>();
        if (DataHolder.didTutorial)
        {
            Food = 100;
        }
        else
        {
            Food = FoodBar.fillAmount * 100;
        }
        
        SMN = 100;
        Health = 100;
        actionDone = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterSpeed();
        //at the beginning of the game
        if (!GameManage.gameStarted && !GameManage.dialogFinished && !GameManage.dialog)
        {
            Debug.Log("called");
            noInputTime += Time.deltaTime * 2;
            //Debug.Log(noInputTime);
            if (auto && !findingRoute && !MentalBarController.belowHalf && !actionDone)
            {
                StartCoroutine(FindRoute());
            }

            
        }

        coolDownFill = Mathf.Clamp(coolDownFill, 0, 1);
        //Debug.Log(coolDownFill);
        coolDown.GetComponent<Image>().fillAmount = coolDownFill;
        if (coolDownFill >= 1 || coolDownFill <= 0)
        {
            coolDown.SetActive(false);
        }
        else
        {
            coolDown.SetActive(true);
        }
        Vector3 coolDownPos = Camera.main.WorldToScreenPoint(transform.position);
        coolDown.transform.position = coolDownPos;

        #region Auto Route Finding
        if (auto && !findingRoute && !MentalBarController.belowHalf && actionDone && GameManage.gameStarted && !GameManage.dialog)
        {
            StartCoroutine(FindRoute());
        }
        if (GameManage.dialog)
        {
            myAgent.isStopped = true;
        }
        else
        {
            myAgent.isStopped = false;
        }
        #endregion

        #region Manual Route Finding
        if (Input.GetMouseButtonDown(0) && GameManage.gameStarted || Input.GetMouseButtonDown(0) && GameManage.dialogFinished && !DataHolder.didTutorial)
        {
            auto = false;
            noInputTime = 0;
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, GroundLayer))
            {
                //go to the position of the child (usually the front of the object)
                myAgent.SetDestination(hitInfo.point);
            }
            if (Physics.Raycast(myRay, out hitInfo, 100, InteractLayer))
            {
                //go to the position of the child (usually the front of the object)
                Debug.Log(hitInfo.transform.name);
                myAgent.SetDestination(hitInfo.transform.GetChild(0).position);
            }
        }else if (transform.position == myAgent.destination && actionDone || entered && actionDone)
        {
            //Debug.Log(noInputTime);
            noInputTime += Time.deltaTime;
        }
        if (noInputTime >= waitTime)
        {
            noInputTime = waitTime;
            auto = true;
            entered = false;
        }
        #endregion

        if (GameManage.gameStarted || GameManage.dialogFinished && !DataHolder.didTutorial)
        {
            Food -= Time.deltaTime * HungerPerSec;
            //FoodBar.fillAmount = Food / 100;

            //clamp the floats between 0 and 100
            Food = Mathf.Clamp(Food, 0, 100);
            SMN = Mathf.Clamp(SMN, 0, 100);
            Health = Mathf.Clamp(Health, 0, 100);

            //create more gradual changes in the bars
            if (Food / 100 != FoodBar.fillAmount)
            {
                FoodBar.fillAmount += (Food / 100 - FoodBar.fillAmount) * Time.deltaTime;
            }
            if (SMN / 100 != SMNBar.fillAmount)
            {
                SMNBar.fillAmount += (SMN / 100 - SMNBar.fillAmount) * Time.deltaTime;
            }
            if (Health / 100 != HealthBar.fillAmount)
            {
                HealthBar.fillAmount += (Health / 100 - HealthBar.fillAmount) * Time.deltaTime;
            }
        }
        
    }

    public void CharacterSpeed()
    {
        if (MentalBarController.belowHalf)
        {
            myAgent.speed = 2f;
        }

        if (MentalBarController.belowQuater)
        {
            myAgent.speed = 1f;
        }
    }

    IEnumerator FindRoute()
    {
        //Debug.Log("findroute");
        findingRoute = true;
        int r = Random.Range(0, dest.Length);
        Debug.Log(interactables[r].name);
        myAgent.SetDestination(dest[r]);
        yield return new WaitForSeconds(Random.Range(5f, 9f));
        findingRoute = false;
        //yield return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        entered = true;
        for(int i = 0; i < interactables.Length; i++)
        {
            if (other.gameObject == interactables[i])
            {
                MentalBarController.ActionOrder += i;
                Debug.Log(MentalBarController.ActionOrder);
            }
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        entered = false;
    }
}
