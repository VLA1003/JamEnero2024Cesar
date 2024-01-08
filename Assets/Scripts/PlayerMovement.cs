using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // if (PauseMenu.isPaused = false)
        //{
        //
        //}
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);
    }
}
