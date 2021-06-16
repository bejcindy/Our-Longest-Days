using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    
    //Creates an array of light objects which allows specific light objects to be dragged into the inspector
    public Light[] anxiousLights;
    //Other light objects which will be controlled below
    public Light lampLight;
    public Light Sun;


   
    void Start()
    {
        
    
        //For loop which runs through the public lights array in order to set their specific settings to specific values at the start of the game.
        for (int i = 0; i < anxiousLights.Length; i ++)
        {
            //Gets the light component properties of the objects in the array
            anxiousLights[i] = anxiousLights[i].GetComponent<Light>();
            anxiousLights[i].intensity = 0.34f;
            anxiousLights[i].bounceIntensity = 1.9f;
            anxiousLights[i].range = 10f;
        }
        //Same as with the loop above
        lampLight.intensity = 0f;
        lampLight.bounceIntensity = 0f;
        lampLight.range = 0f;
        lampLight.shadowStrength = 0f;

        //Same as with the loop above
        Sun.intensity = 0.5f;
        Sun.bounceIntensity = 1.96f;

        //Invoke Repeat function to apply the LampLight function every 3 seconds
        InvokeRepeating("LampLight", 0, 3);

    }
    /// <summary>
    /// Lerps lighting object intensities over time
    /// </summary>
    public void LightEffects()
    {
  
        //For loop which runs through the array and Lerps the values which were set in the Start function to change based on time.
        for (int i = 0; i < anxiousLights.Length; i++)
        {
            anxiousLights[i].intensity = Mathf.Lerp(anxiousLights[i].intensity, 1.20f, .04f * Time.deltaTime);
            anxiousLights[i].bounceIntensity = Mathf.Lerp(anxiousLights[i].bounceIntensity, 1f, .04f * Time.deltaTime);
            anxiousLights[i].range = Mathf.Lerp(anxiousLights[i].range, 20f, .04f * Time.deltaTime);
        }
        //Same as above
        Sun.intensity = Mathf.Lerp(Sun.intensity, 0f, .05f * Time.deltaTime);
        Sun.bounceIntensity = Mathf.Lerp(Sun.bounceIntensity, 0f, .05f * Time.deltaTime);


    }
    /// <summary>
    /// Lerps the lamp light object's intensity properties over time
    /// </summary>
    public void LampLight()
    {
        lampLight.intensity = Mathf.Lerp(lampLight.intensity, 1.42f, 1.2f * Time.deltaTime);
        lampLight.bounceIntensity = Mathf.Lerp(lampLight.bounceIntensity, 1.9f, 1.2f * Time.deltaTime);
        lampLight.range = Mathf.Lerp(lampLight.range, 7.81f, 1.2f * Time.deltaTime);
        lampLight.shadowStrength = Mathf.Lerp(lampLight.shadowStrength, .659f, 1.2f * Time.deltaTime);
    }

    /// <summary>
    /// Lerps the lamp light object's intensity properties over time to dim them
    /// </summary>
   /* public void DimLampLight()
    {
        lampLight.intensity = Mathf.Lerp(1.42f, 0f, 1.2f * Time.deltaTime);
        lampLight.bounceIntensity = Mathf.Lerp(1.9f, 0f, 1.2f * Time.deltaTime);
        lampLight.range = Mathf.Lerp(7.81f, 0f, 1.2f * Time.deltaTime);
        lampLight.shadowStrength = Mathf.Lerp(.659f, 0f, 1.2f * Time.deltaTime);
    } */


}
