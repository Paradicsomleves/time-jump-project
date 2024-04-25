using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

public class Interact : MonoBehaviour
{
    public TMP_Text text;
    public GameObject textObject;
    ObjectProperties objectProperties;

    [SerializeField] float interactRange = 1.2f;
    public bool foundTarget;

    StarterAssetsInputs _input;
    public GameObject player;

    public Vector3 targetPos;

    private void Start()
    {
        _input = player.GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_input.interact && objectProperties != null)
        {
            if (objectProperties.objectType == ObjectProperties.OfTypes.person)
            {
                _input.interact = false;
                Debug.Log("Interacted!");
            }

            else if (objectProperties.objectType == ObjectProperties.OfTypes.item)
            {
                _input.interact = false;
                Debug.Log("Picked Up!");
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ObjectProperties _objectType))
        {
            foundTarget = true;
            targetPos = other.transform.position;

            objectProperties = _objectType;
            text.text = _objectType.customOverheadText;
            textObject.transform.position = other.transform.position + _objectType.textOffset;
            Debug.DrawLine(player.transform.position, other.transform.position);
            StartCoroutine(IsTargetCloseEnough(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ObjectProperties _objectType))
        {
            
            objectProperties = null;
            textObject.SetActive(false);
            StopAllCoroutines();
            foundTarget = false;
        }
    }

    IEnumerator IsTargetCloseEnough(Collider target)
    {
        while (objectProperties != null)
        {
            if ((target.transform.position - player.transform.position).sqrMagnitude < interactRange)
            {
                textObject.SetActive(true);
            } else { textObject.SetActive(false); }
            yield return new WaitForSeconds(0.1f);
        }
    }
}