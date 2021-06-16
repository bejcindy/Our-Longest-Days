using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    //Allows for the object with the Camera Shake script to be added to be able to access it here.
    public CameraShake cameraShake;

    [Header("Sounds")]
    private AudioManager audioManager;
    public string FridgeInteract = "FridgeInteract";
    public string GameController = "GameController";
    public string BedInteract = "BedInteract";
    public string PCInteract = "PCInteract";
    public string WorkoutInteract1 = "WorkoutInteract1";
    public string WorkoutInteract2 = "WorkoutInteract2";
    public string BookInteract1 = "BookInteract1";
    public string BookInteract2 = "BookInteract2";
    public string DarkFridgeInteract = "DarkFridgeInteract";
    public string IntenseDarkFridgeInteract = "IntenseDarkFridgeInteract";
    public string DarkBedInteract = "DarkBedInteract";
    public string IntenseDarkBedInteract = "IntenseDarkBedInteract";
    public string MildDarkPCInteract = "MildDarkPCInteract";
    public string IntenseDarkPCInteract = "IntenseDarkPCInteract";
    public string DarkBookInteract = "DarkBookInteract";
    public string DarkGymInteract = "DarkGymInteract";
    public string Shatter1 = "BottleShatter";
    public string Shatter2 = "RGlassShatter";
    public string Shatter3 = "GlasshShatter";




    void Start()
    {
        //caching. Just in case AudioManager happens to be missing from the scene
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("FREAK OUT! No AudioManager found in the scene.");
        }


    }

    //The trigger functions below control which specific sound will play when the player character collides with a specific object 
     void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "FoodInteract")
        {
            audioManager.PlaySound(FridgeInteract);
        }

        if (other.gameObject.tag == "DarkFoodInteract")
        {
            audioManager.PlaySound(DarkFridgeInteract);
        }

        if (other.gameObject.tag == "IntenseDarkFridgeInteract")
        {
            audioManager.PlaySound(IntenseDarkFridgeInteract);
        }

        if (other.gameObject.tag == "PCInteract")
        {
            audioManager.PlaySound(PCInteract);
        }

        if (other.gameObject.tag == "PCInteract")
        {
            audioManager.PlaySound(GameController);
        }

        if (other.gameObject.tag == "MildDarkPCInteract")
        {
            audioManager.PlaySound(MildDarkPCInteract);
        }

        if (other.gameObject.tag == "IntenseDarkPCInteract")
        {
            audioManager.PlaySound(IntenseDarkPCInteract);
        }

        if (other.gameObject.tag == "BedInteract")
        {
            audioManager.PlaySound(BedInteract);
        }

        if (other.gameObject.tag == "DarkBedInteract")
        {
            audioManager.PlaySound(DarkBedInteract);
        }

        if (other.gameObject.tag == "IntenseDarkBedInteract")
        {
            audioManager.PlaySound(IntenseDarkBedInteract);
        }

        if (other.gameObject.tag == "GymInteract")
        {
            audioManager.PlaySound(WorkoutInteract1);
            audioManager.PlaySound(WorkoutInteract2);
        }

        if (other.gameObject.tag == "DarkGymInteract")
        {
            audioManager.PlaySound(DarkGymInteract);
        }

        if (other.gameObject.tag == "BookInteract")
        {
            audioManager.PlaySound(BookInteract1);
            audioManager.PlaySound(BookInteract2);
        }

        if (other.gameObject.tag == "DarkBookInteract")
        {
            audioManager.PlaySound(DarkBookInteract);
        }

        if (other.gameObject.tag == "DarkInteractable")
        {
            audioManager.PlaySound(Shatter1);
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FoodInteract")
        {
            audioManager.StopSound(FridgeInteract);
        }

        if (other.gameObject.tag == "DarkFoodInteract")
        {
            audioManager.StopSound(DarkFridgeInteract);
        }

        if (other.gameObject.tag == "IntenseDarkFridgeInteract")
        {
            audioManager.StopSound(IntenseDarkFridgeInteract);
        }

        if (other.gameObject.tag == "PCInteract")
        {
            audioManager.StopSound(PCInteract);
        }

        if (other.gameObject.tag == "PCInteract")
        {
            audioManager.StopSound(GameController);
        }

        if (other.gameObject.tag == "MildDarkPCInteract")
        {
            audioManager.StopSound(MildDarkPCInteract);
        }

        if (other.gameObject.tag == "IntenseDarkPCInteract")
        {
            audioManager.StopSound(IntenseDarkPCInteract);
        }

        if (other.gameObject.tag == "BedInteract")
        {
            audioManager.StopSound(BedInteract);
        }

        if (other.gameObject.tag == "DarkBedInteract")
        {
            audioManager.StopSound(DarkBedInteract);
        }

        if (other.gameObject.tag == "IntenseDarkBedInteract")
        {
            audioManager.StopSound(IntenseDarkBedInteract);
        }

        if (other.gameObject.tag == "GymInteract")
        {
            audioManager.StopSound(WorkoutInteract1);
            audioManager.StopSound(WorkoutInteract2);
        }

        if (other.gameObject.tag == "DarkGymInteract")
        {
            audioManager.StopSound(DarkGymInteract);
        }

        if (other.gameObject.tag == "BookInteract")
        {
            audioManager.StopSound(BookInteract1);
            audioManager.StopSound(BookInteract2);
        }

        if (other.gameObject.tag == "DarkBookInteract")
        {
            audioManager.StopSound(DarkBookInteract);
        }

        if (other.gameObject.tag == "DarkInteractable")
        {
            audioManager.StopSound(Shatter1);
        }
    }
}
