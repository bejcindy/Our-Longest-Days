using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickIndication : MonoBehaviour
{
    public GameObject Marker;

    RaycastHit hit;
    private float raycastLength = 100;
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (GameManage.dialogFinished)
        {
            if(Physics.Raycast(ray, out hit, raycastLength))
            {
                if (hit.collider.name == "floor")
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        
                        GameObject TargetMarker = Instantiate(Marker, hit.point, Quaternion.Euler(90,0,0)) as GameObject;


                    }
                }
            }
        }
        
        
    }

    
}
