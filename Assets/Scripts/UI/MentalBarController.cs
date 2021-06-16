using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class MentalBarController : MonoBehaviour
{
    //Finds the EffectManager and LightManager scripts in the scene
    private EffectManager em;
    private LightManager lm;

    //stores player's actions; new character is added in the Player script
    public static string ActionOrder;
    //detecting whether the mental bar is below a certain amount
    public static bool belowHalf, belowQuater, isZero;

    public static float Mental;

    public Image mentalBar;
    public float reduceAmount;
    public float hungryTime, staminaTime, healthTime;
    [Header("Light and Sounds")]
    public GameObject dayLight, daySounds,darkSounds, intensedarkSounds, distort;
    public CameraShake cameraShake;
    public float cameraShakeGap;

    [SerializeField]
    int oldLength, newLength;
    float hungryTimer = 0;
    float staminaTimer = 0;
    float healthTimer = 0;
    bool shake = false;

    // Start is called before the first frame update
    void Start()
    {
        //Looks for the EffectManager and LightManager in scene based on their tags.
        em = GameObject.FindGameObjectWithTag("GM").GetComponent<EffectManager>();
        lm = GameObject.FindGameObjectWithTag("LM").GetComponent<LightManager>();
        ActionOrder = "";
        oldLength = 0;
        InvokeRepeating("Shake", 0, cameraShakeGap);
        Mental = 100;
    }

   void Shake()
    {
        if (shake)
        {
            //The two variables here allow for editing of the magnitude as well as the duration of the shake
            cameraShake.ScreenShake(.5f, 4f);
        }
    } 

    // Update is called once per frame
    void Update()
    {
        newLength = ActionOrder.Length;
        //mentalBar.fillAmount = Mental / 100;
        //clamp the mental bar fillAmount between 0 and 1
        Mental = Mathf.Clamp(Mental, 0, 100);

        if (GameManage.gameStarted || GameManage.dialogFinished && !DataHolder.didTutorial)
        {
            Mental -= Time.deltaTime * .1f;
        }

            //a more gradual change in the bar
            if (Mental / 100 != mentalBar.fillAmount && mentalBar.fillAmount>0.01f)
        {
            mentalBar.fillAmount += (Mental / 100 - mentalBar.fillAmount) * Time.deltaTime;
        }
        if (mentalBar.fillAmount<.01f)
        {
            Mental = 0;
            mentalBar.fillAmount = 0;
        }

        //set decters to true
        if (mentalBar.fillAmount <= .8f)
        {
            lm.LightEffects();
            //dayLight.SetActive(false);
            daySounds.SetActive(false);;
            darkSounds.SetActive(true);
            

        }
        else
        {
            //dayLight.SetActive(true);
            daySounds.SetActive(true);
            darkSounds.SetActive(false);
           
        }
        if (mentalBar.fillAmount <= .5f)
        {
            dayLight.SetActive(false);   
            intensedarkSounds.SetActive(true);
            belowHalf = true;
            
        }
        else
        {
            belowHalf = false;
            intensedarkSounds.SetActive(false);
            dayLight.SetActive(true);
            
        }

        if (mentalBar.fillAmount <= .40f)
        {

            em.DistortionEffects();
            intensedarkSounds.SetActive(true);
            belowQuater = true;
            shake = true;
        }
        else
        {

            intensedarkSounds.SetActive(false);
            belowQuater = false;
            shake = false;
        }
        if (mentalBar.fillAmount == 0)
        {
            isZero = true;
        }
        else
        {
            isZero = false;
            
        }

        //if new behaviors are done and the player has done over four behaviors, detect if the behaviors are done repeatitively. if so, reduce mental bar
        if (newLength > oldLength && newLength >= 4)
        {
            //if player do one thing repetitively (e.g. eat eat eat)
            if (ActionOrder[newLength-1] == ActionOrder[newLength - 2])
            {
                //mentalBar.fillAmount -= reduceAmount / 100;
                Mental -= reduceAmount;
                oldLength = newLength;
            }
            //if player do two things repeatitively (e.g. eat sleep eat sleep)
            if (ActionOrder[newLength - 1] == ActionOrder[newLength - 3] && ActionOrder[newLength - 2] == ActionOrder[newLength - 4])
            {
                //mentalBar.fillAmount -= reduceAmount / 100;
                Mental -= reduceAmount;
                oldLength = newLength;
            }
        }

        //if food bar is empty for a while
        if (NewPlayerController.Food <= 0)
        {
            hungryTimer += Time.deltaTime;
            if (hungryTimer >= hungryTime)
            {
                //mentalBar.fillAmount -= 0.0001f;
                Mental -= 0.01f;
            }
        }
        //if stamina bar drops under 1/3 for a while
        if (NewPlayerController.SMN <= 33)
        {
            
            staminaTimer += Time.deltaTime;
            if (staminaTimer >= staminaTime)
            {
                //mentalBar.fillAmount -= 0.0001f;
                Mental -= 0.01f;
            }
        }
        //if health bar drops under 1/2 for a while
        if (NewPlayerController.Health <= 50)
        {
            healthTimer += Time.deltaTime;
            if (healthTimer >= healthTime)
            {
                //mentalBar.fillAmount -= 0.0001f;
                Mental -= 0.01f;
            }
        }
    }
}
