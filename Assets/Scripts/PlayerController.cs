using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    public static PlayerController instance;
    public float verticalInput;
    public float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        PlayerMovement();
    }

    public virtual void PlayerMovement()
    {


        Vector3 playerMovement = new Vector3(horizontalInput, 0, verticalInput);

        transform.Translate(playerMovement.normalized * speed * Time.deltaTime);
    }

    
}
