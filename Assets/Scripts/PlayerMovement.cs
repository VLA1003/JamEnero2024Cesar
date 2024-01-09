using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 15f;
    public GameObject poop;
    public float poopDecayTime = 0;
    public bool canPoop = true;
    public Image staminaBar, poopCover;
    float stamina = 3f, maxStamina = 3f;
    bool running = false, canRun = true;
    public bool isMoving;

    public Animator animator;

    private void Start()
    {
        poop.SetActive(false);
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");

        if (horizontalMovement != 0f || verticalMovement != 0f)
        {
            isMoving = true;

            if (Mathf.Abs(horizontalMovement) > Mathf.Abs(verticalMovement))
            {
                verticalMovement = 0f;
            }
            else
            {
                horizontalMovement = 0f;
            }
        }
        else
        {
            isMoving = false;
        }
        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0f) * speed * Time.deltaTime;
        gameObject.GetComponent<Rigidbody2D>().MovePosition(transform.position + movement * speed * Time.deltaTime);

        UpdateAnimatorParameters(horizontalMovement, verticalMovement);
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
            if (poopCover.gameObject.activeSelf == false)
            {
                poopCover.gameObject.SetActive(true);
                poopCover.fillAmount = 1f;
            }
            poopDecayTime += Time.deltaTime;
            poopCover.fillAmount = 1 - poopDecayTime / 3f;
        }

            if (poopDecayTime > 3f)
            {
                poopDecayTime = 0f;
                poop.transform.SetParent(gameObject.transform);
                poop.transform.localPosition = Vector2.zero;
                poop.SetActive(false);
                poopCover.gameObject.SetActive(false);
                canPoop = true;
            }
        //}
        if (running == true)
        {
            if (stamina > 0f)
            {
                stamina -= Time.deltaTime;
                staminaBar.fillAmount = stamina / maxStamina;
            } else
            {
                running = false;
                canRun = false;
                staminaBar.color = new Color (1f, 0.7466f, .6941f, 1f);
                speed = 10f;
            }
        } else
        {
            if (stamina < maxStamina)
            {
                stamina += Time.deltaTime / 2.5f;
                staminaBar.fillAmount = stamina / maxStamina;

            } else
            {
                stamina = maxStamina;
                running = false;
                canRun = true;
                staminaBar.color = new Color (1, 0.9870f, 0.6933f, 1f);
                staminaBar.gameObject.SetActive(false);
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canRun == true)
        {
            if (staminaBar.gameObject.activeSelf == false)
            {
                staminaBar.gameObject.SetActive(true);
            }
            running = true;
            speed = 22.5f;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;

            if (canRun == true)
            {
                speed = 15f;
            }
        }

    }

    private void UpdateAnimatorParameters(float horizontal, float vertical)
    {
        animator.SetFloat("Horizontal", horizontal);
        animator.SetFloat("Vertical", vertical);
        animator.SetBool("IsMoving", isMoving);
    }

    void DropPoop()
    {
        poopDecayTime = 0f;
        poop.SetActive(true);
        poop.transform.SetParent(null);
        canPoop = false;
    }
}
