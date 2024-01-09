using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;
    public GameObject poop;
    public float poopDecayTime = 0;
    public bool canPoop = true;

    private void Start()
    {
        poop.SetActive(false);
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

        if (horizontalMovement != 0f && verticalMovement != 0f)
        {
            if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
            {
                verticalMovement = 0f;
            }
            else
            {
                horizontalMovement = 0f;
            }
        }
        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0f) * speed * Time.deltaTime;
        gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + movement * speed * Time.deltaTime);
    }

    private void Update()
    {
        //if (PauseMenu.instancePausa.isPaused == false)
        //{
        if (Input.GetKeyDown(KeyCode.Space) && poop.activeSelf == false)
            {
                //Debug.Log("Ha defecado");
                DropPoop();
            }

        if (poop.activeSelf == true)
        {
            poopDecayTime += Time.deltaTime;
        }

            if (poopDecayTime > 3f)
            {
                poopDecayTime = 0f;
                poop.transform.SetParent(gameObject.transform);
                poop.transform.localPosition = Vector2.zero;
                poop.SetActive(false);
                canPoop = true;
            }
        //}

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 22.5f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 15f;
        }

    }

    void DropPoop()
    {
        poopDecayTime = 0f;
        poop.SetActive(true);
        poop.transform.SetParent(null);
        canPoop = false;
    }
}
