using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;
    public Image FoodBar;
    public float startFood = 100;
    public static float Food;
    public float HungerPerSec = 1;
    public float foodrecoverAmount = 20;

    public Image SMNBar;
    public float startSMN = 100;
    public static float SMN;
    public float SMNLost = 25;
    public float SMNGainRest = 25;
    public float SMNGainEat = 10;

    public Image HealthBar;
    public float startHealth = 100;
    public static float Health;
    public float HealthrecoverAmount = 10;
    //public float HealthMinus = 10;
    public float BadHealth = 10;


    Transform fridge, bed, pc;
    //whether the character is in auto route finding mode
    bool auto = true;
    //whether the character is running the route finding coroutine
    bool findingRoute = false;
    bool entered = false;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        Food = startFood;
        SMN = startSMN;
        Health = startHealth;
        fridge = GameObject.FindGameObjectWithTag("food").transform;
        bed = GameObject.FindGameObjectWithTag("rest").transform;
        pc = GameObject.FindGameObjectWithTag("action").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("auto: " + auto + ", findingRoute: " + findingRoute + ", entered: " + entered);
        //Debug.Log(entered);
        #region Auto Route Finding
        if (auto && !findingRoute)
        {
            StartCoroutine(FindRoute());
        }
        if (auto && findingRoute && entered)
        {
            StartCoroutine(WaitBeforeMoveAgain());
        }
        #endregion

        #region Manual Route Finding
        if (Input.GetMouseButtonDown(0))
        {
            auto = false;
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if(Physics.Raycast(myRay, out hitInfo, 100, whatCanBeClickedOn))
            {
                myAgent.SetDestination(hitInfo.point);
            }
        }
        #endregion

        Food -= Time.deltaTime * HungerPerSec;
        FoodBar.fillAmount = Food / startFood;

        //clamp the floats between 0 and 100
        Food = Mathf.Clamp(Food, 0, 100);
        SMN = Mathf.Clamp(SMN, 0, 100);
        Health = Mathf.Clamp(Health, 0, 100);

        //Health -= Time.deltaTime * BadHealthPerSec;
        //HealthBar.fillAmount = Health / startHealth;
    }

    IEnumerator FindRoute()
    {
        //Debug.Log("findroute");
        findingRoute = true;
        entered = false;
        yield return new WaitForSeconds(Random.Range(2, 4));
        int r = Random.Range(1, 4);
        if (r == 1)
        {
            myAgent.SetDestination(fridge.position);
            Debug.Log("destination fridge");
        }else if (r == 2)
        {
            myAgent.SetDestination(bed.position);
            Debug.Log("destination bed");
        }else if (r == 3)
        {
            myAgent.SetDestination(pc.position);
            Debug.Log("destination pc");
        }
    }

    IEnumerator WaitBeforeMoveAgain()
    {
        Debug.Log("waiting");
        entered = false;
        yield return new WaitForSeconds(Random.Range(2, 4));
        findingRoute = false;
    }

    void OnTriggerEnter(Collider other)
    {
        entered = true;
        auto = true;
        if (other.gameObject.tag == "food")
        {
            Food += foodrecoverAmount;
            FoodBar.fillAmount = Food / startFood;
            //SMN += SMNGainEat;
            Debug.Log("eating");
            Health -= BadHealth;
            HealthBar.fillAmount = Health / startHealth;
            //add food behavior to the actions
            MentalBarController.ActionOrder += "f";
        }

        if(other.gameObject.tag == "action")
        {
            SMN -= SMNLost;
            SMNBar.fillAmount = SMN / startSMN;

            Health += HealthrecoverAmount;
            HealthBar.fillAmount = Health;
            Debug.Log("acting");
            //add action behavior to the actions
            MentalBarController.ActionOrder += "a";
        }

        if(other.gameObject.tag == "rest")
        {
            SMN += SMNGainRest;
            SMNBar.fillAmount = SMN;
            Debug.Log("sleep");
            //add rest behavior to the actions
            MentalBarController.ActionOrder += "r";
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("food") && myAgent.destination == fridge.position)
        {
            entered = true;
            Debug.Log("same fridge");
        }
        if (other.CompareTag("action") && myAgent.destination == pc.position)
        {
            entered = true;
            Debug.Log("same pc");
        }
        if (other.CompareTag("rest") && myAgent.destination == bed.position)
        {
            Debug.Log("same bed");
            entered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        entered = false;
    }
}
