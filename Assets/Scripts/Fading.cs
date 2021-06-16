using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fading : MonoBehaviour
{
    public Animator animator;
    private int levelToLoad;

    float currentTime = 0f;
    float startingTime = 20f;

    void Start()
    {
        currentTime = startingTime;
    }

    
    void Update()
    {
        if (MentalBarController.isZero == true)
        {
            currentTime -= 1 * Time.deltaTime;
            if(currentTime <= 0)
            {
                Fade(1);
            }
        }
    }

    public void Fade(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    public void FadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
