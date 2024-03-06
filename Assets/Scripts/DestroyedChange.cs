using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyedChange : MonoBehaviour
{
    GameObject pastBuilding;
    GameObject presentBuilding;
    public bool IsPast = false;


    void Start()
    {
        pastBuilding = this.transform.GetChild(0).gameObject;
        presentBuilding = this.transform.GetChild(1).gameObject;

        ChangeObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeObjects();
        }
        
    }

    private void ChangeObjects()
    {
        if (!IsPast)
        {
            //We're in the present
            pastBuilding.SetActive(false);
            presentBuilding.SetActive(true);
            IsPast = true;
            //Debug.Log("CHANGE!");

        }
        else if (IsPast)
        {
            // We're in the past
            pastBuilding.SetActive(true);
            presentBuilding.SetActive(false);
            //Debug.Log("CHANGE!");

            IsPast = false;
        }
    }
}
