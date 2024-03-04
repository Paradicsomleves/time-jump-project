using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DestoryedChange : MonoBehaviour
{
    GameObject pastBuilding;
    GameObject presentBuilding;
    public bool IsPast;

    // Start is called before the first frame update
    void Start()
    {
        
        pastBuilding = this.transform.GetChild(0).gameObject;
        presentBuilding = this.transform.GetChild(1).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !IsPast)
        {
            //We're in the present
            pastBuilding.SetActive(false);
            presentBuilding.SetActive(true);
            IsPast = true;

        }
        else if (Input.GetKeyDown(KeyCode.Space) && IsPast)
        {
            // We're in the past
            pastBuilding.SetActive(true);
            presentBuilding.SetActive(false);

            IsPast = false;
        }

    }
}
