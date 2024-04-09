using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class DetectOthers : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] GameObject head;

    [SerializeField] LayerMask layerMask;

    private void Start()
    {
        LookingAtStuff();
    }
    private void FixedUpdate()
    {
            
        if (target.transform.position.z < transform.localPosition.z)
        {
            LookingAtStuff();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LookTarget"))
        {
            target.transform.position = other.transform.position;
        }
        else if (other.CompareTag("Interact"))
        {
            target.transform.position = other.transform.position;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LookTarget") || other.CompareTag("Interact"))
        {

        }
    }

    void LookingAtStuff()
    {
        float[] randoms = new float[3];
        for (int i = 0; i < 3; i++)
        {
            randoms[i] = UnityEngine.Random.Range(-1f, 1f);
        }

        Vector3 facing = new Vector3(randoms[0], randoms[1]/2+0.4f, randoms[2]+1).normalized;

        RaycastHit hit;

        if (Physics.Raycast(head.transform.position, facing, out hit, 50, layerMask))
        {
            Debug.Log("Valamit eltalált, " + hit.distance + " unit-ra innen");
            target.gameObject.transform.position = hit.point;
        }
        
    }
}
