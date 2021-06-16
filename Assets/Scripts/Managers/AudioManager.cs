using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Used in order for the SerializedField function to work and display in the Inspector.
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    //All variables below are used as sound editing variables that can be found on an Audio Source in scene
    //but are instead used here to control sound variables in one single object for the entire scene
    //Range created to restrict tweakings further than 1 or 1.5 by using a Slider.
    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randomPitch = 0.1f;
    //Bool for the loop function of Audio Sources
    public bool loop = false;

    
    public AudioSource source;
    /// <summary>
    /// Sets an audio source by allowing you to drag in a sound as you would on any other object with an Audio Source.
    /// </summary>
    /// <param name="_source"></param>
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        //source.outputAudioMixerGroup = ;
    }
    /// <summary>
    /// Stops a sound from playing.
    /// </summary>
    public void Stop()
    {
        source.Stop();
    }
    /// <summary>
    /// Plays the selected sound with adjusted volumes and pitches to give a better audio range for more repetitive sounds.
    /// </summary>
    public void Play()
    {
        //Creating random ranges for the volume and pitch allow for some variety in more repetitive sounds 
        //so that they do not sound boring and monotonous.
        source.volume = volume * (1 + Random.Range(-randomVolume / 2f, randomVolume / 2f));
        source.pitch = pitch * (1 + Random.Range(-randomPitch / 2f, randomPitch / 2f)); ;
        source.Play();
    }

}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //An array for the Sounds that will be used
    [SerializeField]
    Sound[] sounds;
    //Functionality to stop the AudioManager from changing or being destroyed in other scenes 
    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }

    }

    void Start()
    {
        //Allows for the Sounds to be added to the AudioManager object instead of having to use separate Audiosources.
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            //Sets the spawned sound to be a child of the Game Manager object.
            _go.transform.SetParent(this.transform);

            //Same as referencing the AudioSource like you would traditionally,
            //but with this it eliminates the need for a variable.
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }

        

    }

    //To test stopping sounds
    /* void Update()
    {
        if(Time.time > 5f)
        {
            StopSound("Start Screen");
        }

    } */
    /// <summary>
    /// Stops a specific sound with a specific name used as a variable 
    /// </summary>
    /// <param name="_name"></param>
    public void StopSound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }

        //no sound with _name error which is used to debug missing sounds and wrong names 
        Debug.LogWarning("AudioManager: Sound not found on list, " + _name);
    }
    /// <summary>
    /// Plays a specific sound with a specific name used as a variable 
    /// </summary>
    /// <param name="_name"></param>
    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        //no sound with _name
        Debug.LogWarning("AudioManager: Sound not found on list, " + _name);
    }

}
