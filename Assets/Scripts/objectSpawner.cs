using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class objectSpawner : MonoBehaviour
{
    public NavMeshSurface surface;

    public Transform[] spawnLocations;
    public GameObject[] SpawnedObject;
    public GameObject[] SpawnedObjectClone;

    //float timer = 0f;
    bool spawned = false;

    void Start()
    {
        
    }

    
    void Update()
    {
       /* timer += Time.deltaTime;

        if(timer >= 40&&!spawned)
        {
            Spawn();
            spawned = true;
        }*/
    }

    public void Spawn()
    {
        SpawnedObjectClone[0] = Instantiate(SpawnedObject[0], spawnLocations[0].transform.position, Quaternion.Euler(0,0,0)) as GameObject;
        SpawnedObjectClone[1] = Instantiate(SpawnedObject[1], spawnLocations[1].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        SpawnedObjectClone[2] = Instantiate(SpawnedObject[2], spawnLocations[2].transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
        surface.BuildNavMesh();
    }
}
