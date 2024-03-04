using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControl : MonoBehaviour
{
    public Transform player;
    public GameObject mainCamera;
    ThirdPersonController thirdPersonController;

    public float lag;
    public float cameraSpeed = 2f;
    public Vector3 cameraOffset = new Vector3(0, 1, 0);

    private void Start()
    {
        thirdPersonController = player.GetComponent<ThirdPersonController>();

    }

    void Update()
    {
        if (IsPlayerMoving())
        {
            CameraMovement(lag);
        }
        else
        {
            CameraMovement(0f);
        }
        mainCamera.transform.LookAt(player.position + cameraOffset);
    }

    void CameraMovement(float lag)
    {
        float _cameraSpeed = Mathf.Clamp01((transform.position - player.transform.position).sqrMagnitude) * cameraSpeed;
        if (_cameraSpeed > lag)
        {
            Vector3 desiredPosition = player.position;
            Vector3 smoothedPostition = Vector3.Lerp(transform.position, desiredPosition, _cameraSpeed * Time.deltaTime);

            

            transform.position = smoothedPostition;
        } else
        {
            _cameraSpeed = 0f;
        }

        Debug.Log((transform.position - player.transform.position).sqrMagnitude - lag);
    }


    bool IsPlayerMoving()
    {
        bool characterMoves;
        if (Keyboard.current.wKey.isPressed || Keyboard.current.aKey.isPressed || Keyboard.current.sKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            characterMoves = true;
            //  Debug.Log("Character Moves");
        }
        else
        {
            characterMoves = false;
            // Debug.Log("Character DOESN'T Move");
        }
        return characterMoves;
    }

}
