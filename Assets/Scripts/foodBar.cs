using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class foodBar : MonoBehaviour
{
    public Image FoodBar;
    public float startFood = 100;
    public float Food;
    public float HungerPerSec = 5;
    public float recoverAmount = 10;

    // Start is called before the first frame update
    void Start()
    {
        Food = startFood;
    }

    // Update is called once per frame
    void Update()
    {
        Food -= Time.deltaTime * HungerPerSec;
        FoodBar.fillAmount = Food / startFood;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "food")
        {
            Food += recoverAmount;
            FoodBar.fillAmount = Food / startFood;
            
            
        }
    }
}
